using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.RowContextMenu;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class MenuRowContextMenu : RowContextMenu<MenuModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuRowContextMenu"/> class.
        /// </summary>
        /// <param name="model">A model.</param>
        public MenuRowContextMenu(MenuModel model) : base(model)
        {
        }

        protected override void CreateMenu(IList<RowContextMenuLink> links)
        {
            Url editUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.EditDialog));
            Url recoverUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.Recover));
            Url deleteUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.Delete));
            Url forbiddenUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.Forbidden));
            Url normalUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.Normal));
            links.Add(new RowContextMenuLink("编辑", "core.editDialog", editUrl));
            links.Add(this.Model.IsEnable
                ? new RowContextMenuLink("恢复", "index.recover", recoverUrl)
                : new RowContextMenuLink("删除", "index.delete", deleteUrl));
            links.Add(this.Model.Status == IsForbiddenEnum.Normal
                ? new RowContextMenuLink("禁用", "index.forbidden", forbiddenUrl)
                : new RowContextMenuLink("启用", "index.normal", normalUrl));
        }
    }
}