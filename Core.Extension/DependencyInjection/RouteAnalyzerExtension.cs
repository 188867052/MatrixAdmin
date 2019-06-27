using Core.Extension.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extension.DependencyInjection
{
    public static class RouteAnalyzerExtension
    {
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.Routes.Add(new Router(routes.DefaultHandler, Router.DefaultRoute));
            });
        }

        public static void AddService(IServiceCollection services)
        {
            services.AddSingleton<IRouteAnalyzer, RouteAnalyzer.RouteAnalyzer>();
        }
    }
}