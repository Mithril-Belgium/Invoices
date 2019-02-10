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
}
