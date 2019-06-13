using Core.Api.Framework.MiddleWare;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Api.Framework.StartupConfigurations
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
    }
}