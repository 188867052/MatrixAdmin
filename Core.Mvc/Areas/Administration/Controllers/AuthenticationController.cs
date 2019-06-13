using System.Threading.Tasks;
using Core.Extension;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.AuthenticationController;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Auth(string username, string password)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Auth));
            var data = await HttpClientAsync.GetAsync(url, parameters: new { username, password });
            data.isSuccess = true;
            return this.Json(data);
        }
    }
}