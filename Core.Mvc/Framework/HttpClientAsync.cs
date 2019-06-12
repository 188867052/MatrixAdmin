using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model;
using Newtonsoft.Json;

namespace Core.Mvc.Framework
{
    public static class HttpClientAsync
    {
        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="url">url.</param>
        /// <returns>Task.</returns>
        public static async Task<ResponseModel> GetAsync<T>(Url url, AuthenticationHeaderValue authorization = null)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Authorization = authorization;
                }

                httpResponse = await client.GetAsync(SiteConfiguration.Host + url.Render());
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

            return model;
        }

        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="url">url.</param>
        /// <returns>Task.</returns>
        public static async Task<dynamic> GetAsync(Url url, AuthenticationHeaderValue authorization = null, object parameters = null)
        {
            var httpResponse = await GetResponseAsync(url, authorization, parameters);
            string json = await httpResponse.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject<dynamic>(json);

            return data;
        }

        public static async Task<HttpResponseMessage> GetResponseAsync(Url url, AuthenticationHeaderValue authorization = null, object parameters = null)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Authorization = authorization;
                }

                var query = parameters == null ? string.Empty : url.Query(parameters);
                string requestUrl = SiteConfiguration.Host + url.Render() + query;
                return httpResponse = await client.GetAsync(requestUrl);
            }
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
                string requestUrl = SiteConfiguration.Host + url.Render();
                if (data != null)
                {
                    requestUrl += $"?{url.ActionParameterName[0]}=" + data;
                }

                httpResponse = await client.GetAsync(requestUrl);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

            return model;
        }

        /// <summary>
        /// DeleteAsync.
        /// </summary>
        /// <param name="url">url.</param>
        /// <param name="data">data.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<ResponseModel> DeleteAsync(Url url, object data = null)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = SiteConfiguration.Host + url.Render();
                if (data != null)
                {
                    requestUrl += $"?{url.ActionParameterName[0]}=" + data;
                }

                httpResponse = await client.GetAsync(requestUrl);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);

            return model;
        }

        /// <summary>
        /// PostAsync.
        /// </summary>
        /// <typeparam name="TModel">TModel.</typeparam>
        /// <typeparam name="TPostModel">TPostModel.</typeparam>
        /// <param name="url">url.</param>
        /// <param name="postModel">postModel.</param>
        /// <returns>ResponseModel.</returns>
        public static async Task<ResponseModel> PostAsync<TModel, TPostModel>(Url url, TPostModel postModel)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = new HttpClient())
            {
                string postData = JsonConvert.SerializeObject(postModel);
                StringContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(SiteConfiguration.Host + url.Render(), httpContent);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);
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
                httpResponse = await client.PostAsync(SiteConfiguration.Host + url.Render(), httpContent);
            }

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(await json);

            return model;
        }
    }
}
