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
        /// <summary>
        /// Initializes a new instance of the <see cref="MyDeveloperExceptionPageMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="diagnosticSource"></param>
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
