using System;
using Microsoft.Extensions.Logging;

namespace Core.Model.Log
{
    public class LogPostModel : Pager
    {
        /// <summary>
        ///
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///
        /// </summary>
        public LogLevel? LogLevel { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}