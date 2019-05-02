using Core.Model.Administration.User;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserRowContextMenu
    {
        private User model;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRowContextMenu(User model)
        {
            this.model = model;
        }


        public string Render()
        {
            string html = $"<a class=\"icon-edit dropdown-item\" data-method=\"index.edit\" href=\"#\">&nbsp;编辑</a>";
            if (model.IsDeleted)
            {
                html += "<a class=\"icon-repeat dropdown-item\" data-method=\"index.recover\" href=\"#\">&nbsp;恢复</a>";

            }
            else
            {
                html += "<a class=\"icon-remove-sign dropdown-item\" data-method=\"index.delete\" href=\"#\">&nbsp;删除</a>";

            }

            return html;
        }
    }
}