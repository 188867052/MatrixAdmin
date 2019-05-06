using System.Collections.Generic;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class RoleViewConfiguration : GridConfiguration<Role>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewConfiguration"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public RoleViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Role>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Role>(o => o.Name, RoleIndexResource.Name));
            gridColumns.Add(new BooleanGridColumn<Role>(o => o.Status, RoleIndexResource.Status));
            gridColumns.Add(new BooleanGridColumn<Role>(o => o.IsBuiltin, RoleIndexResource.IsBuiltin));
            gridColumns.Add(new BooleanGridColumn<Role>(o => o.IsSuperAdministrator, RoleIndexResource.IsSuperAdministrator));
            gridColumns.Add(new DateTimeGridColumn<Role>(o => o.CreatedOn, RoleIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Role>(o => o.CreatedByUserName, RoleIndexResource.CreatedByUserName));
        }
    }
}
