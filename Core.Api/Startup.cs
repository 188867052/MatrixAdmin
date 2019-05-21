using System.ComponentModel;
using AutoMapper;
using Core.Api.Framework.MiddleWare;
using Core.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Core.Api
{
    /// <summary>
    /// The startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public Startup(IHostingEnvironment environment)
        {
            this.Configuration = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("AppSettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            string[] urls = { "http://localhost:90" };
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain", builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));

            this.AddSwaggerGen(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc(config =>
            {
            }).AddJsonOptions(options =>
           {
               options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
           })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#pragma warning disable 618
            services.AddAutoMapper();
#pragma warning disable 618
            services.AddDbContext<CoreApiContext>(options =>
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddProvider(new EntityFrameworkLoggerProvider());
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")).UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="environment">environment.</param>
        /// <param name="loggerFactory">loggerFactory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            this.SwaggerBuilder(app);
            app.UseCors("AllowSameDomain");

            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));
            app.UseStaticFiles().UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvcWithDefaultRoute();
        }

        private void SwaggerBuilder(IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Api V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Core Api V2");
                c.RoutePrefix = "swagger";
            });
        }

        private void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Api V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Core Api V2", Version = "v2" });
            });
        }
    }
}