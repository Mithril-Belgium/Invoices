using Mithril.Invoices.Domain.Invoice;
using ServiceStack.Redis;
using System;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure.Bus
{
    public class RedisInvoiceSubscriber : ISubscriber<Invoice, Guid>
    {
        private readonly IRedisClientsManager _redisClientsManager;

        public RedisInvoiceSubscriber(IRedisClientsManager redisClientsManager)
        {
            _redisClientsManager = redisClientsManager;
        }

        public Task NotifyAsync(Invoice aggregate)
        {
            using(var client = _redisClientsManager.GetClient())
            {
                client.Set(aggregate.Id.ToString(), aggregate);
            }

            return Task.CompletedTask; 
        }
    }
}
