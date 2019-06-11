using AutoMapper;
using Core.Api.Auth;
using Core.Api.Framework;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationController : StandardController
    {
        private readonly AppAuthenticationSettings _appSettings;

        public AuthenticationController(IOptions<AppAuthenticationSettings> appSettings, CoreContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            this._appSettings = appSettings.Value;
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
            using (this.DbContext)
            {
                User user = this.DbContext.User.FirstOrDefault(x => x.LoginName == username.Trim());
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

                if (!user.IsEnable)
                {
                    return this.FailResponse("账号已被禁用");
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("id", user.Id.ToString()),
                    new Claim("avatar", string.Empty),
                    new Claim("displayName", user.DisplayName),
                    new Claim("loginName", user.LoginName),
                    new Claim("emailAddress", string.Empty),
                    new Claim("userType", user.UserType.ToString())
                });

                return this.Ok(new
                {
                    token = JwtBearerAuthenticationExtension.GetJwtAccessToken(this._appSettings, claimsIdentity),
                    code = (int)HttpStatusCode.OK,
                    message = "操作成功"
                });
            }
        }

        private OkObjectResult FailResponse(string message)
        {
            return this.Ok(new
            {
                code = (int)HttpStatusCode.OK,
                message
            });
        }
    }
}