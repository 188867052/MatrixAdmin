using Core.Model;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogGridConfiguration : GridConfiguration<Model.Log.Log>
    {
        public LogGridConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddIntegerColumn(new IntegerGridColumn<Model.Log.Log>(o => o.Id, LogResource.ID));
            GridColumn.AddTextColumn(new TextGridColumn<Model.Log.Log>(o => o.Message, LogResource.Message));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Model.Log.Log>(o => o.CreateTime, LogResource.CreateTime));
        }
    }
}
