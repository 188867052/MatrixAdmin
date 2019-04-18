using System.Linq;
using System.Security.Claims;
using Core.Api.Auth;
using Core.Api.Entities;
using Core.Api.Entities.Enums;
using Core.Api.Extensions;
using Core.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly Context _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="dbContext"></param>
        public OauthController(IOptions<AppAuthenticationSettings> appSettings, Context dbContext)
        {
            this._appSettings = appSettings.Value;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Auth(string username, string password)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            User user;
            using (this._dbContext)
            {
                user = this._dbContext.User.FirstOrDefault(x => x.LoginName == username.Trim());
                if (user == null || user.IsEnable)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                if (user.Password != password.Trim())
                {
                    response.SetFailed("密码不正确");
                    return Ok(response);
                }
                if (user.IsLocked == IsLockedEnum.Locked)
                {
                    response.SetFailed("账号已被锁定");
                    return Ok(response);
                }
                if (user.Status == UserStatusEnum.Forbidden)
                {
                    response.SetFailed("账号已被禁用");
                    return Ok(response);
                }
            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("guid",user.Guid.ToString()),
                    new Claim("avatar",""),
                    new Claim("displayName",user.DisplayName),
                    new Claim("loginName",user.LoginName),
                    new Claim("emailAddress",""),
                    new Claim("guid",user.Guid.ToString()),
                    new Claim("userType",((int)user.UserType).ToString())
                });
            string token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

            response.SetData(token);
            return Ok(response);
        }
    }
}