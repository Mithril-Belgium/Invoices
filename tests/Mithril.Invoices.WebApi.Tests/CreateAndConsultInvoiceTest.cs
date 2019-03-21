using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using Xunit;

namespace Mithril.Invoices.WebApi.Tests
{
    public class CreateAndConsultInvoiceTest : EndToEndTest
    {

        public CreateAndConsultInvoiceTest() : base(50002)
        {
        }

        [Fact]
        public void InvoiceCreatedShouldBeConsultable()
        {
            // Arrange
            var client = new RestClient(BaseUrl);

            // Act
            var creationRequest = new RestRequest("api/invoices", Method.POST);
            IRestResponse creationResponse = client.Execute(creationRequest);

            // Assert
            creationResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var rawInvoiceId = new string(creationResponse.Content.Where(c => c != '"').ToArray());
            Guid.TryParse(rawInvoiceId, out var invoiceId).Should().BeTrue();

            // Act
            var consultRequest = new RestRequest($"api/invoices/{invoiceId}", Method.GET);
            IRestResponse consultResponse = client.Execute(consultRequest);

            // Assert
            consultResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
