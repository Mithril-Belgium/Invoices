using Mithril.Invoices.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mithril.Invoices.Application.InvoiceCreation
{
    public class InvoiceCreationCommand : ICommand
    {
        public Guid Id { get; }

        public InvoiceCreationCommand(Guid id)
        {
            Id = id;
        }
    }
}
