using System;
using System.Collections.Generic;

namespace ConsoleApp2.DataModels2
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Message { get; set; }
    }
}
