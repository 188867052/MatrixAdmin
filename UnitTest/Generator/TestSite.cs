using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

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
            string siteContentRoot = GetApplicationPath(Path.Combine("..", "..", "..", "..", "WebSites"));
            siteContentRoot = @"C:\Users\54215\Desktop\Study\Asp.Net\Core.Api";
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(siteContentRoot)
                .UseStartup(_startupType);

            return new TestServer(builder);
        }

        public HttpClient BuildClient()
        {
            var server = BuildServer();
            var client = server.CreateClient();
            client.BaseAddress = new Uri("http://localhost");

            return client;
        }

        private string GetApplicationPath(string relativePath)
        {
            var startupAssembly = _startupType.GetTypeInfo().Assembly;
            var applicationName = startupAssembly.GetName().Name;
            var applicationBasePath = System.AppContext.BaseDirectory;
            return Path.GetFullPath(Path.Combine(applicationBasePath, relativePath, applicationName));
        }
    }
}
