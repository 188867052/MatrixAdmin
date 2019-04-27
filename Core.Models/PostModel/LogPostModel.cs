using System;
using Core.Model.ResponseModels;

namespace Core.Model.PostModel
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
        public LogType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Person Person { get; set; }
    }


    public class Person
    {
        public string Name { get; set; }
        public string Sex { get; set; }
    }


    public enum LogType
    {
        Info = 0,
        Error = 1,
        Alert = 2,
        Debug = 3
    }
}