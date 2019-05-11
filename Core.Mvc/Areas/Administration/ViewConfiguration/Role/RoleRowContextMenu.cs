using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
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
            Url editUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.EditDialog));
            Url recoverUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Recover));
            Url deleteUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Delete));
            Url forbiddenUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Forbidden));
            Url normalUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Normal));
            links.Add(new RowContextMenuLink("编辑", "index.edit", editUrl));
            links.Add(this.Model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover", recoverUrl)
                : new RowContextMenuLink("删除", "index.delete", deleteUrl));
            links.Add(this.Model.IsForbidden == IsForbiddenEnum.Normal
                ? new RowContextMenuLink("禁用", "index.forbidden", forbiddenUrl)
                : new RowContextMenuLink("启用", "index.normal", normalUrl));
        }
    }
}