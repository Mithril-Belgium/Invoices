using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mithril.Invoices.Domain.Core
{
    public class AggregateRoot<TId> : IAggregateRoot<TId>
    {
        public TId Id { get; }

        private List<IDomainEvent<TId>> _pendingEvents;

        public IReadOnlyList<IDomainEvent<TId>> PendingEvents => _pendingEvents.AsReadOnly();

        private IDictionary<string, Action<IDomainEvent<TId>>> _applyEvents;

        public void ClearPendingEvents()
        {
            _pendingEvents.Clear();
        }

        public AggregateRoot()
        {
            _pendingEvents = new List<IDomainEvent<TId>>();
        }

        public void RaiseEvent(IDomainEvent<TId> domainEvent)
        {
            _pendingEvents.Add(domainEvent);
            ApplyEventToAggregate(domainEvent);
        }

        private void ApplyEventToAggregate(IDomainEvent<TId> domainEvent)
        {
            dynamic thisObject = this;

            thisObject.ApplyEvent(domainEvent);
        }
    }
}
