using Microsoft.Extensions.Logging;

namespace Core.Api.MiddleWare
{
    public class EntityFrameworkLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new EntityFrameworkLogger(categoryName);

        public void Dispose()
        {
        }
    }
}