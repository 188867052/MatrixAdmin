using Core.Mvc.CustomException;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mvc.Framework.StartupConfigurations
{
    public static class ValidateConfiguration
    {
        public static void AddService(IServiceCollection services)
        {
            services.AddMvc(s => s.Filters.Add(new ValidateModelAttribute()));
        }
    }
}