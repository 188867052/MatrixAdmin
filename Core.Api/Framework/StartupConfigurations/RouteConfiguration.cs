using Core.Extension.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Framework.StartupConfigurations
{
    public static class RouteConfiguration
    {
        public const string Route = "/routes";

        /// <summary>
        /// The Configure must be the last of the Configure in Startup.Configure.
        /// </summary>
        /// <param name="app"></param>
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer(Route); // Add
                routes.MapRoute(
                     name: "areaRoute",
                     template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "apiDefault",
                    template: "api/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void AddService(IServiceCollection services)
        {
            services.Configure<RouteOptions>(o => o.LowercaseUrls = false);
        }
    }
}