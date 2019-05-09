using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Api.Configurations
{
    /// <summary>
    /// API自定义错误消息处理委托类。
    /// 用于处理访问不到对应API地址的情况，对错误进行自定义操作。.
    /// </summary>
    public class CustomErrorMessageDelegatingHandler : DelegatingHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomErrorMessageDelegatingHandler"/> class.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="logger"></param>
        public CustomErrorMessageDelegatingHandler(RequestDelegate next, ILoggerFactory loggerFactory, ILogger logger)
        {
            this._next = next;
            this._logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            await this._next.Invoke(context);
        }

        public class ApiKeyMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger _logger;

            /// <summary>
            /// Initializes a new instance of the <see cref="ApiKeyMiddleware"/> class.
            /// </summary>
            /// <param name="next"></param>
            /// <param name="loggerFactory"></param>
            public ApiKeyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
            {
                this._next = next;
                this._logger = loggerFactory.CreateLogger<ApiKeyMiddleware>();
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="context"></param>
            /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
            public async Task Invoke(HttpContext context)
            {
                this._logger.LogInformation("Handling API key for: " + context.Request.Path);

                // do custom stuff here with service

                await this._next.Invoke(context);

                this._logger.LogInformation("Finished handling api key.");
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
