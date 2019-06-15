using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extension.RouteAnalyzer;
using Core.Mvc.Framework;
using Core.Mvc.Framework.StartupConfigurations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Route.Generator
{
    public class ModelGenerator
    {
        private readonly ILogger _logger;

        public ModelGenerator(ILoggerFactory logger)
        {
            this._logger = logger.CreateLogger<ModelGenerator>();
        }

        public static string GetRoutesGenerated(string content)
        {
            IEnumerable<RouteInformation> infos = JsonConvert.DeserializeObject<IEnumerable<RouteInformation>>(content);
            StringBuilder sb = new StringBuilder();
            foreach (IGrouping<string, RouteInformation> group in infos.GroupBy(o => o.ControllerName))
            {
                sb.AppendLine($"namespace {group.First().Namespace.Replace("Controllers", "Routes")}");
                sb.AppendLine("{");
                sb.AppendLine($"    public class {group.First().ControllerName}Route");
                sb.AppendLine("    {");
                foreach (RouteInformation item in group)
                {
                    sb.AppendLine($"        public const string {item.ActionName} = \"{item.Path}\";");
                }

                sb.AppendLine("    }");
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        public string GenerateCode()
        {
            var client = new TestSite(typeof(Core.Api.Framework.Startup)).BuildClient();
            var response = client.GetAsync(RouteConfiguration.Route).Result;
            Task<string> content = response.Content.ReadAsStringAsync();

            return GetRoutesGenerated(content.Result);
        }
    }
}