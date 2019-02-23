using EventStore.ClientAPI;
using FluentAssertions;
using Mithril.Invoices.Domain.Invoice;
using Mithril.Invoices.Infrastructure.Bus;
using Moq;
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
            var eventStore = new EventStore(EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113")));
            var aggregateRepository = new AggregateRepository<Invoice, Guid>(eventStore, 
                new Mock<IMessageBus<Invoice, Guid>>().Object);
            var invoice = new Invoice(Guid.NewGuid());

            // Act
            aggregateRepository.SaveAsync(invoice).Wait();
            var retrievedInvoice = aggregateRepository.GetByIdAsync(invoice.Id).Result;

            // Assert
            invoice.Id.Should().Be(retrievedInvoice.Id);
        }
    }
}
