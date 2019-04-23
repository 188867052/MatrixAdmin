using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.GridColumn;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class MenuViewConfiguration : ViewConfiguration<Menu>
    {
        public MenuViewConfiguration(IList<Menu> entities) : base(entities)
        {
        }

        public override string Render()
        {
            GridColumn<Menu> column = new GridColumn<Menu>(base.Entity);
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.Name, MenuIndexResource.Name));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.Url, MenuIndexResource.Url));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.Alias, MenuIndexResource.Alias));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.ParentName, MenuIndexResource.ParentName));
            column.AddIntegerColumn(new IntegerGridColumn<Menu>(o => o.Sort, MenuIndexResource.Sort));
            column.AddBooleanColumn(new BooleanGridColumn<Menu>(o => o.Status, MenuIndexResource.Status));
            column.AddEnumColumn(new EnumGridColumn<Menu>(o => o.IsDefaultRouter, MenuIndexResource.IsDefaultRouter));
            column.AddDateTimeColumn(new DateTimeGridColumn<Menu>(o => o.CreatedOn, MenuIndexResource.CreatedOn));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.CreatedByUserName, MenuIndexResource.CreatedByUserName));
            return column.Render();
        }
    }
}
