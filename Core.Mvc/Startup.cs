using System.Text.Encodings.Web;
using System.Text.Unicode;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (true || env.IsDevelopment())
            {
                app.UseMyDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

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
    }
}
