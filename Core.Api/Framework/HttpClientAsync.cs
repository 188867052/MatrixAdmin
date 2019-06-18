using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Model;
using Newtonsoft.Json;
using System.Net.Http;
using Core.Extension.RouteAnalyzer;
using System.Collections.Generic;
using Core.Api.Routes;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Api.Framework
{
    public static class HttpClientAsync
    {
        public static HttpClient CreateInstance()
        {
            return new HttpClient() { BaseAddress = new Uri(SiteConfiguration.Host) };
        }

        public static async Task<HttpResponseModel> Async<TData>(string route, AuthenticationHeaderValue authorization, params object[] data)
        {
            using (HttpClient httpClient = CreateInstance())
            {
                httpClient.DefaultRequestHeaders.Authorization = authorization;
                string json = await ExcuteAsync(httpClient, route, data);
                HttpResponseModel model = JsonConvert.DeserializeObject<HttpResponseModel>(json);
                model.Data = JsonConvert.DeserializeObject<TData>(model.Data.ToString());

                return model;
            }
        }

        public static async Task<HttpResponseModel> Async<TData>(string route, params object[] data)
        {
            HttpResponseModel model = await Async2<HttpResponseModel>(route, data);
            model.Data = JsonConvert.DeserializeObject<TData>(model.Data.ToString());

            return model;
        }

        public static async Task<dynamic> Async(string route, AuthenticationHeaderValue authorization, params object[] data)
        {
            using (HttpClient httpClient = CreateInstance())
            {
                httpClient.DefaultRequestHeaders.Authorization = authorization;
                string json = await ExcuteAsync(httpClient, route, data);
                dynamic model = JsonConvert.DeserializeObject<dynamic>(json);

                return model;
            }
        }

        public static async Task<T> Async2<T>(string route, params object[] data)
        {
            using (HttpClient httpClient = CreateInstance())
            {
                string json = await ExcuteAsync(httpClient, route, data);
                T model = JsonConvert.DeserializeObject<T>(json);

                return model;
            }
        }

        public static async Task<dynamic> Async(string route, params object[] data)
        {
            using (HttpClient httpClient = CreateInstance())
            {
                string json = await ExcuteAsync(httpClient, route, data);
                var model = JsonConvert.DeserializeObject<dynamic>(json);

                return model;
            }
        }

        private static async Task<string> PostAsync(HttpClient httpClient, string route, params object[] data)
        {
            var content = JsonConvert.SerializeObject(data[0]);
            using (StringContent httpContent = new StringContent(content))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (HttpResponseMessage httpResponse = await httpClient.PostAsync(route, httpContent))
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
        }

        private static async Task<string> GetAsync(HttpClient httpClient, string relativeUri, params object[] data)
        {
            string url = GenerateUrl(relativeUri, data);
            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(url))
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
        }

        private static string GenerateUrl(string relativeUri, params object[] data)
        {
            string url = relativeUri;
            IList<ParameterInfo> parameterInfos = Cache.Dictionary[relativeUri].Parameters;

            // Attribute
            if (relativeUri.Contains("{") && relativeUri.Contains("}"))
            {
                parameterInfos[0].Value = data[0].ToString();
                url = Regex.Replace(relativeUri, "{.+}", data[0].ToString());
                return url;
            }
            else if (data != null && data.Length > 0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    var obj = data[i].ToString();
                    if (obj.Contains("{") && obj.Contains("}") && obj.Contains("="))
                    {
                        var keyPare = obj.Split(',');
                        foreach (var item in keyPare)
                        {
                            string key = item.Split('=')[0].Trim('{').Trim();
                            string value = item.Split('=')[1].Trim('}').Trim();
                            parameterInfos.FirstOrDefault(o => o.Name == key).Value = value;
                        }
                    }
                    else
                    {
                        parameterInfos[i].Value = data[i].ToString();
                    }
                }
            }

            url = relativeUri
                + (parameterInfos.Count > 0 ? "?" : "")
                + string.Join("&", parameterInfos.Select(o => $"{o.Name}={o.Value}"));

            return url;
        }

        private static Task<string> ExcuteAsync(HttpClient httpClient, string route, params object[] data)
        {
            var httpMethod = Cache.Dictionary[route].HttpMethod;
            Task<string> json;
            switch (httpMethod.ToUpper())
            {
                case "GET":
                    json = GetAsync(httpClient, route, data);
                    break;
                case "POST":
                    json = PostAsync(httpClient, route, data);
                    break;
                default:
                    throw new HttpRequestException($"Unsupported Http Method: {httpMethod}");
            }

            return json;
        }
    }
}
