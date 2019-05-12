using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleViewConfiguration : GridConfiguration<RoleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewConfiguration"/> class.
        /// </summary>
        /// <param name="entity">The. </param>
        public RoleViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<RoleModel>> gridColumns)
        {
            Url url = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.RowContextMenu));
            gridColumns.Add(new RowContextMenuColumn<RoleModel>(o => o.Id.ToString(), "操作", url));
            gridColumns.Add(new TextGridColumn<RoleModel>(o => o.Name, RoleIndexResource.Name));
            gridColumns.Add(new EnumGridColumn<RoleModel>(o => o.IsForbidden, "禁用状态"));
            gridColumns.Add(new TextGridColumn<RoleModel>(o => o.Description, "描述"));
            gridColumns.Add(new BooleanGridColumn<RoleModel>(o => o.IsSuperAdministrator, RoleIndexResource.IsSuperAdministrator));
            gridColumns.Add(new DateTimeGridColumn<RoleModel>(o => o.CreateTime, RoleIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<RoleModel>(o => o.CreatedByUserName, RoleIndexResource.CreatedByUserName));
            gridColumns.Add(new DateTimeGridColumn<RoleModel>(o => o.UpdateTime, "更新时间"));
        }
    }
}
