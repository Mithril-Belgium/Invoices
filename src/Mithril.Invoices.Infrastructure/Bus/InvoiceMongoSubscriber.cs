using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Domain.Invoice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure.Bus
{
    public class InvoiceMongoSubscriber : ISubscriber<Invoice, Guid>
    {
        private readonly IWriteAggregateRepository<Invoice, Guid> _repository;

        public InvoiceMongoSubscriber(MongoAggregateRepository<Invoice, Guid> repository)
        {
            _repository = repository;
        }

        public async Task NotifyAsync(Invoice aggregateRoot)
        {
            await _repository.SaveAsync(aggregateRoot);
        }
    }
}
