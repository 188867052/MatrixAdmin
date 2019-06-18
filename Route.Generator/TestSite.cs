using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Route.Generator
{
    public class TestSite
    {
        private readonly Type _startupType;

        public TestSite(Type startupType)
        {
            this._startupType = startupType;
        }

        public HttpClient BuildClient()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(AppContext.BaseDirectory)
                .UseStartup(this._startupType);

#pragma warning disable CA2000 // Dispose objects before losing scope
            TestServer server = new TestServer(builder);
#pragma warning restore CA2000 // Dispose objects before losing scope
            var client = server.CreateClient();
            client.BaseAddress = new Uri("http://localhost");

            return client;
        }
    }
}