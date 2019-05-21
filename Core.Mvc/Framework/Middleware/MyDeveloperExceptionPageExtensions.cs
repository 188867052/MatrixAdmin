using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Core.Mvc.Framework.Middleware
{
    /// <summary>
    /// <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" /> extension methods for the <see cref="T:Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware" />.
    /// </summary>
    public static class MyDeveloperExceptionPageExtensions
    {
        /// <summary>
        /// Captures synchronous and asynchronous <see cref="T:System.Exception" /> instances from the pipeline and generates HTML error responses.
        /// </summary>
        /// <param name="app">The <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" />.</param>
        /// <returns>A reference to the <paramref name="app" /> after the operation has completed.</returns>
        public static IApplicationBuilder UseMyDeveloperExceptionPage(
          this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<MyDeveloperExceptionPageMiddleware>();
        }

        /// <summary>
        /// Captures synchronous and asynchronous <see cref="T:System.Exception" /> instances from the pipeline and generates HTML error responses.
        /// </summary>
        /// <param name="app">The <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" />.</param>
        /// <param name="options">A <see cref="T:Microsoft.AspNetCore.Builder.DeveloperExceptionPageOptions" /> that specifies options for the middleware.</param>
        /// <returns>A reference to the <paramref name="app" /> after the operation has completed.</returns>
        public static IApplicationBuilder UseDeveloperExceptionPage(
          this IApplicationBuilder app,
          MyDeveloperExceptionPageOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<MyDeveloperExceptionPageMiddleware>((object)Options.Create(options));
        }
    }
}
