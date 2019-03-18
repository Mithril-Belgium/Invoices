using EventStore.ClientAPI;
using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Application.InvoiceConsultation;
using Mithril.Invoices.Application.InvoiceCreation;
using Mithril.Invoices.Domain.Invoice;
using Mithril.Invoices.Infrastructure;
using Mithril.Invoices.Infrastructure.Bus;
using MongoDB.Driver;
using ServiceStack.Redis;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;

namespace Mithril.Invoices.Bootstrap
{
    public static class ContainerBuilder
    {
        public static Container GetContainer(string eventStoreUrl, string mongoConnectionString)
        {
            var container = new Container();

            container.Options.DefaultLifestyle = new AsyncScopedLifestyle();

            container.Register<IEventStore, Infrastructure.EventStore>();
            container.Register(typeof(IWriteAggregateRepository<,>), typeof(EventAggregateRepository<,>));

            container.RegisterConditional<IReadAggregateRepository<Invoice, Guid>, MongoAggregateRepository<Invoice, Guid>>(c => c.Consumer.ImplementationType == typeof(InvoiceConsultationQueryHandler));
            container.RegisterConditional(typeof(IReadAggregateRepository<,>), typeof(EventAggregateRepository<,>), c => !c.Handled);

            container.Register<IEventStoreConnection>(() => EventStoreConnection.Create(new Uri(eventStoreUrl), Guid.NewGuid().ToString()));
            container.Register(typeof(ICommandHandler<>), typeof(ICommandHandler<>).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);
            
            container.Register<MongoClient>(() => new MongoClient(mongoConnectionString));
            container.Collection.Register(typeof(ISubscriber<,>), typeof(ISubscriber<,>).Assembly);
            container.Register<IMessageBus<Invoice, Guid>, MessageBus<Invoice, Guid>>();

            return container;
        }
    }
}
