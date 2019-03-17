using EventStore.ClientAPI;
using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Domain.Invoice;
using Mithril.Invoices.Infrastructure;
using Mithril.Invoices.Infrastructure.Bus;
using ServiceStack.Redis;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Mithril.Invoices.Bootstrap
{
    public static class ContainerBuilder
    {
        public static Container GetContainer(string eventStoreUrl, string redisConnectionString)
        {
            var container = new Container();

            container.Options.DefaultLifestyle = new AsyncScopedLifestyle();

            container.Register<IEventStore, Infrastructure.EventStore>();
            container.Register(typeof(IReadAggregateRepository<,>), typeof(AggregateRepository<,>));
            container.Register(typeof(IWriteAggregateRepository<,>), typeof(AggregateRepository<,>));


            container.Register<IEventStoreConnection>(() => EventStoreConnection.Create(new Uri(eventStoreUrl), Guid.NewGuid().ToString()));
            container.Register(typeof(ICommandHandler<>), typeof(ICommandHandler<>).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);

            container.Register<IRedisClientsManager>(() => new RedisManagerPool(redisConnectionString));
            container.Register<ISubscriber<Invoice, Guid>, RedisInvoiceSubscriber>();
            container.Collection.Register(typeof(ISubscriber<,>), typeof(ISubscriber<,>).Assembly);
            container.Register<IMessageBus<Invoice, Guid>, MessageBus<Invoice, Guid>>();

            return container;
        }
    }
}
