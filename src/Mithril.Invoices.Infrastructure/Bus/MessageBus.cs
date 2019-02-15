using Mithril.Invoices.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure.Bus
{
    public class MessageBus<TAggregate, TId> : IMessageBus<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
    {
        private HashSet<ISubscriber<TAggregate, TId>> _subscribers;

        public MessageBus()
        {
            _subscribers = new HashSet<ISubscriber<TAggregate, TId>>();
        }

        public void Subscribe(ISubscriber<TAggregate, TId> subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public async Task PublishAsync(TAggregate aggregate)
        {
            var tasks = new List<Task>();
            foreach(var subscriber in _subscribers)
            {
                tasks.Add(subscriber.NotifyAsync(aggregate));
            }

            await Task.WhenAll(tasks);
        }
    }
}
