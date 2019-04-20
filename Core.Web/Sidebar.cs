using System.Collections.Generic;

namespace Core.Web
{
    public class Sidebar
    {
        private List<Submenu> _subMenus;

        public Sidebar(/*string iconClass, string iconText*/)
        {

        }

        public void AddLinkButton(Submenu Submenu)
        {
            if (this._subMenus == null)
            {
                this._subMenus = new List<Submenu>();
            }
            this._subMenus.Add(Submenu);
        }

        public string Render()
        {
            string format = "<div id=\"sidebar\"><a href=\"#\" class=\"visible-phone\"><i class=\"icon icon-home\"></i>Dashboard</a>{0}<ul></ul></div>";
            string text = "";
            foreach (var item in this._subMenus)
            {
                text += item.Render();
            }
            return string.Format(format, text);
        }
    }
}
