using Core.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Builder;

namespace Core.Api.Framework.StartupConfigurations
{
    public static class ExceptionConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
