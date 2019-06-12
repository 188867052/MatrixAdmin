using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Core.Mvc.Framework.Middleware
{
    public static class MyDeveloperExceptionPageExtensions
    {
        public static IApplicationBuilder UseMyDeveloperExceptionPage(
          this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<MyDeveloperExceptionPageMiddleware>();
        }

        public static IApplicationBuilder UseDeveloperExceptionPage(this IApplicationBuilder app, MyDeveloperExceptionPageOptions options)
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
