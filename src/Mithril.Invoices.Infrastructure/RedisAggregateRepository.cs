using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Domain.Core;
using Mithril.Invoices.Domain.Invoice;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure
{
    public class RedisAggregateRepository<T, TId> : IReadAggregateRepository<T, TId>, IWriteAggregateRepository<T, TId>
        where T : AggregateRoot<TId>
    {
        private readonly IRedisClientsManager _redisClientsManager;

        public RedisAggregateRepository(IRedisClientsManager redisClientsManager)
        {
            _redisClientsManager = redisClientsManager;
        }

        public Task<T> GetByIdAsync(TId id)
        {
            using (var client = _redisClientsManager.GetClient())
            {
                return Task.FromResult(client.Get<T>(id.ToString()));           
            }
        }

        public Task SaveAsync(T aggregateRoot)
        {
            using (var client = _redisClientsManager.GetClient())
            {
                client.Set(aggregateRoot.Id.ToString(), aggregateRoot);
            }

            return Task.CompletedTask;
        }
    }
}
