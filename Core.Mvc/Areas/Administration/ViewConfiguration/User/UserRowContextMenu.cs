using System.Collections.Generic;
using Core.Web.RowContextMenu;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserRowContextMenu : RowContextMenu<Entity.DataModels.User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRowContextMenu"/> class.
        /// </summary>
        /// <param name="model">A model.</param>
        public UserRowContextMenu(Entity.DataModels.User model) : base(model)
        {
        }

        protected override void CreateMenu(IList<RowContextMenuLink> links)
        {
            links.Add(new RowContextMenuLink("编辑", "index.edit"));
            links.Add(this.Model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover")
                : new RowContextMenuLink("删除", "index.delete"));
        }
    }
}