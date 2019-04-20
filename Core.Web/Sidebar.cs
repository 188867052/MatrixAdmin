using System.Collections.Generic;

namespace Core.Web
{
    public class Sidebar
    {
        private readonly List<Submenu> Submenus;

        public Sidebar(/*string iconClass, string iconText*/)
        {

        }

        public void AddLinkButton(Submenu Submenu)
        {
            this.Submenus.Add(Submenu);
        }

        public string Render()
        {
            string format = "<div id=\"sidebar\">{0}<ul></ul></div>";
            string text = "";
            foreach (var item in Submenus)
            {
                text += item.Render();
            }
            return string.Format(format, text);
        }
    }
}
