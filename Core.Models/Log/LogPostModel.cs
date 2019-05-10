using System;

namespace Core.Model.Log
{
    public class LogPostModel : Pager
    {
        public bool? IsEnable { get; set; }

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
        public LogType? Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}