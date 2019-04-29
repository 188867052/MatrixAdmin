using Core.Model;
using Core.Model.Administration.Icon;
using Core.Resource;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class IconGridConfiguration : GridConfiguration<Icon>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IconGridConfiguration(ResponseModel respnse) : base(respnse)
        {
        }

        public override void GenerateGridColumn()   
        {
            GridColumn.AddIconColumn(new IconGridColumn<Icon>(o => o.Code, IconResource.Icon));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Code, IconResource.Code));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Custom, IconResource.Custom));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Size, IconResource.Size));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Color, IconResource.Color));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Icon>(o => o.IsEnable, IconResource.Status));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Icon>(o => o.CreatedOn, IconResource.CreatedOn));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.CreatedByUserName, IconResource.CreatedByUserName));
        }
    }
}
