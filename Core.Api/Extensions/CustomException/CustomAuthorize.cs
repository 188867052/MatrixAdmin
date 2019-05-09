using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Api.Extensions.CustomException
{
    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        // private readonly string _someFilterParameter;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ClaimsPrincipal user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
                throw new UnauthorizeException();
            }

            // you can also use registered services
            // var someService = context.HttpContext.RequestServices.GetService<ISomeService>();

            // var isAuthorized = someService.IsUserAuthorized(user.Identity.Name, _someFilterParameter);
            // if (!isAuthorized)
            // {
            //    context.Result = new StatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            //    return;
            // }
        }
    }
}
