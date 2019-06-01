using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Log
    {
        public Log()
        {
        }

        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Message { get; set; }

        public int LogLevel { get; set; }

        public int SqlOperateType { get; set; }
    }
}
