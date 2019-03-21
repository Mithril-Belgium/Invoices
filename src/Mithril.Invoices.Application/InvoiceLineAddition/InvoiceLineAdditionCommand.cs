using Mithril.Invoices.Application.Core;

namespace Mithril.Invoices.Application.InvoiceLineAddition
{
    public class InvoiceLineAdditionCommand : ICommand
    {
        public string Label { get; }

        public InvoiceLineAdditionCommand(string label)
        {
            Label = label;
        }
    }
}
