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
        private readonly IRedisClientsManager _redisClientsManager;

        public InvoiceConsultationQueryHandler(IRedisClientsManager redisClientsManager)
        {
            _redisClientsManager = redisClientsManager;
        }

        public Task<InvoiceConsultationModel> ProcessAsync(InvoiceConsultationQuery query)
        {
            using (var client = _redisClientsManager.GetClient())
            {
                var invoice = client.Get<Invoice>(query.Id.ToString());

                return Task.FromResult(new InvoiceConsultationModel() {
                    Id = invoice.Id,
                    Version = invoice.Version
                });
            }
        }
    }
}
