using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Model.ResponseModels;
using Newtonsoft.Json;

namespace Core.Extension
{
    public static class AsyncRequest
    {
        private static string host = "https://localhost:44377";
        public static async Task<ResponseModel> GetAsync<T>(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponse = await client.GetAsync(host + url);
            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());

            return model;
        }


        public static async Task<ResponseModel> PostAsync<TModel, TPostModel>(string url, TPostModel postModel)
        {
            HttpClient client = new HttpClient();
            string postPara = JsonConvert.SerializeObject(postModel);
            StringContent httpContent = new StringContent(postPara);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = await client.PostAsync(host + url, httpContent);

            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<TModel>(model.Data.ToString());

            return model;
        }
    }
}
