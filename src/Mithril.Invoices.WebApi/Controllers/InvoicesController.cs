using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Application.InvoiceConsultation;
using Mithril.Invoices.Application.InvoiceCreation;
using Mithril.Invoices.Domain.Invoice;

namespace Mithril.Invoices.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ICommandHandler<InvoiceCreationCommand> _invoiceCreationHandler;
        private readonly IQueryHandler<InvoiceConsultationQuery, InvoiceConsultationModel> _invoiceConsultationHandler;

        public InvoicesController(ICommandHandler<InvoiceCreationCommand> invoiceCreationHandler, IQueryHandler<InvoiceConsultationQuery, InvoiceConsultationModel> invoiceConsultationHandler)
        {
            _invoiceCreationHandler = invoiceCreationHandler;
            _invoiceConsultationHandler = invoiceConsultationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice()
        {
            var command = new InvoiceCreationCommand(Guid.NewGuid());

            await _invoiceCreationHandler.ProcessAsync(command);

            return Ok(command.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ConsultInvoice(Guid id)
        {
            var query = new InvoiceConsultationQuery(id);

            var invoice = await _invoiceConsultationHandler.ProcessAsync(query);

            return Ok(invoice);
        }
    }
}
