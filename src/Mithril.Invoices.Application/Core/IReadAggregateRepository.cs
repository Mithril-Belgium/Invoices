using Mithril.Invoices.Domain.Core;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.Core
{
    public interface IReadAggregateRepository<T, TId>
        where T : AggregateRoot<TId>
    {
        Task<T> GetByIdAsync(TId id);
    }
}
