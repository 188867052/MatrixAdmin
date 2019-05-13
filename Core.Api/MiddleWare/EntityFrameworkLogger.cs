using System;
using System.Text.RegularExpressions;
using Core.Entity;
using Microsoft.Extensions.Logging;

namespace Core.Api.MiddleWare
{
    public class EntityFrameworkLogger : ILogger
    {
        private readonly string _categoryName;

        public EntityFrameworkLogger(string categoryName)
        {
            this._categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (this._categoryName == "Microsoft.EntityFrameworkCore.Database.Command")
            {
                var logContent = formatter(state, exception);
                if (logContent != "A data reader was disposed." && !logContent.Contains("[Log]"))
                {
                    logContent = this.ConvertToSql(logContent);
                    CoreApiContext coreApiContext = new CoreApiContext();
                    coreApiContext.Log.Add(new Log
                    {
                        Message = $"<code class=\"sql\">{logContent}</code>",
                        LogLevel = (int)logLevel
                    });
                    coreApiContext.SaveChanges();
                }
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        private string ConvertToSql(string content)
        {
            string sql = Regex.Match(content, "(SELECT|UPDATE|INSERT|DELETE)+(.|\\n)+").Value;

            string pare = Regex.Match(content, "Parameters[^\\]]+").Value.Replace("Parameters=[", string.Empty);
            MatchCollection matches = Regex.Matches(pare, "@[^ ]+");
            foreach (Match item in matches)
            {
                string key = item.Value.Split('=')[0];
                string value = item.Value.Split('=')[1].Replace(",", string.Empty);
                sql = sql.Replace(key, value);
            }

            Match match = Regex.Match(sql, "OFFSET+.+ROWS FETCH NEXT+.+ROWS ONLY");
            if (!string.IsNullOrEmpty(match.Value))
            {
                string pagSql = match.Value;
                sql = sql.Replace(pagSql, pagSql.Replace("'", string.Empty));
            }

            return sql;
        }
    }
}