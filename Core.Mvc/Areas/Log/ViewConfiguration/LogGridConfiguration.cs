using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogGridConfiguration : GridConfiguration<Entity.DataModels.Log>
    {
        public LogGridConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.DataModels.Log>> gridColumns)
        {
            gridColumns.Add(new IntegerGridColumn<Entity.DataModels.Log>(o => o.Id, LogResource.ID));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Log>(o => o.Message, LogResource.Message));
            gridColumns.Add(new DateTimeGridColumn<Entity.DataModels.Log>(o => o.CreateTime, LogResource.CreateTime));
        }
    }
}
