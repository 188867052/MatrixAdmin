using System.Net.Http;
using System.Threading.Tasks;
using Core.Models.Models.Response;
using Newtonsoft.Json;

namespace Core.Extension
{
    public static class AsyncRequest
    {
        public static async Task<ResponseModel> GetAsync<T>(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponse = await client.GetAsync("https://localhost:44377" + url);
            Task<string> json = httpResponse.Content.ReadAsStringAsync();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(json.Result);
            model.Data = JsonConvert.DeserializeObject<T>(model.Data.ToString());
            return model;
        }
    }
}
