using System.Collections.Generic;

namespace Mithril.Invoices.Domain.Core
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; }

        IReadOnlyList<IDomainEvent> PendingEvents { get; }

        void ClearPendingEvents();
    }
}
