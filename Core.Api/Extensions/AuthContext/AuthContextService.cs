using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using Core.Entity.Enums;

namespace Core.Api.Extensions.AuthContext
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthContextService
    {
        private static IHttpContextAccessor _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
        /// <summary>
        /// 
        /// </summary>
        public static HttpContext Current => _context.HttpContext;
        /// <summary>
        /// 
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
                    Avator= Current.User.FindFirstValue("avator"),
                    Guid= new Guid(Current.User.FindFirstValue("guid"))
                };
                return user;
            }
        }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static bool IsSupperAdministrator
        {
            get
            {
                return ((UserRoleEnum)Convert.ToInt32(Current.User.FindFirstValue("userType"))== UserRoleEnum.SuperAdministrator);
            }
        }
    }
}
