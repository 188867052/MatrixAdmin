using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.Grid;

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
            ColumnConfiguration<Menu> column = new ColumnConfiguration<Menu>(_menus);
            column.AddTextColumn(new TextColumn<Menu>(o => o.Name, "菜单名称"));
            column.AddTextColumn(new TextColumn<Menu>(o => o.Url, "请求地址"));
            column.AddTextColumn(new TextColumn<Menu>(o => o.Alias, "菜单别名"));
            column.AddTextColumn(new TextColumn<Menu>(o => o.ParentName, "上级菜单"));
            column.AddIntegerColumn(new IntegerColumn<Menu>(o => o.Sort, "排序"));
            column.AddBooleanColumn(new BooleanColumn<Menu>(o => o.Status, "状态"));
            column.AddEnumColumn(new EnumColumn<Menu>(o => o.IsDefaultRouter, "默认路由"));
            column.AddDateTimeColumn(new DateTimeColumn<Menu>(o => o.CreatedOn, "创建时间"));
            column.AddTextColumn(new TextColumn<Menu>(o => o.CreatedByUserName, "创建者"));
            return column.Render();
        }
    }
}
