using AutoMapper;
using Core.Mvc.Authentication;
using Core.Mvc.Framework.StartupConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Extension.DependencyInjection;

namespace Core.Mvc.Framework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            DbContextConfiguration.AddService(services, Configuration);
            CorsConfiguration.AddService(services);
            RouteConfiguration.AddService(services);
            RouteAnalyzerExtension.AddService(services);
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            try
            {
                // TODO: when use code gen, a exception will occurred.
#pragma warning disable 618
                AuthenticationConfiguration.AddService(services, Configuration);
                services.AddAutoMapper();
#pragma warning disable 618
            }
            catch
            {
            }

            WebEncoderConfiguration.AddService(services);
            ValidateConfiguration.AddService(services);
            JsonExtension.AddService(services);
            SetCompatibilityVersionExtension.AddService(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AuthenticationConfiguration.AddConfigure(app);
            DevelopmentConfiguration.AddConfigure(app, env);

            app.UseStaticFiles();
            app.UseFileServer();
            CorsConfiguration.AddConfigure(app);
            ExceptionConfiguration.AddConfigure(app);

            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthenticationContextService.AddConfigure(httpContextAccessor);
            RouteConfiguration.AddConfigure(app);
            RouteAnalyzerExtension.AddConfigure(app);
        }
    }
}
