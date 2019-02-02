using Mithril.Invoices.Domain.Core;

namespace Mithril.Invoices.Infrastructure
{
    public interface IEventStore
    {
        IDomainEvent[] GetEvents<TId>(TId id);

        void SaveEvents(IDomainEvent[] events);
    }
}
