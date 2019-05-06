using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Web.RowContextMenu;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserRowContextMenu : RowContextMenu<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRowContextMenu"/> class.
        /// </summary>
        public UserRowContextMenu(User model)
            : base(model)
        {
        }

        protected override void CreateMenu(IList<RowContextMenuLink> links)
        {
            links.Add(new RowContextMenuLink("编辑", "index.edit"));
            links.Add(this.model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover")
                : new RowContextMenuLink("删除", "index.delete"));
        }
    }
}