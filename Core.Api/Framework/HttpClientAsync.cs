using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Model;
using Newtonsoft.Json;
using System.Net.Http;
using Core.Extension.RouteAnalyzer;
using System.Collections.Generic;
using Core.Api.Routes;

namespace Core.Api.Framework
{
    public static class HttpClientAsync
    {
        public static HttpClient CreateInstance()
        {
            return new HttpClient() { BaseAddress = new Uri(SiteConfiguration.Host) };
        }

        public static async Task<ResponseModel> Async<T>(string route, AuthenticationHeaderValue authorization, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters);
            using (HttpClient httpClient = CreateInstance())
            {
                httpClient.DefaultRequestHeaders.Authorization = authorization;
                string json = await ExcuteAsync(httpClient, route, parameters, data);
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json);
                model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

                return model;
            }
        }

        public static async Task<dynamic> Async(string route, AuthenticationHeaderValue authorization, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters);
            using (HttpClient httpClient = CreateInstance())
            {
                httpClient.DefaultRequestHeaders.Authorization = authorization;
                string json = await ExcuteAsync(httpClient, route, parameters, data);
                dynamic model = JsonConvert.DeserializeObject<dynamic>(json);

                return model;
            }
        }

        public static async Task<T> Async2<T>(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters);
            using (HttpClient httpClient = CreateInstance())
            {
                string json = await ExcuteAsync(httpClient, route, parameters, data);
                T model = JsonConvert.DeserializeObject<T>(json);

                return model;
            }
        }

        public static async Task<ResponseModel> Async<T>(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters);
            using (HttpClient httpClient = CreateInstance())
            {
                string json = await ExcuteAsync(httpClient, route, parameters, data);
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json);
                model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

                return model;
            }
        }

        public static async Task<ResponseModel> ResponseAsync(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters);
            using (HttpClient httpClient = CreateInstance())
            {
                string json = await ExcuteAsync(httpClient, route, parameters, data);
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json);

                return model;
            }
        }

        public static async Task<dynamic> Async(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters);
            using (HttpClient httpClient = CreateInstance())
            {
                string json = await ExcuteAsync(httpClient, route, parameters, data);
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

        private static async Task<string> GetAsync(HttpClient httpClient, string relativeUri, IList<ParameterInfo> parameterInfos, params object[] data)
        {
            if (relativeUri.Contains("{") && relativeUri.Contains("}"))
            {
                relativeUri = relativeUri.Replace($"{{{parameterInfos[0].Name}}}", $"{data[0]}");
            }
            else if (data != null && data.Length > 0)
            {
                relativeUri += "?";
                for (int i = 0; i < data.Length; i++)
                {
                    relativeUri += $"{parameterInfos[i].Name}={data[i]}";
                    if (i != data.Length - 1)
                    {
                        relativeUri += "&";
                    }
                }
            }
            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(relativeUri))
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
        }

        private static Task<string> ExcuteAsync(HttpClient httpClient, string route, IList<ParameterInfo> Parameters, params object[] data)
        {
            var httpMethod = Cache.Dictionary[route].HttpMethod;
            Task<string> json;
            switch (httpMethod.ToUpper())
            {
                case "GET":
                    json = GetAsync(httpClient, route, Parameters, data);
                    break;
                case "POST":
                    json = PostAsync(httpClient, route, data);
                    break;
                default:
                    throw new HttpRequestException($"Unsupported Http Method: {httpMethod}");
            }

            return json;
        }

        private static void OnAsync(string route, out IList<ParameterInfo> parameters)
        {
            parameters = Cache.Dictionary[route].Parameters;
        }
    }
}
