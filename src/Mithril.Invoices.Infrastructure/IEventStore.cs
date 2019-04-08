using Mithril.Invoices.Domain.Core;
using System;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure
{
    public interface IEventStore : IDisposable
    {
        Task<IDomainEvent[]> GetEventsAsync<TId>(string category, TId id);

        Task SaveEventsAsync<TId>(string category, TId id, IDomainEvent[] events);
    }
}
