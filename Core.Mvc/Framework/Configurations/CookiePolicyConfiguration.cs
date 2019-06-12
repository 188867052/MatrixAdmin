using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mvc.Framework.Configurations
{
    public static class CookiePolicyConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseCookiePolicy();
        }

        public static void AddService(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
    }
}