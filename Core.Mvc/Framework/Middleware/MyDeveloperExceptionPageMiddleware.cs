using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Mvc.Framework.Middleware
{
    /// <summary>
    /// Captures synchronous and asynchronous exceptions from the pipeline and generates HTML error responses.
    /// </summary>
    public class MyDeveloperExceptionPageMiddleware : DeveloperExceptionPageMiddleware
    {
        public MyDeveloperExceptionPageMiddleware(
          RequestDelegate next,
          IOptions<MyDeveloperExceptionPageOptions> options,
          ILoggerFactory loggerFactory,
          IHostingEnvironment hostingEnvironment,
          DiagnosticSource diagnosticSource) : base(next, options, loggerFactory, hostingEnvironment, diagnosticSource)
        {
        }
    }
}
