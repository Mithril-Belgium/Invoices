using FluentAssertions;
using Mithril.Invoices.Domain.Invoice;
using Mithril.Invoices.Domain.Invoice.Events;
using System;
using System.Linq;
using Xunit;

namespace Mithril.Invoices.Domain.Tests.Invoice
{
    public class InvoiceTests
    {
        [Fact]
        public void InvoiceCreationShouldGenerateCorrespondingEvent()
        {
            // Act
            var invoice = new Domain.Invoice.Invoice(Guid.NewGuid());

            // Assert
            invoice.PendingEvents.Should().HaveCount(1);
            invoice.PendingEvents.First().GetType().Should().Be(typeof(InvoiceCreated));
        }

        [Fact]
        public void InvoiceCreatedShouldHaveAnId()
        {
            // Act
            var invoice = new Domain.Invoice.Invoice(Guid.NewGuid());

            // Assert
            invoice.Id.Should().NotBe(default(Guid));
        }

        [Fact]
        public void AddInvoiceLineShouldGenerateCorrespondingEvent()
        {
            // Arrange
            var invoice = new Domain.Invoice.Invoice(Guid.NewGuid());
            invoice.ClearPendingEvents();

            // Act 
            invoice.AddInvoiceLine("Hello");

            // Assert
            invoice.PendingEvents.Should().HaveCount(1);
            invoice.PendingEvents.First().Should().BeOfType<InvoiceLineAdded>();
        }

        [Fact]
        public void AddInvoiceLineShouldAppear()
        {
            // Arrange
            var invoice = new Domain.Invoice.Invoice(Guid.NewGuid());
            invoice.ClearPendingEvents();

            // Act 
            invoice.AddInvoiceLine("Hello");

            // Assert
            invoice.InvoiceLines.Should().HaveCount(1);
            invoice.InvoiceLines.First().Should().Be(new InvoiceLine(1, "Hello"));
        }
    }
}
