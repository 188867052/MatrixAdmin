using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Model;
using Newtonsoft.Json;
using System.Net.Http;
using HttpMethodEnum = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;
using Core.Extension.RouteAnalyzer;
using System.Collections.Generic;

namespace Core.Mvc.Framework
{
    public static class HttpClientAsync
    {
        public static HttpClient CreateInstance()
        {
            return new HttpClient() { BaseAddress = new Uri(SiteConfiguration.Host) };
        }

        public static async Task<ResponseModel> Async<T>(string route, AuthenticationHeaderValue authorization, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters, data);
            using (HttpClient httpClient = CreateInstance())
            {
                httpClient.DefaultRequestHeaders.Authorization = authorization;
                Task<string> json = GetResponseAsync(httpClient, route, parameters, data);
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);
                model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

                return model;
            }
        }

        public static async Task<dynamic> Async(string route, AuthenticationHeaderValue authorization, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters, data);
            using (HttpClient httpClient = CreateInstance())
            {
                httpClient.DefaultRequestHeaders.Authorization = authorization;
                Task<string> json = GetResponseAsync(httpClient, route, parameters, data);
                dynamic model = JsonConvert.DeserializeObject<dynamic>(await json);

                return model;
            }
        }

        public static async Task<ResponseModel> Async<T>(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters, data);
            using (HttpClient httpClient = CreateInstance())
            {
                Task<string> json = GetResponseAsync(httpClient, route, parameters, data);
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);
                model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

                return model;
            }
        }

        public static async Task<ResponseModel> ResponseAsync(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters, data);
            using (HttpClient httpClient = CreateInstance())
            {
                Task<string> json = GetResponseAsync(httpClient, route, parameters, data);
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);

                return model;
            }
        }

        public static async Task<dynamic> Async(string route, params object[] data)
        {
            OnAsync(route, out IList<ParameterInfo> parameters, data);
            using (HttpClient httpClient = CreateInstance())
            {
                Task<string> json = GetResponseAsync(httpClient, route, parameters, data);
                var model = JsonConvert.DeserializeObject<dynamic>(await json);

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

        private static Task<string> GetResponseAsync(HttpClient httpClient, string route, IList<ParameterInfo> Parameters, params object[] data)
        {
            var httpMethod = Api.Framework.RouteController.GetHttpMethod(route);
            Task<string> json;
            switch (httpMethod)
            {
                case HttpMethodEnum.Get:
                    json = GetAsync(httpClient, route, Parameters, data);
                    break;
                case HttpMethodEnum.Post:
                    json = PostAsync(httpClient, route, data);
                    break;
                default:
                    throw new HttpRequestException($"Unsupported HttpMethod: {httpMethod}");
            }

            return json;
        }

        private static void OnAsync(string route, out IList<ParameterInfo> parameters, params object[] data)
        {
            parameters = Api.Framework.RouteController.GetParameterInfo(route);
            if (data.Length != parameters.Count)
            {
                throw new HttpRequestException($"Http Parameter Count Wrong. Request: {JsonConvert.SerializeObject(data)},Expected: {JsonConvert.SerializeObject(parameters)}");
            }
        }
    }
}
