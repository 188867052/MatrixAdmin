using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleViewConfiguration : GridConfiguration<Entity.DataModels.Role>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewConfiguration"/> class.
        /// </summary>
        /// <param name="entity">The </param>
        public RoleViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.DataModels.Role>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Role>(o => o.Name, RoleIndexResource.Name));
            gridColumns.Add(new BooleanGridColumn<Entity.DataModels.Role>(o => o.Status, RoleIndexResource.Status));
            gridColumns.Add(new BooleanGridColumn<Entity.DataModels.Role>(o => o.IsBuiltin, RoleIndexResource.IsBuiltin));
            gridColumns.Add(new BooleanGridColumn<Entity.DataModels.Role>(o => o.IsSuperAdministrator, RoleIndexResource.IsSuperAdministrator));
            gridColumns.Add(new DateTimeGridColumn<Entity.DataModels.Role>(o => o.CreatedTime, RoleIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Role>(o => o.CreatedByUserName, RoleIndexResource.CreatedByUserName));
        }
    }
}
