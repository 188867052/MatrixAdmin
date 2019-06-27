using AutoMapper;
using Core.Api.Authentication;
using Core.Api.Framework.DependencyInjection;
using Core.Extension.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Framework
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
            SwaggerConfiguration.AddService(services);
            DbContextExtension.AddService(services, Configuration);
            CorsExtension.AddService(services);
            RouteExtension.AddService(services);
            RouteAnalyzerExtension.AddService(services);
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            try
            {
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
            SwaggerConfiguration.AddConfigure(app);
            AuthenticationConfiguration.AddConfigure(app);
            DevelopmentConfiguration.AddConfigure(app, env);

            app.UseStaticFiles();
            app.UseFileServer();
            CorsExtension.AddConfigure(app);
            ExceptionConfiguration.AddConfigure(app);

            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthenticationContextService.AddConfigure(httpContextAccessor);
            RouteExtension.AddConfigure(app);
            RouteAnalyzerExtension.AddConfigure(app);
        }
    }
}
