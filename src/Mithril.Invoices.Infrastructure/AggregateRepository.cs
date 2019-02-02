using Mithril.Invoices.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mithril.Invoices.Infrastructure
{
    public class AggregateRepository<T, TId>
        where T : AggregateRoot<TId>
    {
        public void Save(T aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public T GetById(TId id)
        {
            throw new NotImplementedException();
        }
    }
}
