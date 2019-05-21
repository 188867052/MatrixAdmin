using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.Role.RoleIndexResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleViewConfiguration<T> : GridConfiguration<T>
         where T : RoleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewConfiguration{T}"/> class.
        /// </summary>
        /// <param name="entity">The. </param>
        public RoleViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<T>> gridColumns)
        {
            Url url = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.RowContextMenu));
            gridColumns.Add(new RowContextMenuColumn<T>(o => o.Id, "操作", url));
            gridColumns.Add(new TextGridColumn<T>(o => o.Name, Resources.Name));
            gridColumns.Add(new EnumGridColumn<T>(o => o.IsForbidden, "禁用状态"));
            gridColumns.Add(new TextGridColumn<T>(o => o.Description, "描述"));
            gridColumns.Add(new BooleanGridColumn<T>(o => o.IsSuperAdministrator, Resources.IsSuperAdministrator));
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.CreateTime, Resources.CreatedOn));
            gridColumns.Add(new TextGridColumn<T>(o => o.CreatedByUserName, Resources.CreatedByUserName));
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.UpdateTime, "更新时间"));
        }
    }
}
