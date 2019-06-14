using System.Text.Encodings.Web;
using System.Text.Unicode;
using Core.Api.Framework.StartupConfigurations;
using Core.Api.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Core.Mvc.Framework.StartupConfigurations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CookiePolicyConfiguration.AddService(services);
            CorsConfiguration.AddService(services);
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            RouteConfiguration.AddService(services);
            services.AddRouteAnalyzer();
            services.AddMvc().AddJsonOptions(s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DevelopmentConfiguration.AddConfigure(app, env);
            CorsConfiguration.AddConfigure(app);
            ExceptionConfiguration.AddConfigure(app);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            CookiePolicyConfiguration.AddConfigure(app);
            RouteConfiguration.AddConfigure(app);
        }
    }
}
