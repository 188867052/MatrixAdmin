using Microsoft.AspNetCore.Builder;

namespace Core.Api.Configurations
{
    public static class RouteConfiguration
    {
        /// <summary>
        /// The Configure must be the last of the Configure in Startup.Configure.
        /// </summary>
        /// <param name="app"></param>
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
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
    }
}