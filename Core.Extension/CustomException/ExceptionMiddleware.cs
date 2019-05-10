using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Extension.CustomException
{
    /// <summary>
    /// 异常中间件.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this._next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorDetails error = new ErrorDetails
            {
                StatusCode = 500,
                Message = $"资源服务器忙,请稍候再试,原因:{exception.Message}"
            };
            if (exception is UnauthorizeException)
            {
                error.StatusCode = (int)HttpStatusCode.Unauthorized;
                error.Message = "未授权的访问(未登录或者登录已超时)";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            return context.Response.WriteAsync(error.ToString());
        }
    }
}
