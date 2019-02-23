using Mithril.Invoices.Domain.Core;
using System;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure
{
    public interface IEventStore : IDisposable
    {
        Task<IDomainEvent[]> GetEventsAsync<TId>(TId id);

        Task SaveEventsAsync<TId>(TId id, IDomainEvent[] events);
    }
}
