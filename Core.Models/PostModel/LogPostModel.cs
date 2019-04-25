using System;

namespace Core.Model.PostModel
{
    public class LogPostModel
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
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Person Person { get; set; }
    }


    public class Person
    {
        public string Name { get; set; }
        public string Sex { get; set; }
    }
}