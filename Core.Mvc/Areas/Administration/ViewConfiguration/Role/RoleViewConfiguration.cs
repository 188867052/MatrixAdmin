using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleViewConfiguration : GridConfiguration<Entity.Role>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewConfiguration"/> class.
        /// </summary>
        /// <param name="entity">The </param>
        public RoleViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.Role>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Entity.Role>(o => o.Name, RoleIndexResource.Name));
            gridColumns.Add(new BooleanGridColumn<Entity.Role>(o => o.Status, RoleIndexResource.Status));
            gridColumns.Add(new BooleanGridColumn<Entity.Role>(o => o.IsBuiltin, RoleIndexResource.IsBuiltin));
            gridColumns.Add(new BooleanGridColumn<Entity.Role>(o => o.IsSuperAdministrator, RoleIndexResource.IsSuperAdministrator));
            gridColumns.Add(new DateTimeGridColumn<Entity.Role>(o => o.CreatedTime, RoleIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.Role>(o => o.CreatedByUserName, RoleIndexResource.CreatedByUserName));
        }
    }
}
