using System;
using System.Threading.Tasks;
using Mithril.Invoices.Application.Core;
using Mithril.Invoices.Application.InvoiceConsultation;
using Mithril.Invoices.Domain.Core;
using Mithril.Invoices.Domain.Invoice;
using Mithril.Invoices.Infrastructure.Bus;
using MongoDB.Driver;

namespace Mithril.Invoices.Infrastructure
{
    public class MongoAggregateRepository<T, TId> : IWriteAggregateRepository<T, TId>
        where T : AggregateRoot<TId>
    {
        protected const string DatabaseName = "mithril-invoices";
        protected const string CollectionName = "invoices";

        protected readonly MongoClient _mongoClient;
       
        public MongoAggregateRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task SaveAsync(T aggregateRoot)
        {
            var database = _mongoClient.GetDatabase(DatabaseName);
            var collection = database.GetCollection<T>(CollectionName);

            var result = await collection.ReplaceOneAsync(
                i => i.Id.Equals(aggregateRoot.Id),
                aggregateRoot,
                new UpdateOptions()
                {
                    IsUpsert = true
                });
        }
    }
}
