using System.Collections.Generic;
using Core.Model;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Permission.PermissionIndexResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Permission
{
    public class PermissionGridConfiguration : GridConfiguration<Entity.Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionGridConfiguration"/> class.
        /// </summary>
        /// <param name="entity">The. </param>
        public PermissionGridConfiguration(HttpResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.Permission>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Entity.Permission>(o => o.Name, Resources.Name));
            gridColumns.Add(new BooleanGridColumn<Entity.Permission>(o => o.Status, "关联菜单"));
            gridColumns.Add(new TextGridColumn<Entity.Permission>(o => o.ActionCode, Resources.ActionCode));
            gridColumns.Add(new BooleanGridColumn<Entity.Permission>(o => o.Status, Resources.Status));
            gridColumns.Add(new DateTimeGridColumn<Entity.Permission>(o => o.CreateTime, Resources.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.Permission>(o => o.CreateByUserName, Resources.CreatedByUserName));
        }
    }
}
