using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Domain.Invoice;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.InvoiceConsultation
{
    public class InvoiceConsultationQueryHandler : IQueryHandler<InvoiceConsultationQuery, InvoiceConsultationModel>
    {
        private readonly IReadAggregateRepository<Invoice, Guid> _repository;

        public InvoiceConsultationQueryHandler(IReadAggregateRepository<Invoice, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceConsultationModel> ProcessAsync(InvoiceConsultationQuery query)
        {
            var invoice = await _repository.GetByIdAsync(query.Id);

            return new InvoiceConsultationModel() {
                Id = invoice.Id,
                Version = invoice.Version
            };
        }
    }
}
