using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using JsonNet.PrivateSettersContractResolvers;
using Mithril.Invoices.Domain.Core;
using Mithril.Invoices.Domain.Invoice;
using Newtonsoft.Json;

namespace Mithril.Invoices.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreConnection _connection;

        public EventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task<IDomainEvent[]> GetEventsAsync<TId>(TId id)
        {
            var readEvents = await _connection.ReadStreamEventsForwardAsync(id.ToString(), 0, 100, true);
            return MapEvents(readEvents);
        }

        private IDomainEvent[] MapEvents(StreamEventsSlice eventSlice)
        {
            IDomainEvent[] domainEvents = new IDomainEvent[eventSlice.Events.Length];

            for(int i = 0; i < eventSlice.Events.Length; i++)
            {
                domainEvents[i] = (IDomainEvent)JsonConvert.DeserializeObject(
                Encoding.UTF8.GetString(eventSlice.Events[i].Event.Data),
                Type.GetType(eventSlice.Events[i].Event.EventType),
                new JsonSerializerSettings()
                {
                    ContractResolver = new PrivateSetterContractResolver()
                });
            }
            
            return domainEvents;
        }

        public async Task SaveEventsAsync<TId>(TId id, IDomainEvent[] domainEvents)
        {
            await _connection.ConnectAsync();
            EventData[] events = MapEvents(domainEvents);
            await _connection.AppendToStreamAsync(id.ToString(), ExpectedVersion.Any, events);
        }

        private EventData[] MapEvents(IDomainEvent[] domainEvents)
        {
            return domainEvents.Select(domainEvent => new EventData(
                Guid.NewGuid(),
                domainEvent.GetType().AssemblyQualifiedName,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domainEvent)),
                new byte[0])).ToArray();
        }
    }
}
