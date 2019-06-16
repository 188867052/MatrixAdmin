using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Routes;
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
            links.Add(new RowContextMenuLink("编辑", "core.editDialog", UserRoute.EditDialog));
            links.Add(this.Model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover", UserRoute.Recover)
                : new RowContextMenuLink("删除", "index.delete", UserRoute.Delete));
            links.Add(this.Model.IsEnable
                ? new RowContextMenuLink("禁用", "index.forbidden", UserRoute.Forbidden)
                : new RowContextMenuLink("启用", "index.normal", UserRoute.Normal));
        }
    }
}