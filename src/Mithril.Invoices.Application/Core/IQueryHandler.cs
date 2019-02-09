using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.Core
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        Task<TResult> ProcessAsync(TQuery query);
    }
}
