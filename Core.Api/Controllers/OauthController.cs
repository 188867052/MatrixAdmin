using System.Linq;
using System.Security.Claims;
using Core.Api.Authentication;
using Core.Entity;
using Core.Model;
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
        //http://localhost:90/api/Oauth/Auth
        public IActionResult Auth(string username, string password)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            User user;
            using (this._dbContext)
            {
                user = this._dbContext.User.FirstOrDefault(x => x.LoginName == username.Trim());
                if (user == null || !user.IsEnable)
                {
                    response.SetFailed("用户不存在");
                    return this.Ok(response);
                }

                if (user.Password != password.Trim())
                {
                    response.SetFailed("密码不正确");
                    return this.Ok(response);
                }

                if (user.IsLocked == 1)
                {
                    response.SetFailed("账号已被锁定");
                    return this.Ok(response);
                }

                if (user.Status == 1)
                {
                    response.SetFailed("账号已被禁用");
                    return this.Ok(response);
                }
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
            string token = JwtBearerAuthenticationExtension.GetJwtAccessToken(this._appSettings, claimsIdentity);

            response.SetData(token);
            return this.Ok(response);
        }
    }
}