using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Api.Routes;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Auth(string username, string password)
        {
            var data = await HttpClientAsync.Async(AuthenticationRoute.Auth, new { username, password });
            data.isSuccess = true;
            return this.Json(data);
        }
    }
}