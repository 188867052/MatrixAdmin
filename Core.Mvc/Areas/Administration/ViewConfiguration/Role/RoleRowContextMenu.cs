using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.RowContextMenu;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleRowContextMenu : RowContextMenu<RoleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRowContextMenu"/> class.
        /// </summary>
        /// <param name="model">A model.</param>
        public RoleRowContextMenu(RoleModel model) : base(model)
        {
        }

        protected override void CreateMenu(IList<RowContextMenuLink> links)
        {
            links.Add(new RowContextMenuLink("编辑", "core.editDialog", RoleRoute.EditDialog));
            links.Add(this.Model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover", RoleRoute.Recover)
                : new RowContextMenuLink("删除", "index.delete", RoleRoute.Delete));
            links.Add(this.Model.IsForbidden == ForbiddenStatusEnum.Normal
                ? new RowContextMenuLink("禁用", "index.forbidden", RoleRoute.Forbidden)
                : new RowContextMenuLink("启用", "index.normal", RoleRoute.Normal));
        }
    }
}