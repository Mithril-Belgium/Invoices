using Mithril.Invoices.Domain.Core;
using Mithril.Invoices.Domain.Invoice.Events;
using System;
using System.Collections.Generic;

namespace Mithril.Invoices.Domain.Invoice
{
    public class Invoice : AggregateRoot<Guid>
    {
        private List<InvoiceLine> _invoiceLines;

        public IEnumerable<InvoiceLine> InvoiceLines => _invoiceLines.AsReadOnly();

        public Invoice(Guid guid) : this()
        {
            RaiseEvent(new InvoiceCreated(guid));
        }

        private Invoice()
        {
            _invoiceLines = new List<InvoiceLine>();
        }

        public void AddInvoiceLine(string label)
        {
            RaiseEvent(new InvoiceLineAdded(_invoiceLines.Count + 1, label));
        }

        internal void ApplyEvent(InvoiceCreated @event)
        {
            Id = @event.Id;
        }

        internal void ApplyEvent(InvoiceLineAdded @event)
        {
            _invoiceLines.Add(new InvoiceLine(@event.LineNumber, @event.Label));
        }
    }
}
