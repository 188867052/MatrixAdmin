using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Extension.DependencyInjection
{

    public static class JsonExtension
    {
        public static void AddService(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions((options) =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        }
    }
}