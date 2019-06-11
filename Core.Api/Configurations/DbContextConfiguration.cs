using Core.Api.Framework.MiddleWare;
using Core.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Api.Configurations
{
    public static class DbContextConfiguration
    {
        public static void AddService(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<CoreContext>(options =>
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddProvider(new EntityFrameworkLoggerProvider());
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
            });
        }

        public static void AddConfigure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}