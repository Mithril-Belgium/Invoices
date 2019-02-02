using Mithril.Invoices.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mithril.Invoices.Domain.Invoice
{
    public class Invoice : AggregateRoot<Guid>
    {
        public Invoice()
        {
            RaiseEvent(new InvoiceCreated(Guid.NewGuid()));
        }

        internal void ApplyEvent(InvoiceCreated @event)
        {
            Id = @event.Id;
        }
    }
}
