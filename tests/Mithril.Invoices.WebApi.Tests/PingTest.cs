using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using RestSharp;
using System;
using System.Net;
using Xunit;

namespace Mithril.Invoices.WebApi.Tests
{
    public class PingTest : EndToEndTest
    {

        public PingTest() : base(50001)
        {
        }

        [Fact]
        public void PingShouldPong()
        {
            var client = new RestClient(BaseUrl);

            var request = new RestRequest("api/ping", Method.GET);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().Be("\"pong\"");
        }
    }
}
