using Core.Model.Entity;
using Core.Web.Grid;
using System.Collections.Generic;

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
            GridColumn<Log> column = new GridColumn<Log>(_errors);
            column.AddIntegerColumn(new IntegerColumn<Log>(o => o.Id, "ID"));
            column.AddTextColumn(new TextColumn<Log>(o => o.Message, "日志"));
            column.AddDateTimeColumn(new DateTimeColumn<Log>(o => o.CreateTime, "创建时间"));
            return column.Render();
        }
    }
}
