using System;
using Core.Entity;
using Microsoft.Extensions.Logging;

namespace Core.Api.MiddleWare
{
    public class EntityFrameworkLogger : ILogger
    {
        private readonly string _categoryName;

        public EntityFrameworkLogger(string categoryName) => this._categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (this._categoryName == "Microsoft.EntityFrameworkCore.Database.Command" /*&& logLevel == LogLevel.Information*/)
            {
                var logContent = formatter(state, exception);
                if (logContent != "A data reader was disposed." && !logContent.Contains("[Log]"))
                {
                    CoreApiContext coreApiContext = new CoreApiContext();
                    coreApiContext.Log.Add(new Log
                    {
                        Message = logContent,
                        LogLevel = (int)LogLevel.Information
                    });
                    coreApiContext.SaveChanges();
                }
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}