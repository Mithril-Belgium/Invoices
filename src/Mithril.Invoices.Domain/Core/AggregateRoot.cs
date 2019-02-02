using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mithril.Invoices.Domain.Core
{
    public class AggregateRoot<TId> : IAggregateRoot<TId>
    {
        public TId Id { get; protected set; }

        private List<IDomainEvent> _pendingEvents;

        public IReadOnlyList<IDomainEvent> PendingEvents => _pendingEvents.AsReadOnly();

        public void ClearPendingEvents()
        {
            _pendingEvents.Clear();
        }

        public AggregateRoot()
        {
            _pendingEvents = new List<IDomainEvent>();
        }

        public void RaiseEvent(IDomainEvent domainEvent)
        {
            _pendingEvents.Add(domainEvent);
            ApplyEventToAggregate(domainEvent);
        }

        private void ApplyEventToAggregate(IDomainEvent domainEvent)
        {
            ((dynamic)this).ApplyEvent((dynamic)domainEvent);
        }
    }
}
