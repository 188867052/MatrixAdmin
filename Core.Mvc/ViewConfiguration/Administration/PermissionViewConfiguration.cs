using Core.Model;
using Core.Model.Administration.Permission;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class PermissionGridConfiguration : GridConfiguration<Permission>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public PermissionGridConfiguration(ResponseModel entity) : base(entity)
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
    }
}
