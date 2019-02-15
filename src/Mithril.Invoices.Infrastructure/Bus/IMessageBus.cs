using Mithril.Invoices.Domain.Core;
using System.Threading.Tasks;

namespace Mithril.Invoices.Infrastructure.Bus
{
    public interface IMessageBus<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
    {
        void Subscribe(ISubscriber<TAggregate, TId> subscriber);
        
        Task PublishAsync(TAggregate aggregate);
    }
}