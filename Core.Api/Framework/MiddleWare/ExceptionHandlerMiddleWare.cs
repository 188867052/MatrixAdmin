using System;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Api.Framework.MiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

#pragma warning disable VSTHRD200 // 对异步方法使用“Async”后缀
        public async Task Invoke(HttpContext context)
#pragma warning restore VSTHRD200 // 对异步方法使用“Async”后缀
        {
            try
            {
                await this._next(context);
            }
            catch (Exception exception)
            {
                using (CoreContext coreApiContext = new CoreContext())
                {
                    coreApiContext.Log.Add(new Log
                    {
                        Message = $"[1:]{exception.StackTrace}{Environment.NewLine}{Environment.NewLine}" +
                                  $"<p style=\"color:blue\">{exception.Message}</p>{Environment.NewLine}{Environment.NewLine}" +
                                  $"<p style=\"color:red\">{exception.InnerException.Message}</p>{Environment.NewLine}{Environment.NewLine}" +
                                  $"[4:]{exception.InnerException.StackTrace}",
                        LogLevel = (int)LogLevel.Error
                    });
                    coreApiContext.SaveChanges();
                }
            }
        }
    }
}