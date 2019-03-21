using System;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.InvoiceConsultation
{
    public interface IInvoiceConsultationRepository
    {
        Task<InvoiceConsultationModel> GetForConsultationByIdAsync(Guid id);
    }
}
