using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Core.Api.Framework.Configurations
{
    public static class WebEncoderConfiguration
    {
        public static void AddService(IServiceCollection services)
        {
            services.Configure<WebEncoderOptions>(o => o.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs));
        }
    }
}