using Core.Mvc.CustomException;
using Microsoft.AspNetCore.Builder;

namespace Core.Mvc.Framework.StartupConfigurations
{
    public static class ExceptionConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
