using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Core.UnitTest.CodeGenerator
{
    public class TestSite
    {
        private readonly Type _startupType;

        public TestSite(Type startupType)
        {
            this._startupType = startupType;
        }

        public TestServer BuildServer()
        {
            string siteContentRoot = this.GetApplicationPath();
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(siteContentRoot)
                .UseStartup(this._startupType);

            return new TestServer(builder);
        }

        public HttpClient BuildClient()
        {
            TestServer server = this.BuildServer();
            var client = server.CreateClient();
            client.BaseAddress = new Uri("http://localhost");

            return client;
        }

        private string GetApplicationPath()
        {
            var startupAssembly = this._startupType.GetTypeInfo().Assembly;
            var applicationName = startupAssembly.GetName().Name;
            var applicationBasePath = AppContext.BaseDirectory;
            string basePath = applicationBasePath.Split("UnitTest")[0];
            return Path.Combine(basePath, applicationName);
        }
    }
}
