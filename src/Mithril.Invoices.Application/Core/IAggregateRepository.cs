using Mithril.Invoices.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.Core
{
    public interface IAggregateRepository<T, TId>
        where T : AggregateRoot<TId>
    {
        Task SaveAsync(T aggregateRoot);

        Task<T> GetByIdAsync(TId id);
    }
}
