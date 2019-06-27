using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Framework.DependencyInjection
{
    /// <summary>
    /// The cross-origin resource sharing configuration.
    /// </summary>
    public static class CorsExtension
    {
        public static void AddService(IServiceCollection services)
        {
            services.AddCors(o =>
               o.AddPolicy("*",
                   builder => builder
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials()
               ));
        }

        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseCors("*");
        }
    }
}