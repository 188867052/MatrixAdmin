using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model;
using Newtonsoft.Json;

namespace Core.Mvc
{
    public static class HttpClientAsync
    {
        private static readonly string Host = "https://localhost:44377/api";

        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="url">url.</param>
        /// <returns>Task.</returns>
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

        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="url">url.</param>
        /// <param name="data">data.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<ResponseModel> GetAsync<T>(Url url, object data)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = Host + url.Render();
                if (data != null)
                {
                    requestUrl += $"?{url.ActionParameterName}=" + data;
                }

                httpResponse = await client.GetAsync(requestUrl);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

            return model;
        }

        /// <summary>
        /// DeleteAsync.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<ResponseModel> DeleteAsync(Url url, object data = null)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = Host + url.Render();
                if (data != null)
                {
                    requestUrl += $"?{url.ActionParameterName}=" + data;
                }

                httpResponse = await client.GetAsync(requestUrl);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);

            return model;
        }

        /// <summary>
        /// PostAsync.
        /// </summary>
        /// <typeparam name="TModel">TModel.</typeparam>
        /// <typeparam name="TPostModel">TPostModel.</typeparam>
        /// <param name="url"></param>
        /// <param name="postModel"></param>
        /// <returns></returns>
        public static async Task<ResponseModel> PostAsync<TModel, TPostModel>(Url url, TPostModel postModel)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string postData = JsonConvert.SerializeObject(postModel);
                StringContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(Host + url.Render(), httpContent);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<TModel>(model.Data.ToString());

            return model;
        }

        /// <summary>
        /// SubmitAsync.
        /// </summary>
        /// <typeparam name="TPostModel">TPostModel.</typeparam>
        /// <param name="url">url.</param>
        /// <param name="postModel">postModel.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<ResponseModel> SubmitAsync<TPostModel>(Url url, TPostModel postModel)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string postData = JsonConvert.SerializeObject(postModel);
                StringContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(Host + url.Render(), httpContent);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);

            return model;
        }
    }
}
