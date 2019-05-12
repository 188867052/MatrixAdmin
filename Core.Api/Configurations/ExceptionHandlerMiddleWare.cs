using System;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Api.Configurations
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            // 返回友好的提示
            HttpResponse response = context.Response;
            response.ContentType = context.Request.Headers["Accept"];

            ResponseModel model = new ResponseModel
            {
                Message = exception.Message,
                InnerExceptionMessage = exception.InnerException.Message,
                Code = 500,
                StackTrace = exception.StackTrace,
                InnerExceptionStackTrace = exception.InnerException.StackTrace,
            };

            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(model)).ConfigureAwait(false);
        }
    }
}