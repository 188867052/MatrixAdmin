using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Core.Api.Framework.DependencyInjection
{
    public static class DevelopmentConfiguration
    {
        public static void AddConfigure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}