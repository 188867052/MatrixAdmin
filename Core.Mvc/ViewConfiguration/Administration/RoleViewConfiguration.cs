using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class RoleViewConfiguration : ViewConfiguration<Role>
    {
        public RoleViewConfiguration(IList<Role> entity) : base(entity)
        {
        }

        public override string Render()
        {
            GridColumn<Role> column = new GridColumn<Role>(base.Entity);
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
