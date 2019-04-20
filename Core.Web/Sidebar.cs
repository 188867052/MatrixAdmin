using System.Collections.Generic;

namespace Core.Web
{
    public class Sidebar
    {
        private List<SubMenu> _subMenus;

        public Sidebar(/*string iconClass, string iconText*/)
        {

        }

        public void AddLinkButton(SubMenu subMenu)
        {
            if (this._subMenus == null)
            {
                this._subMenus = new List<SubMenu>();
            }
            this._subMenus.Add(subMenu);
        }

        public string Render()
        {
            string sidebar = "<div id=\"sidebar\"><a href=\"#\" class=\"visible-phone\"><i class=\"icon icon-home\"></i>Dashboard</a><ul>{0}</ul></div>";
            string text = "";
            foreach (var item in this._subMenus)
            {
                text += item.Render();
            }
            return string.Format(sidebar, text);
        }
    }
}
