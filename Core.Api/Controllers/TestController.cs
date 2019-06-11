using Core.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [CustomAuthorize]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 测试日志
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Logger()
        {
            var a = this.HttpContext.User;
            bool isSuccess = !string.IsNullOrEmpty(a.Identity.Name);
            return this.Ok(isSuccess);
        }
    }
}
