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
            string html = $"<a class=\"dropdown-item\" data-method=\"index.edit\" href=\"#\">编辑</a>";
            html += "<a class=\"dropdown-item\" data-method=\"index.delete\" href=\"#\">删除</a>";

            return html;
        }
    }
}