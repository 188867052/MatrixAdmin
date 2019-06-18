using AutoMapper;
using Core.Api.Authentication;
using Core.Api.Framework;
using Core.Api.Framework.StartupConfigurations;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Resources = Core.Api.Resource.Controllers.AuthenticationControllerResource;

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
        public IActionResult Auth(string username, string password)
        {
            using (this.DbContext)
            {
                User user = this.DbContext.User.AsNoTracking().FirstOrDefault(x => x.LoginName == username.Trim());
                if (user == null || !user.IsEnable)
                {
                    return this.FailResponse(Resources.UserNotExist);
                }

                if (user.Password != password.Trim())
                {
                    return this.FailResponse(Resources.PasswordWrong);
                }

                if (user.IsLocked)
                {
                    return this.FailResponse(Resources.Locked);
                }

                if (!user.IsEnable)
                {
                    return this.FailResponse(Resources.UserDisable);
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(nameof(Entity.User.Id), user.Id.ToString()),
                    new Claim(nameof(Entity.User.LoginName), user.LoginName),
                    new Claim(nameof(Entity.User.Password), user.Password),
                    new Claim(nameof(Entity.User.CreateByUserName), user.CreateByUserName??string.Empty),
                    new Claim(nameof(Entity.User.UpdateByUserName), user.UpdateByUserName??string.Empty),
                    new Claim(nameof(Entity.User.Description), user.Description??string.Empty),
                    new Claim(nameof(Entity.User.DisplayName), user.DisplayName??string.Empty),
                    new Claim(nameof(Entity.User.IsEnable),  user.IsEnable.ToString()),
                    new Claim(nameof(Entity.User.UserType), user.UserType.ToString())
                });

                return this.Ok(new
                {
                    token = AuthenticationConfiguration.GetJwtAccessToken(this._appSettings, claimsIdentity),
                    code = (int)HttpStatusCode.OK,
                    message = Resources.OperateSuccess
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