using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mvc.Framework.StartupConfigurations
{
    public static class CookiePolicyConfiguration
    {
        public static bool DisableCookie { get; set; }

        public static void AddConfigure(IApplicationBuilder app)
        {
            if (DisableCookie)
            {
                // AspNetCore2.1 supports the GDPR specification introduced on May 25, 2018,
                // which considers cookies to be private data of users.If they are to be used,
                // they must obtain user consent.
                app.UseCookiePolicy();
            }
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