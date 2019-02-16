using Mithril.Invoices.Application.Core;
using System;

namespace Mithril.Invoices.Application.InvoiceConsultation
{
    public class InvoiceConsultationQuery : IQuery
    {
        public Guid Id { get; }

        public InvoiceConsultationQuery(Guid id)
        {
            Id = id;
        }
    }
}
