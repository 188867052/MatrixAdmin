using Core.Model;
using Core.Model.Administration.Menu;
using Core.Resource;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class MenuViewConfiguration : GridConfiguration<Menu>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuViewConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddTextColumn(new TextGridColumn<Menu>(o => o.Name, MenuIndexResource.Name));
            GridColumn.AddTextColumn(new TextGridColumn<Menu>(o => o.Url, MenuIndexResource.Url));
            GridColumn.AddTextColumn(new TextGridColumn<Menu>(o => o.Alias, MenuIndexResource.Alias));
            GridColumn.AddTextColumn(new TextGridColumn<Menu>(o => o.ParentName, MenuIndexResource.ParentName));
            GridColumn.AddIntegerColumn(new IntegerGridColumn<Menu>(o => o.Sort, MenuIndexResource.Sort));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Menu>(o => o.Status, MenuIndexResource.Status));
            GridColumn.AddEnumColumn(new EnumGridColumn<Menu>(o => o.IsDefaultRouter, MenuIndexResource.IsDefaultRouter));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Menu>(o => o.CreatedOn, MenuIndexResource.CreatedOn));
            GridColumn.AddTextColumn(new TextGridColumn<Menu>(o => o.CreatedByUserName, MenuIndexResource.CreatedByUserName));
        }
    }
}
