using Mithril.Invoices.Domain.Core;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure
{
    public interface IEventStore
    {
        Task<IDomainEvent[]> GetEventsAsync<TId>(TId id);

        Task SaveEventsAsync<TId>(TId id, IDomainEvent[] events);
    }
}
