using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Api.Framework.DependencyInjection
{
    public static class SwaggerConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Api V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Core Api V2");
                c.RoutePrefix = "swagger";
            });
        }

        public static void AddService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Api V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Core Api V2", Version = "v2" });
            });
        }
    }
}