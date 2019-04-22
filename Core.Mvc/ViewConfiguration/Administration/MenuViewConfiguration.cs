using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.GridColumn;

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
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.Name, "菜单名称"));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.Url, "请求地址"));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.Alias, "菜单别名"));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.ParentName, "上级菜单"));
            column.AddIntegerColumn(new IntegerGridColumn<Menu>(o => o.Sort, "排序"));
            column.AddBooleanColumn(new BooleanGridColumn<Menu>(o => o.Status, "状态"));
            column.AddEnumColumn(new EnumGridColumn<Menu>(o => o.IsDefaultRouter, "默认路由"));
            column.AddDateTimeColumn(new DateTimeGridColumn<Menu>(o => o.CreatedOn, "创建时间"));
            column.AddTextColumn(new TextGridColumn<Menu>(o => o.CreatedByUserName, "创建者"));
            return column.Render();
        }
    }
}
