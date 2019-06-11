using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Configurations
{
    /// <summary>
    /// The cross-origin resource sharing configuration.
    /// </summary>
    public static class CorsConfiguration
    {
        public static void AddService(IServiceCollection services)
        {
            string[] urls = { "http://localhost:90" };
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain", builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));

            //services.AddCors(o =>
            //   o.AddPolicy("*",
            //       builder => builder
            //           .AllowAnyHeader()
            //           .AllowAnyMethod()
            //           .AllowAnyOrigin()
            //           .AllowCredentials()
            //   ));
        }

        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseCors("AllowSameDomain");
            //app.UseCors("*");
        }
    }
}