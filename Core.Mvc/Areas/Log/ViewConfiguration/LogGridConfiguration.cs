using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogGridConfiguration: GridConfiguration<Model.Log.Log>
    {
        public LogGridConfiguration(ResponseModel response) : base(response)
        {
        }

        //public override void CreateGridColumn<Log>(IList<BaseGridColumn<Model.Log.Log>> gridColumns)
        //{
        //    gridColumns.Add(new IntegerGridColumn<Model.Log.Log>(o => o.Id, LogResource.ID));
        //    gridColumns.Add(new TextGridColumn<Model.Log.Log>(o => o.Message, LogResource.Message));
        //    gridColumns.Add(new DateTimeGridColumn<Model.Log.Log>(o => o.CreateTime, LogResource.CreateTime));
        //}

        public override void CreateGridColumn(IList<BaseGridColumn<Core.Model.Log.Log>> gridColumns)
        {
            gridColumns.Add(new IntegerGridColumn<Model.Log.Log>(o => o.Id, LogResource.ID));
            gridColumns.Add(new TextGridColumn<Model.Log.Log>(o => o.Message, LogResource.Message));
            gridColumns.Add(new DateTimeGridColumn<Model.Log.Log>(o => o.CreateTime, LogResource.CreateTime));
        }
    }
}
