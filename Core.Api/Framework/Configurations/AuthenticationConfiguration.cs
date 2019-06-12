using Core.Api.Auth;
using Core.Api.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Framework.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static void AddService(IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>();
            services.Configure<AppAuthenticationSettings>(appSettingsSection);
            services.AddJwtBearerAuthentication(appSettings);
        }

        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}