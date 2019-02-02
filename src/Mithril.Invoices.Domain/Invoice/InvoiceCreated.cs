using Mithril.Invoices.Domain.Core;
using System;

namespace Mithril.Invoices.Domain.Invoice
{
    public class InvoiceCreated : IDomainEvent
    {
        public Guid Id { get; }

        public InvoiceCreated(Guid id)
        {
            Id = id;
        }
    }
}
