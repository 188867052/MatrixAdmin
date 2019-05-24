using System;
using Core.Entity.Enums;
using Microsoft.Extensions.Logging;

namespace Core.Model.Log
{
    public class LogModel : Pager
    {
        public LogModel()
        {
        }

        public LogModel(Entity.Log entity)
        {
            this.Id = entity.Id;
            this.Message = entity.Message;
            this.LogLevel = (LogLevel?)entity.LogLevel;
            this.CreateTime = entity.CreateTime;
            this.SqlType = (SqlTypeEnum)entity.SqlOperateType;
            int a = 66223336;
        }

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

        public SqlTypeEnum SqlType { get; set; }

        public DateTime CreateTime { get; set; }

        public static LogModel Convert(Entity.Log arg)
        {
            return new LogModel(arg);
        }
    }
}