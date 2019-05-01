namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserRowContextMenu
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRowContextMenu(string id)
        {
        }


        public string Render()
        {
            string html = $"<a class=\"icon-edit dropdown-item\" data-method=\"index.edit\" href=\"#\">&nbsp;编辑</a>";
            html += "<a class=\"icon-remove-sign dropdown-item\" data-method=\"index.delete\" href=\"#\">&nbsp;删除</a>";

            return html;
        }
    }
}