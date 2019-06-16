using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extension.RouteAnalyzer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Route.Generator
{
    public class RouteGenerator
    {
        private readonly ILogger _logger;

        public RouteGenerator(ILoggerFactory logger)
        {
            this._logger = logger.CreateLogger<RouteGenerator>();
        }

        public static string GenerateRoutes(string content)
        {
            IEnumerable<RouteInformation> infos = JsonConvert.DeserializeObject<IEnumerable<RouteInformation>>(content);
            StringBuilder sb = new StringBuilder();
            var group = infos.GroupBy(o => o.Namespace);
            for (int i = 0; i < group.Count(); i++)
            {
                sb.Append(GenerateNamespace(group.ElementAt(i), i == (group.Count() - 1)));
            }

            return sb.ToString();
        }

        public string GenerateCode(string workingDirectory)
        {
            Type type = workingDirectory.Contains("Core.Api") ? typeof(Core.Api.Framework.Startup) : typeof(Core.Mvc.Framework.Startup);
            var client = new TestSite(type).BuildClient();
            var response = client.GetAsync(Router.DefaultRoute).Result;
            Task<string> content = response.Content.ReadAsStringAsync();

            return GenerateRoutes(content.Result);
        }

        private static StringBuilder GenerateNamespace(IGrouping<string, RouteInformation> namespaceGroup, bool isLast)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {GetConvertedNamespace(namespaceGroup.Key)}");
            sb.AppendLine("{");

            var group = namespaceGroup.GroupBy(o => o.ControllerName);
            for (int i = 0; i < group.Count(); i++)
            {
                sb.Append(GenerateClass(group.ElementAt(i), i == (group.Count() - 1)));
            }

            sb.AppendLine("}");
            if (!isLast)
            {
                sb.AppendLine();
            }

            return sb;
        }

        private static StringBuilder GenerateClass(IGrouping<string, RouteInformation> group, bool isLast)
        {
            string classFullName = $"{group.First().Namespace}.{group.First().ControllerName}Controller";
            string crefNamespace = GetCrefNamespace(classFullName, GetConvertedNamespace(group.First().Namespace));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// <see cref=\"{crefNamespace}\"/>");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public class {group.Key}Route");
            sb.AppendLine("    {");
            for (int i = 0; i < group.Count(); i++)
            {
                var item = group.ElementAt(i);
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// <see cref=\"{crefNamespace}.{item.ActionName}\"/>");
                sb.AppendLine($"        /// </summary>");
                sb.AppendLine($"        public const string {item.ActionName} = \"{item.Path}\";");
                if (i != (group.Count() - 1))
                {
                    sb.AppendLine();
                }
            }

            sb.AppendLine("    }");
            if (!isLast)
            {
                sb.AppendLine();
            }

            return sb;
        }

        private static string GetConvertedNamespace(string name)
        {
            return name.Replace("Controllers", "Routes");
        }

        private static string GetCrefNamespace(string cref, string @namespace)
        {
            IList<string> sameString = new List<string>();
            var splitedNamespace = @namespace.Split('.');
            var splitedCref = cref.Split('.');
            int minLength = Math.Min(splitedNamespace.Length, splitedCref.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (splitedCref[i] == splitedNamespace[i])
                {
                    sameString.Add(splitedCref[i]);
                }
                else
                {
                    break;
                }
            }

            cref = cref.Substring(string.Join('.', sameString).Length + 1);
            return cref;
        }
    }
}