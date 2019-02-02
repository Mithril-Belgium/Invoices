using Mithril.Invoices.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task SaveAsync(T aggregateRoot)
        {
            await _eventStore.SaveEventsAsync(aggregateRoot.Id, 
                aggregateRoot.PendingEvents.ToArray());

            aggregateRoot.ClearPendingEvents();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            T aggregateRoot = (T)typeof(T)
                    .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                        null, new Type[0], new ParameterModifier[0])
                    .Invoke(new object[0]);

            var domainEvents = await _eventStore.GetEventsAsync(id);

            foreach(var domainEvent in domainEvents)
            {
                aggregateRoot.RaiseEvent(domainEvent);
            }

            return aggregateRoot;
        }
    }
}
