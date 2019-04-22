using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.Grid;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class RoleViewConfiguration
    {
        private readonly IList<Role> users;

        public RoleViewConfiguration(IList<Role> users)
        {
            this.users = users;
        }

        public string Render()
        {
            GridColumn<Role> column = new GridColumn<Role>(users);
            column.AddTextColumn(new TextGridColumn<Role>(o => o.Name, "角色名称"));
            column.AddBooleanColumn(new BooleanGridColumn<Role>(o => o.Status, "状态"));
            column.AddBooleanColumn(new BooleanGridColumn<Role>(o => o.IsBuiltin, "是否内置"));
            column.AddBooleanColumn(new BooleanGridColumn<Role>(o => o.IsSuperAdministrator, "是否超管"));
            column.AddDateTimeColumn(new DateTimeGridColumn<Role>(o => o.CreatedOn, "创建时间"));
            column.AddTextColumn(new TextGridColumn<Role>(o => o.CreatedByUserName, "创建者"));
            return column.Render();
        }
    }
}
