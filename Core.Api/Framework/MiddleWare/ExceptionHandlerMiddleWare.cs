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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception exception)
            {
                CoreApiContext coreApiContext = new CoreApiContext();
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