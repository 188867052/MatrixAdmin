using System;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Mvc.Framework.MiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                using (CoreContext coreContext = new CoreContext())
                {
                    coreContext.Log.Add(new Log
                    {
                        Message = $"[1:]{exception.StackTrace}{Environment.NewLine}{Environment.NewLine}" +
                                  $"<p style=\"color:blue\">{exception.Message}</p>{Environment.NewLine}{Environment.NewLine}" +
                                  $"<p style=\"color:red\">{exception.InnerException?.Message}</p>{Environment.NewLine}{Environment.NewLine}" +
                                  $"[4:]{exception.InnerException?.StackTrace}",
                        LogLevel = (int)LogLevel.Error
                    });
                    coreContext.SaveChanges();
                }
            }
        }
    }
}