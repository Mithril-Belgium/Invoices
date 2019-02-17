using Mithril.Invoices.Domain.Core;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure.Bus
{
    public interface ISubscriber<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
    {
        Task NotifyAsync(TAggregate aggregate);
    }
}
