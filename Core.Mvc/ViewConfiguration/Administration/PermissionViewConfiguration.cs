using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.Grid;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class PermissionViewConfiguration
    {
        private readonly IList<Permission> _permissions;

        public PermissionViewConfiguration(IList<Permission> permissions)
        {
            this._permissions = permissions;
        }

        public string Render()
        {
            GridColumn<Permission> column = new GridColumn<Permission>(_permissions);
            column.AddTextColumn(new TextColumn<Permission>(o => o.Name, "权限名称"));
            column.AddBooleanColumn(new BooleanColumn<Permission>(o => o.Status, "关联菜单"));
            column.AddTextColumn(new TextColumn<Permission>(o => o.ActionCode, "操作码"));
            column.AddBooleanColumn(new BooleanColumn<Permission>(o => o.Status, "状态"));
            column.AddDateTimeColumn(new DateTimeColumn<Permission>(o => o.CreatedOn, "创建时间"));
            column.AddTextColumn(new TextColumn<Permission>(o => o.CreatedByUserName, "创建者"));
            return column.Render();
        }
    }
}
