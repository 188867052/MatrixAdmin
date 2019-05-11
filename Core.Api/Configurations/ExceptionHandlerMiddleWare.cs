using System;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Api.Configurations
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
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
            //返回友好的提示
            HttpResponse response = context.Response;

            //状态码
            int nCode = 0;
            if (exception is Exception exception1)
            {
                nCode = 500;
            }

            response.ContentType = context.Request.Headers["Accept"];


            ResponseModel model = new ResponseModel
            {
                Message = exception.Message,
                InnerExceptionMessage = exception.InnerException.Message,
                Code = 500
            };

            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(model)).ConfigureAwait(false);
        }
    }
}