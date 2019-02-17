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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            Container container = new Container();

            container.Options.DefaultLifestyle = new AsyncScopedLifestyle();

            container.Register<IEventStore, Infrastructure.EventStore>();
            container.Register(typeof(IAggregateRepository<,>), typeof(AggregateRepository<,>));
            var eventStoreUrl = Configuration.GetValue<string>("ExternalServices:EventStoreUrl");

            container.Register<IEventStoreConnection>(() => EventStoreConnection.Create(new Uri(eventStoreUrl), Guid.NewGuid().ToString()));
            container.Register(typeof(ICommandHandler<>), typeof(ICommandHandler<>).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);

            container.Register<InvoicesController>();

            var redisConnectionString = Configuration.GetValue<string>("ExternalServices:RedisUrl");
            container.Register<IRedisClientsManager>(() => new RedisManagerPool(redisConnectionString));
            container.Register<ISubscriber<Invoice, Guid>, RedisInvoiceSubscriber>();
            container.Collection.Register(typeof(ISubscriber<,>), typeof(ISubscriber<,>).Assembly);
            container.Register<IMessageBus<Invoice, Guid>, MessageBus<Invoice, Guid>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));



            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
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
        }
    }
}
