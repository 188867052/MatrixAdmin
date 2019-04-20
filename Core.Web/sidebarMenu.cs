using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Web
{


    public class LinkAnchor
    {
        public LinkAnchor(string url, string innerText)
        {
            this.Url = url;
            this.InnerText = innerText;
        }

        public string InnerText { get; set; }

        public string Url { get; set; }
    }


    public class Submenu
    {
        public IList<LinkAnchor> LinkButtons;

        public Submenu(string iconClass, string url, string innerText)
        {
            this.IconClass = iconClass;
            this.Url = url;
            this.InnerText = innerText;
        }
        public void AddLinkButton(LinkAnchor linkButton)
        {
            if (this.LinkButtons == null)
            {
                this.LinkButtons = new List<LinkAnchor>();
            }
            this.LinkButtons.Add(linkButton);
        }

        public bool IsActive { get; set; }

        public string Url { get; set; }

        public string IconClass { get; set; }

        public string InnerText { get; set; }

        public string Render()
        {
            string activeClass = IsActive ? "class=\"active\"" : "";
            string url = string.IsNullOrEmpty(Url) ? Url : "";

            if (LinkButtons == null)
            {
                return $"<li {activeClass}><a href=\"{url}\"><i class=\"{IconClass}\"></i><span>{InnerText}</span></a></li>";
            }

            string text = "<li class=\"submenu\">{0}<ul>{1}</ul></li>";
            string importantText = $"<a href=\"#\"><i class=\"{IconClass}\"></i><span>{InnerText}</span> <span class=\"label label-important\">3</span></a>";
            string html = string.Empty;
            foreach (var item in LinkButtons)
            {
                html += $"<li><a href=\"{Url}\">{InnerText}</a></li>";
            }

            return string.Format(text, importantText, html);
        }
    }
}
