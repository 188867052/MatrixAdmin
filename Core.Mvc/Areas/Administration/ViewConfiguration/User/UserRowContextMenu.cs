using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.RowContextMenu;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserRowContextMenu : RowContextMenu<UserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRowContextMenu"/> class.
        /// </summary>
        /// <param name="model">A model.</param>
        public UserRowContextMenu(UserModel model) : base(model)
        {
        }

        protected override void CreateMenu(IList<RowContextMenuLink> links)
        {
            Url editUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.EditDialog));
            Url recoverUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Recover));
            Url deleteUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Delete));
            Url forbiddenUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Forbidden));
            Url normalUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Normal));
            links.Add(new RowContextMenuLink("编辑", "core.editDialog", editUrl));
            links.Add(this.Model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover", recoverUrl)
                : new RowContextMenuLink("删除", "index.delete", deleteUrl));
            links.Add(this.Model.Status == IsForbiddenEnum.Normal
                ? new RowContextMenuLink("禁用", "index.forbidden", forbiddenUrl)
                : new RowContextMenuLink("启用", "index.normal", normalUrl));
        }
    }
}