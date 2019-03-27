using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Application.InvoiceCreation;
using Mithril.Invoices.Bootstrap;
using Mithril.Invoices.Domain.Invoice;
using Mithril.Invoices.Infrastructure;
using Mithril.Invoices.Infrastructure.Bus;
using Mithril.Invoices.WebApi.Controllers;
using ServiceStack.Redis;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Mithril.Invoices.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private Container Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddLogging(lb =>
            {
                lb.ClearProviders();
                lb.AddLog4Net();
            });

            var eventStoreUrl = Configuration.GetValue<string>("ExternalServices:EventStoreUrl");
            var mongoConnectionString = Configuration.GetValue<string>("ExternalServices:MongoUrl");

            Container = ContainerBuilder.GetContainer(eventStoreUrl, mongoConnectionString);
            Container.Register<InvoicesController>();
            Container.Register<PingController>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(Container));

            services.UseSimpleInjectorAspNetRequestScoping(Container);
            services.EnableSimpleInjectorCrossWiring(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            Container.AutoCrossWireAspNetComponents(app);
        }
    }
}
