using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.RowContextMenu;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.Menu.MenuRowContextMenu;

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
            links.Add(new RowContextMenuLink(Resources.Edit, "core.editDialog", editUrl));
            links.Add(this.Model.IsEnable
                ? new RowContextMenuLink(Resources.Recover, "index.recover", recoverUrl)
                : new RowContextMenuLink(Resources.Delete, "index.delete", deleteUrl));
            links.Add(this.Model.Status == IsForbiddenEnum.Normal
                ? new RowContextMenuLink(Resources.Forbidden, "index.forbidden", forbiddenUrl)
                : new RowContextMenuLink(Resources.Normal, "index.normal", normalUrl));
        }
    }
}