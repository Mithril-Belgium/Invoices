using Mithril.Invoices.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure
{
    public class AggregateRepository<T, TId>
        where T : AggregateRoot<TId>
    {
        private readonly IEventStore _eventStore;

        public AggregateRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Save(T aggregateRoot)
        {
            await _eventStore.SaveEventsAsync(aggregateRoot.Id, 
                aggregateRoot.PendingEvents.ToArray());

            aggregateRoot.ClearPendingEvents();
        }

        public async Task<T> GetById(TId id)
        {
            T aggregateRoot = Activator.CreateInstance<T>();
            var domainEvents = await _eventStore.GetEventsAsync(id);

            foreach(var domainEvent in domainEvents)
            {
                aggregateRoot.RaiseEvent(domainEvent);
            }

            return aggregateRoot;
        }
    }
}
