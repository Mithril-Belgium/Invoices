using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Application.InvoiceCreation;

namespace Mithril.Invoices.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ICommandHandler<InvoiceCreationCommand, Guid> _invoiceCreationHandler;

        public InvoicesController(ICommandHandler<InvoiceCreationCommand, Guid> invoiceCreationHandler)
        {
            _invoiceCreationHandler = invoiceCreationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice()
        {
            var command = new InvoiceCreationCommand(Guid.NewGuid());

            var id = await _invoiceCreationHandler.ProcessAsync(command);

            return Ok(id);
        }
    }
}
