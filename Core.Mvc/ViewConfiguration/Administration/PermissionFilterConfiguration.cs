using System.Collections.Generic;
using Core.Model.Entity;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.GridFilter;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class PermissionFilterConfiguration : ViewConfiguration<Permission>
    {
        public PermissionFilterConfiguration(IList<Permission> entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddTextColumn(new TextGridColumn<Permission>(o => o.Name, PermissionIndexResource.Name));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Permission>(o => o.Status, "关联菜单"));
            GridColumn.AddTextColumn(new TextGridColumn<Permission>(o => o.ActionCode, PermissionIndexResource.ActionCode));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Permission>(o => o.Status, PermissionIndexResource.Status));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Permission>(o => o.CreatedOn, PermissionIndexResource.CreatedOn));
            GridColumn.AddTextColumn(new TextGridColumn<Permission>(o => o.CreatedByUserName, PermissionIndexResource.CreatedByUserName));
        }

        public override void GenerateGridFilter()
        {
            GridFilter.AddTextFilter(new TextGridFilter<Permission>(o => o.Name, PermissionIndexResource.Name));
        }
    }
}
