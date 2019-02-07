using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mithril.Invoices.Application.Core
{
    public interface ICommandHandler<TCommand> 
        where TCommand : ICommand
    {
        Task ProcessAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        Task<TResult> ProcessAsync(TCommand command);
    }
}
