using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.GridColumn;
using  Core.Resource.ViewConfiguration.Administration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class MenuViewConfiguration
    {
        private readonly IList<Menu> _menus;

        public MenuViewConfiguration(IList<Menu> menus)
        {
            this._menus = menus;
        }

        public string Render()
        {
            GridColumn<Menu> column = new GridColumn<Menu>(_menus);
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
