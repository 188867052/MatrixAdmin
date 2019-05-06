using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Permission
{
    public class PermissionGridConfiguration : GridConfiguration<Model.Administration.Permission.Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionGridConfiguration"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public PermissionGridConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Model.Administration.Permission.Permission>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Model.Administration.Permission.Permission>(o => o.Name, PermissionIndexResource.Name));
            gridColumns.Add(new BooleanGridColumn<Model.Administration.Permission.Permission>(o => o.Status, "关联菜单"));
            gridColumns.Add(new TextGridColumn<Model.Administration.Permission.Permission>(o => o.ActionCode, PermissionIndexResource.ActionCode));
            gridColumns.Add(new BooleanGridColumn<Model.Administration.Permission.Permission>(o => o.Status, PermissionIndexResource.Status));
            gridColumns.Add(new DateTimeGridColumn<Model.Administration.Permission.Permission>(o => o.CreatedOn, PermissionIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Model.Administration.Permission.Permission>(o => o.CreatedByUserName, PermissionIndexResource.CreatedByUserName));
        }
    }
}
