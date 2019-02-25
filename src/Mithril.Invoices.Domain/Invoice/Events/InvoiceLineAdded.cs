using Mithril.Invoices.Domain.Core;

namespace Mithril.Invoices.Domain.Invoice.Events
{
    public class InvoiceLineAdded : IDomainEvent
    {
        public int LineNumber { get; private set; }
        public string Label { get; private set; }


        public InvoiceLineAdded(int lineNumber, string label)
        {
            LineNumber = lineNumber;
            Label = label;
        }
    }
}
