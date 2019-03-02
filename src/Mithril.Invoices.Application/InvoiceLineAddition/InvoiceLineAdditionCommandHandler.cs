using System;
using System.Threading.Tasks;
using Mithril.Invoices.Application.Core;

namespace Mithril.Invoices.Application.InvoiceLineAddition
{
    public class InvoiceLineAdditionCommandHandler : ICommandHandler<InvoiceLineAdditionCommand>
    {
        public Task ProcessAsync(InvoiceLineAdditionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
