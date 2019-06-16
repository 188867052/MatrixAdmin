using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.RowContextMenu;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Menu.MenuRowContextMenu;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
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
            links.Add(new RowContextMenuLink(Resources.Edit, "core.editDialog", MenuRoute.EditDialog));
            links.Add(this.Model.IsEnable
                ? new RowContextMenuLink(Resources.Recover, "index.recover", MenuRoute.Recover)
                : new RowContextMenuLink(Resources.Delete, "index.delete", MenuRoute.Delete));
            links.Add(this.Model.Status == ForbiddenStatusEnum.Normal
                ? new RowContextMenuLink(Resources.Forbidden, "index.forbidden", MenuRoute.Forbidden)
                : new RowContextMenuLink(Resources.Normal, "index.normal", MenuRoute.Normal));
        }
    }
}