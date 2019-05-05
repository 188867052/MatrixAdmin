using Core.Model.Administration.User;
using Core.Web.RowContextMenu;
using System.Collections.Generic;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserRowContextMenu : RowContextMenu<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRowContextMenu(User model) : base(model)
        {
        }

        protected override void CreateMenu(IList<RowContextMenuLink> links)
        {
            links.Add(new RowContextMenuLink("编辑", "index.edit"));
            links.Add(model.IsDeleted
                ? new RowContextMenuLink("恢复", "index.recover")
                : new RowContextMenuLink("删除", "index.delete"));
        }
    }
}