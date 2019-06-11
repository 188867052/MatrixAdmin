using System.Linq;
using System.Net;
using System.Security.Claims;
using Core.Api.Authentication;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Core.Api.Controllers
{
    /// <summary>
    /// OauthController.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly CoreContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OauthController"/> class.
        /// </summary>
        /// <param name="appSettings">appSettings.</param>
        /// <param name="dbContext">The dbContext.</param>
        public OauthController(IOptions<AppAuthenticationSettings> appSettings, CoreContext dbContext)
        {
            this._appSettings = appSettings.Value;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Auth.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="password">password.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Auth(string username, string password)
        {
            using (this._dbContext)
            {
                User user = this._dbContext.User.FirstOrDefault(x => x.LoginName == username.Trim());
                if (user == null || !user.IsEnable)
                {
                    return this.FailResponse("用户不存在");
                }

                if (user.Password != password.Trim())
                {
                    return this.FailResponse("密码不正确");
                }

                if (user.IsLocked)
                {
                    return this.FailResponse("账号已被锁定");
                }

                if (user.IsEnable)
                {
                    return this.FailResponse("账号已被禁用");
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("guid", user.Id.ToString()),
                    new Claim("avatar", string.Empty),
                    new Claim("displayName", user.DisplayName),
                    new Claim("loginName", user.LoginName),
                    new Claim("emailAddress", string.Empty),
                    new Claim("guid", user.Id.ToString()),
                    new Claim("userType", user.UserType.ToString())
                });

                var response = (token: JwtBearerAuthenticationExtension.GetJwtAccessToken(this._appSettings, claimsIdentity),
                                code: (int)HttpStatusCode.OK,
                                message: "操作成功");

                return this.Ok(response);
            }
        }

        private OkObjectResult FailResponse(string message)
        {
            var response = (code: (int)HttpStatusCode.OK,
                            message);

            return this.Ok(response);
        }
    }
}