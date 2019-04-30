using System.Collections.Generic;
using Core.Model;
using Core.Model.Administration.Permission;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
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

        public override void CreateGridColumn(IList<BaseGridColumn<Permission>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Permission>(o => o.Name, PermissionIndexResource.Name));
            gridColumns.Add(new BooleanGridColumn<Permission>(o => o.Status, "关联菜单"));
            gridColumns.Add(new TextGridColumn<Permission>(o => o.ActionCode, PermissionIndexResource.ActionCode));
            gridColumns.Add(new BooleanGridColumn<Permission>(o => o.Status, PermissionIndexResource.Status));
            gridColumns.Add(new DateTimeGridColumn<Permission>(o => o.CreatedOn, PermissionIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Permission>(o => o.CreatedByUserName, PermissionIndexResource.CreatedByUserName));
        }
    }
}
