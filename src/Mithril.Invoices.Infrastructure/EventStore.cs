using System;
using System.Collections.Generic;
using System.Text;
using Mithril.Invoices.Domain.Core;

namespace Mithril.Invoices.Infrastructure
{
    public class EventStore : IEventStore
    {
        public IDomainEvent[] GetEvents<TId>(TId id)
        {
            throw new NotImplementedException();
        }

        public void SaveEvents(IDomainEvent[] events)
        {
            throw new NotImplementedException();
        }
    }
}
