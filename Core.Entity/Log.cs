using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Message { get; set; }
        public int? LogLevel { get; set; }
    }
}
