using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Api.Configurations
{
    /// <summary>
    /// API自定义错误消息处理委托类。
    /// 用于处理访问不到对应API地址的情况，对错误进行自定义操作。
    /// </summary>
    public class CustomErrorMessageDelegatingHandler : DelegatingHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public CustomErrorMessageDelegatingHandler(RequestDelegate next, ILoggerFactory loggerFactory, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }

        public class ApiKeyMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger _logger;

            public ApiKeyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
            {
                _next = next;
                _logger = loggerFactory.CreateLogger<ApiKeyMiddleware>();
            }

            public async Task Invoke(HttpContext context)
            {
                _logger.LogInformation("Handling API key for: " + context.Request.Path);

                // do custom stuff here with service      

                await _next.Invoke(context);

                _logger.LogInformation("Finished handling api key.");
            }
        }
    }
    public class ApiKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // do custom stuff here

            return base.SendAsync(request, cancellationToken);
        }
    }
}
