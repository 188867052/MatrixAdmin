using System;
using System.Net;

namespace Core.Api.CustomException
{
    /// <summary>
    ///
    /// </summary>
    public class HttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="statusCode"></param>
        public HttpException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        ///
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
    }
}
