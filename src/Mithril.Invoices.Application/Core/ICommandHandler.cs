using System;
using System.Collections.Generic;
using System.Text;

namespace Mithril.Invoices.Application.Core
{
    public interface ICommandHandler<TCommand> 
        where TCommand : ICommand
    {
        void Process(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        TResult Process(TCommand command);
    }
}
