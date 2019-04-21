using System.Collections.Generic;
using Core.Web.Grid;
using Core.Models.Entities;

namespace Core.Mvc.ViewConfiguration.Error
{
    public class ErrorViewConfiguration
    {
        private readonly IList<Log> _errors;

        public ErrorViewConfiguration(IList<Log> errors)
        {
            _errors = errors;
        }

        public string Render()
        {
            ColumnConfiguration<Log> column = new ColumnConfiguration<Log>(_errors);
            column.AddIntegerColumn(new IntegerColumn<Log>(o => o.Id, "ID"));
            column.AddTextColumn(new TextColumn<Log>(o => o.Message, "日志"));
            column.AddDateTimeColumn(new DateTimeColumn<Log>(o => o.CreateTime, "创建时间"));
            return column.Render();
        }
    }
}
