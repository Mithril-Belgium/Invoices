using System;
using System.Collections.Generic;
using System.Text;

namespace Mithril.Invoices.Application.Core
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        TResult Process(TQuery query);
    }
}
