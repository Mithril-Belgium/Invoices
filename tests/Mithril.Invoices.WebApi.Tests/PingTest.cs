using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using RestSharp;
using System;
using System.Net;
using Xunit;

namespace Mithril.Invoices.WebApi.Tests
{
    public class PingTest : IDisposable
    {
        private readonly IWebHost _webHost;

        public PingTest()
        {
            _webHost = Program.CreateWebHostBuilder(new string[0])
                .Start("http://localhost:5001");
        }

        [Fact]
        public void PingShouldPong()
        {
            var client = new RestClient("http://localhost:5001");

            var request = new RestRequest("api/ping", Method.GET);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().Be("\"pong\"");
        }


        public void Dispose()
        {
            _webHost.StopAsync().Wait();
            _webHost.Dispose();
        }
    }
}
