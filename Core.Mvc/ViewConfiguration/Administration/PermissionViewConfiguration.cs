using System.Collections.Generic;
using Core.Model.Entity;
using Core.Resource.ViewConfiguration.Administration;
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
            GridColumn<Permission> column = new GridColumn<Permission>(this._permissions);
            column.AddTextColumn(new TextColumn<Permission>(o => o.Name, PermissionIndexResource.Name));
            column.AddBooleanColumn(new BooleanColumn<Permission>(o => o.Status, "关联菜单"));
            column.AddTextColumn(new TextColumn<Permission>(o => o.ActionCode, PermissionIndexResource.ActionCode));
            column.AddBooleanColumn(new BooleanColumn<Permission>(o => o.Status, PermissionIndexResource.Status));
            column.AddDateTimeColumn(new DateTimeColumn<Permission>(o => o.CreatedOn, PermissionIndexResource.CreatedOn));
            column.AddTextColumn(new TextColumn<Permission>(o => o.CreatedByUserName, PermissionIndexResource.CreatedByUserName));
            return column.Render();
        }
    }
}
