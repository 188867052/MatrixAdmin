using System;

namespace Core.Model.Log
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}