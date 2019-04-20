using System.Collections.Generic;

namespace Core.Web.Sidebar
{
    public class Sidebar
    {
        private List<SubMenu> _subMenus;
        private List<SidebarContent> _subContent;

        public void AddSubMenu(SubMenu subMenu)
        {
            if (this._subMenus == null)
            {
                this._subMenus = new List<SubMenu>();
            }
            this._subMenus.Add(subMenu);
        }

        public void AddSubContent(SidebarContent subContent)
        {
            if (this._subContent == null)
            {
                this._subContent = new List<SidebarContent>();
            }
            this._subContent.Add(subContent);
        }

        public string Render()
        {
            string sidebar = "<div id=\"sidebar\"><a href=\"#\" class=\"visible-phone\"><i class=\"icon icon-home\"></i>Dashboard</a><ul>{0}{1}</ul></div>";
            string menu = "";
            foreach (var item in this._subMenus)
            {
                menu += item.Render();
            }

            string content = "";
            foreach (var item in this._subContent)
            {
                content += item.Render();
            }

            return string.Format(sidebar, menu, content);
        }
    }
}
