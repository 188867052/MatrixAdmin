using Core.Model.ResponseModels;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogGridConfiguration : GridConfiguration<Model.Entity.Log>
    {
        public LogGridConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddIntegerColumn(new IntegerGridColumn<Model.Entity.Log>(o => o.Id, LogResource.ID));
            GridColumn.AddTextColumn(new TextGridColumn<Model.Entity.Log>(o => o.Message, LogResource.Message));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Model.Entity.Log>(o => o.CreateTime, LogResource.CreateTime));
        }
    }
}
