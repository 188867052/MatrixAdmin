using Core.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Linq;
using Core.Api.Framework;
using Core.Entity;
using AutoMapper;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [CustomAuthorize]
    public class TestController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public TestController(CoreContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// 测试 Authentication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TestAuthentication()
        {
            ClaimsPrincipal ClaimsPrincipal = this.HttpContext.User;
            ClaimsIdentity ClaimsIdentity = (ClaimsIdentity)ClaimsPrincipal.Identity;
            var Claims = JsonConvert.SerializeObject(ClaimsIdentity.Claims.Select(o => o.Value));
            return this.Ok(new { Claims, ClaimsPrincipal.Identity.IsAuthenticated });
        }
    }
}
