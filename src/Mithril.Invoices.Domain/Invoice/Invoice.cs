using Mithril.Invoices.Domain.Core;
using System;

namespace Mithril.Invoices.Domain.Invoice
{
    public class Invoice : AggregateRoot<Guid>
    {
        public Invoice(Guid guid)
        {
            RaiseEvent(new InvoiceCreated(guid));
        }

        private Invoice()
        {
        }

        internal void ApplyEvent(InvoiceCreated @event)
        {
            Id = @event.Id;
        }
    }
}
