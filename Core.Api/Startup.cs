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
using System.ComponentModel;
using AutoMapper;
using ConsoleApp.DataModels;

namespace Core.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("AppSettings.json").Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region 跨域
            string[] urls = { "http://localhost:90" };
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain",
        builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
            );
            #endregion

            this.AddSwaggerGen(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services
                .AddMvc(config =>
                {
                    //config.Filters.Add(new ValidateModelAttribute());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();
            services.AddDbContext<CoreApiContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            // 如果使用SQL Server 2008数据库，请添加UseRowNumberForPaging的选项
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),b=>b.UseRowNumberForPaging())
            );
            //ApplicationContainer = this.AutofacRegister(services);

            //return new AutofacServiceProvider(ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            //app.UseMiddleware<CustomErrorMessageDelegatingHandler.ApiKeyMiddleware>();
            //app.UseMiddleware<CustomErrorMessageDelegatingHandler>();
            this.SwaggerBuilder(app);
            app.UseCors("AllowSameDomain");

            app.UseStaticFiles().UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvcWithDefaultRoute();
            //app.ConfigureCustomExceptionMiddleware();
            //Mappings.RegisterMappings();
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

        //private IContainer AutofacRegister(IServiceCollection services)
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterAssemblyTypes(Assembly.Load("Core.Repository")).Where(m => typeof(IDependency).IsAssignableFrom(m) && m != typeof(IDependency)).AsImplementedInterfaces().InstancePerLifetimeScope();
        //    builder.RegisterAssemblyTypes(Assembly.Load("Core.Service")).Where(m => typeof(IDependency).IsAssignableFrom(m) && m != typeof(IDependency)).AsImplementedInterfaces().InstancePerLifetimeScope();
        //    builder.Populate(services);

        //    return builder.Build();
        //}
    }
}