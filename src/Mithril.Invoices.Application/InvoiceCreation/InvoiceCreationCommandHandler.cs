using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Domain.Invoice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.InvoiceCreation
{
    public class InvoiceCreationCommandHandler : ICommandHandler<InvoiceCreationCommand>
    {
        private readonly IWriteAggregateRepository<Invoice, Guid> _invoiceRepository;

        public InvoiceCreationCommandHandler(IWriteAggregateRepository<Invoice, Guid> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task ProcessAsync(InvoiceCreationCommand command)
        {
            var invoice = new Invoice(command.Id);

            await _invoiceRepository.SaveAsync(invoice);
        }
    }
}
