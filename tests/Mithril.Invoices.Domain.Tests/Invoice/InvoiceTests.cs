using FluentAssertions;
using Mithril.Invoices.Domain.Invoice;
using System;
using System.Linq;
using Xunit;

namespace Mithril.Invoices.Domain.Tests.Invoice
{
    public class InvoiceTests
    {
        [Fact]
        public void InvoiceCreationShouldGenerateInvoiceCreatedEvent()
        {
            // Act
            var invoice = new Domain.Invoice.Invoice();

            // Assert
            invoice.PendingEvents.Should().HaveCount(1);
            invoice.PendingEvents.First().GetType().Should().Be(typeof(InvoiceCreated));
        }

        [Fact]
        public void InvoiceCreatedShouldHaveAnId()
        {
            // Act
            var invoice = new Domain.Invoice.Invoice();

            // Assert
            invoice.Id.Should().NotBe(default(Guid));
        }
    }
}
