using System.Collections.Generic;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogGridConfiguration : GridConfiguration<Model.Entity.Log>
    {
        public LogGridConfiguration(IList<Model.Entity.Log> entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddTextColumn(new TextGridColumn<Model.Entity.Log>(o => o.Message, LogResource.Message));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Model.Entity.Log>(o => o.CreateTime, LogResource.CreateTime));
        }
    }
}
