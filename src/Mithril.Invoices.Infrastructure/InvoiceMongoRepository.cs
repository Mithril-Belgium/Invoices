using Mithril.Invoices.Application.InvoiceConsultation;
using Mithril.Invoices.Domain.Invoice;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure
{
    public class InvoiceMongoRepository : MongoAggregateRepository<Invoice, Guid>, IInvoiceConsultationRepository
    {
        public InvoiceMongoRepository(MongoClient mongoClient) : base(mongoClient)
        {
        }

        public async Task<InvoiceConsultationModel> GetForConsultationByIdAsync(Guid id)
        {
            var database = _mongoClient.GetDatabase(DatabaseName);
            var collection = database.GetCollection<Invoice>(CollectionName);

            var fields = Builders<Invoice>.Projection.Include(p => p.Id).Include(p => p.Version);
            return await collection.Find(i => i.Id == id).Project<InvoiceConsultationModel>(fields).FirstOrDefaultAsync();
        }
    }
}
