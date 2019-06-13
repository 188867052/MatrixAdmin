using Core.Api.Framework.MiddleWare;
using Microsoft.AspNetCore.Builder;

namespace Core.Mvc.Framework.StartupConfigurations
{
    public static class ExceptionConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));
        }
    }
}