using Mithril.Invoices.Domain.Core;
using System;

namespace Mithril.Invoices.Domain.Invoice
{
    public class InvoiceLine : ValueObject<InvoiceLine>
    {
        public int LineNumber { get; set; }

        public string Label { get; }

        public InvoiceLine(int lineNumber, string label)
        {
            LineNumber = lineNumber;
            Label = label;
        }

        private InvoiceLine()
        {
        }

        public override bool EqualsCore(InvoiceLine obj)
        {
            return LineNumber == obj.LineNumber && Label == obj.Label;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(LineNumber, Label);
        }
    }
}
