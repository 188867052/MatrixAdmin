using System;
using System.Security.Claims;
using Core.Entity.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Api.AuthContext
{
    /// <summary>
    /// AuthContextService.
    /// </summary>
    public static class AuthenticationContextService
    {
        private static IHttpContextAccessor _context;

        public static HttpContext Current => _context.HttpContext;

        /// <summary>
        /// CurrentUser.
        /// </summary>
        public static AuthContextUser CurrentUser
        {
            get
            {
                AuthContextUser user = new AuthContextUser
                {
                    LoginName = Current.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    DisplayName = Current.User.FindFirstValue("displayName"),
                    EmailAddress = Current.User.FindFirstValue("emailAddress"),
                    UserType = (UserRoleEnum)Convert.ToInt32(Current.User.FindFirstValue("userType")),
                    Avator = Current.User.FindFirstValue("avator"),
                };
                return user;
            }
        }

        /// <summary>
        /// 是否已授权.
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 是否是超级管理员.
        /// </summary>
        public static bool IsSupperAdministrator
        {
            get
            {
                return (UserRoleEnum)Convert.ToInt32(Current.User.FindFirstValue("userType")) ==
                       UserRoleEnum.SuperAdministrator;
            }
        }

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="httpContextAccessor">httpContextAccessor.</param>
        public static void AddConfigure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
    }
}
