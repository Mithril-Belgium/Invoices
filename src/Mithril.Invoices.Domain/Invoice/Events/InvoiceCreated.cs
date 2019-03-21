using Mithril.Invoices.Domain.Core;
using System;

namespace Mithril.Invoices.Domain.Invoice.Events
{
    public class InvoiceCreated : IDomainEvent
    {
        public Guid Id { get; private set; }

        public InvoiceCreated(Guid id)
        {
            Id = id;
        }
    }
}
