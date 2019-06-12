using Core.Mvc.Framework.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Configurations
{
    public static class DevelopmentConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app, IHostingEnvironment env)
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
        }
    }
}