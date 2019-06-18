using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Extension.RouteAnalyzer;
using Newtonsoft.Json;

namespace Route.Generator
{
    public class RouteGenerator
    {
        private static string _cacheDictionaryClass;

        public static string GenerateRoutes(string content)
        {
            IEnumerable<RouteInfo> infos = JsonConvert.DeserializeObject<IEnumerable<RouteInfo>>(content);
            _cacheDictionaryClass = GenerateCacheDictionary(infos);

            StringBuilder sb = new StringBuilder();
            var group = infos.GroupBy(o => o.Namespace);
            sb.AppendLine($"using {typeof(Dictionary<int, int>).Namespace};");
            sb.AppendLine($"using {typeof(Task).Namespace};");
            sb.AppendLine($"using {typeof(ParameterInfo).Namespace};");
            sb.AppendLine();
            for (int i = 0; i < group.Count(); i++)
            {
                sb.Append(GenerateNamespace(group.ElementAt(i), i == (group.Count() - 1)));
            }

            return sb.ToString();
        }

        public async Task<string> GenerateCodeAsync(string workingDirectory)
        {
            Type type = workingDirectory.Contains("Core.Api") ? typeof(Core.Api.Framework.Startup) : typeof(Core.Mvc.Framework.Startup);
            var client = new TestSite(type).BuildClient();
            using (HttpResponseMessage response = await client.GetAsync(Router.DefaultRoute))
            {
                string content = await response.Content.ReadAsStringAsync();
                return GenerateRoutes(content);
            }
        }

        private static StringBuilder GenerateNamespace(IGrouping<string, RouteInfo> namespaceGroup, bool isLast)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {GetConvertedNamespace(namespaceGroup.Key)}");
            sb.AppendLine("{");
            if (!string.IsNullOrEmpty(_cacheDictionaryClass))
            {
                sb.AppendLine(_cacheDictionaryClass);
                _cacheDictionaryClass = string.Empty;
            }

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

        private static StringBuilder GenerateClass(IGrouping<string, RouteInfo> group, bool isLast)
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
                sb.AppendLine("        /// <summary>");
                sb.AppendLine($"        /// <see cref=\"{crefNamespace}.{item.ActionName}\"/>");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine($"        public const string {item.ActionName} = \"{item.Path}\";");

                // TODO
                if (item.Namespace.Contains("Core.Api"))
                {
                    sb.AppendLine($"        public static async Task<T> {item.ActionName}Async<T>({GeneraParameters(item.Parameters, true, false)})");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            return await {typeof(HttpClientAsync).FullName}.{nameof(HttpClientAsync.Async2)}<T>({item.ActionName}{GeneraParameters(item.Parameters, false, true)});");
                    sb.AppendLine("        }");
                }

                if (i != group.Count() - 1)
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
            var splitNamespace = @namespace.Split('.');
            var splitCref = cref.Split('.');
            int minLength = Math.Min(splitNamespace.Length, splitCref.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (splitCref[i] == splitNamespace[i])
                {
                    sameString.Add(splitCref[i]);
                }
                else
                {
                    break;
                }
            }

            cref = cref.Substring(string.Join('.', sameString).Length + 1);
            return cref;
        }

        private static string GenerateCacheDictionary(IEnumerable<RouteInfo> infos)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("    public class Cache");
            sb.AppendLine("    {");
            sb.AppendLine($"        public static Dictionary<string, {nameof(RouteInfo)}> Dictionary = new Dictionary<string, {nameof(RouteInfo)}>()");
            sb.AppendLine("        {");
            for (int i = 0; i < infos.Count(); i++)
            {
                var item = infos.ElementAt(i);
                sb.AppendLine($"            {{{GetConvertedNamespace(item.Namespace)}.{item.ControllerName}Route.{item.ActionName}, new {nameof(RouteInfo)}");
                sb.AppendLine("                {");
                sb.AppendLine($"                    {nameof(item.HttpMethod)} = \"{item.HttpMethod}\",");
                sb.Append(GenerateParameters(item.Parameters));
                sb.AppendLine("                }");
                sb.AppendLine("            },");
            }

            sb.AppendLine("        };");
            sb.AppendLine("    }");
            return sb.ToString();
        }

        private static string GenerateParameters(IList<ParameterInfo> parameters)
        {
            StringBuilder sb = new StringBuilder();
            if (parameters != null && parameters.Count > 0)
            {
                sb.AppendLine($"                    {nameof(RouteInfo.Parameters)} = new List<{nameof(ParameterInfo)}>");
                sb.AppendLine("                    {");
                foreach (var item in parameters)
                {
                    sb.AppendLine($"                        new {nameof(ParameterInfo)}() {{{nameof(item.Name)} = \"{item.Name}\", {nameof(item.Type)} = \"{item.Type}\"}},");
                }

                sb.AppendLine("                    }");
            }

            return sb.ToString();
        }

        private static string GeneraParameters(IList<ParameterInfo> parameters, bool hasType, bool hasPre)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> list = new List<string>();
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    if (hasType)
                    {
                        list.Add($"{item.Type} {item.Name}");
                    }
                    else
                    {
                        list.Add($"{item.Name}");
                    }
                }

                sb.Append((hasPre ? ", " : string.Empty) + string.Join(", ", list));
            }

            return sb.ToString();
        }
    }
}