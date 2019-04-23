using Core.Model.Entity;
using System.Collections.Generic;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Error
{
    public class ErrorViewConfiguration: ViewConfiguration<Log>
    {
        public ErrorViewConfiguration(IList<Log> entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddIntegerColumn(new IntegerGridColumn<Log>(o => o.Id, "ID"));
            GridColumn.AddTextColumn(new TextGridColumn<Log>(o => o.Message, "日志"));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Log>(o => o.CreateTime, "创建时间"));
        }
    }
}
