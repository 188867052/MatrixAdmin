using Core.Mvc.Areas.Redirect.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mvc.Framework.Configurations
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
                string defaultController = nameof(RedirectController).Replace(nameof(Controller), string.Empty);
                string defaultAction = nameof(RedirectController.Index);
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area:exists}/{controller=Redirect}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=" + defaultController + "}/{action=" + defaultAction + "}/{id?}");
            });
        }

        public static void AddService(IServiceCollection services)
        {
            services.Configure<RouteOptions>(o => o.LowercaseUrls = false);
        }
    }
}