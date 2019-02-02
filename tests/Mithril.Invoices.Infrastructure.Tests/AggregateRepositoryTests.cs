using FluentAssertions;
using Mithril.Invoices.Domain.Invoice;
using System;
using Xunit;

namespace Mithril.Invoices.Infrastructure.Tests
{
    public class AggregateRepositoryTests
    {
        [Fact]
        public void CanSaveAndGetSavedAggregate()
        {
            // Arrange
            var aggregateRepository = new AggregateRepository<Invoice, Guid>(new EventStore());
            var invoice = new Invoice(Guid.NewGuid());

            // Act
            aggregateRepository.SaveAsync(invoice).Wait();
            var retrievedInvoice = aggregateRepository.GetByIdAsync(invoice.Id).Result;

            // Assert
            invoice.Id.Should().Be(retrievedInvoice.Id);
        }
    }
}
