using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extension.DependencyInjection
{
    public static class SetCompatibilityVersionExtension
    {
        public static void AddService(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}