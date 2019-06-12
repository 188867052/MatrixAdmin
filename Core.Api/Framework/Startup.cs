using AutoMapper;
using Core.Api.Authentication;
using Core.Api.Framework.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Core.Api
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
            AuthenticationConfiguration.AddService(services, this.Configuration);
            DbContextConfiguration.AddService(services, this.Configuration);
            CorsConfiguration.AddService(services);
            RouteConfiguration.AddService(services);
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
#pragma warning disable 618
            services.AddAutoMapper();
#pragma warning disable 618
            WebEncoderConfiguration.AddService(services);
            ValidateConfiguration.AddService(services);

            services.AddMvc().AddJsonOptions(s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            CorsConfiguration.AddConfigure(app);
            ExceptionConfiguration.AddConfigure(app);

            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthenticationContextService.AddConfigure(httpContextAccessor);
            RouteConfiguration.AddConfigure(app);
        }
    }
}
