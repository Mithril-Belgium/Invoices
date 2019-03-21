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
        private readonly IInvoiceConsultationRepository _repository;

        public InvoiceConsultationQueryHandler(IInvoiceConsultationRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceConsultationModel> ProcessAsync(InvoiceConsultationQuery query)
        {
            return await _repository.GetForConsultationByIdAsync(query.Id);
        }
    }
}
