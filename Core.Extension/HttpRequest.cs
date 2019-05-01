using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Model;
using Newtonsoft.Json;

namespace Core.Extension
{
    public static class AsyncRequest
    {
        private static readonly string Host = "https://localhost:44377/api";

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<ResponseModel> GetAsync<T>(Url url)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                httpResponse = await client.GetAsync(Host + url.Render());
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

            return model;
        }

        public static async Task<ResponseModel> GetAsync<T>(Url url, string guid)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string value = "d7f32600-64c3-484d-a933-2d4a62bda0bc";
                httpResponse = await client.GetAsync($"{Host}{url.Render()}?{nameof(guid)}={value}");
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

            return model;
        }

        /// <summary>
        /// PostAsync
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TPostModel"></typeparam>
        /// <param name="url"></param>
        /// <param name="postModel"></param>
        /// <returns></returns>
        public static async Task<ResponseModel> PostAsync<TModel, TPostModel>(Url url, TPostModel postModel)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string postPara = JsonConvert.SerializeObject(postModel);
                StringContent httpContent = new StringContent(postPara);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(Host + url.Render(), httpContent);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<TModel>(model.Data.ToString());

            return model;
        }

        public static async void SubmitAsync<TPostModel>(Url url, TPostModel postModel)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string postPara = JsonConvert.SerializeObject(postModel);
                StringContent httpContent = new StringContent(postPara);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(Host + url.Render(), httpContent);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
        }
    }
}
