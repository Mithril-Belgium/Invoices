using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mithril.Invoices.WebApi.Tests
{
    public abstract class EndToEndTest : IDisposable
    {
        private readonly IWebHost _webHost;
        private readonly string _baseUrl;

        protected string BaseUrl => _baseUrl;

        protected EndToEndTest(int port)
        {
            _baseUrl = $"http://localhost:{port}";
            _webHost = Program.CreateWebHostBuilder(new string[0])
                .Start(_baseUrl);
        }

        public void Dispose()
        {
            _webHost.StopAsync().Wait();
            _webHost.Dispose();
        }
    }
}
