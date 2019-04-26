using Core.Model.Entity;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class MenuViewConfiguration : GridConfiguration<Menu>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entities"></param>
        public MenuViewConfiguration(IList<Menu> entities) : base(entities)
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
