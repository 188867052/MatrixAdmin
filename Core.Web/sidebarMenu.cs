using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Web
{
    public class SidebarMenu
    {
        private readonly List<Submenu> LinkButtons;

        public SidebarMenu(/*string iconClass, string iconText*/)
        {

        }

        public void AddLinkButton(Submenu linkButton)
        {
            this.LinkButtons.Add(linkButton);
        }

        public string Render()
        {
            return string.Empty;
        }
    }


    public class Submenu
    {
        public Submenu(string iconClass, string url,string innerText)
        {
            this.IconClass = iconClass;
            this.Url = url;
            this.InnerText = innerText;
        }

        public bool IsActive { get; set; }

        public string Url { get; set; }

        public string IconClass { get; set; }

        public string InnerText { get; set; }

        public string Render()
        {
            string activeClass = IsActive ? "class=\"active\"" : "";
            string url = string.IsNullOrEmpty(Url) ? Url : "";
            return $"<li {activeClass}><a href=\"{url}\"><i class=\"{IconClass}\"></i><span>{InnerText}</span></a></li>";
        }
    }
}
