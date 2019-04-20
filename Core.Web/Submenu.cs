using System.Collections.Generic;

namespace Core.Web
{
    public class Submenu
    {
        public IList<LinkedAnchor> _linkedAnchors;

        public Submenu(string iconClass, string url, string innerText)
        {
            this.IconClass = iconClass;
            this.Url = url;
            this.InnerText = innerText;
        }
        public void AddLinkButton(LinkedAnchor linkedAnchors)
        {
            if (this._linkedAnchors == null)
            {
                this._linkedAnchors = new List<LinkedAnchor>();
            }
            this._linkedAnchors.Add(linkedAnchors);
        }

        public bool IsActive { get; set; }

        public string Url { get; set; }

        public string IconClass { get; set; }

        public string InnerText { get; set; }

        public string Render()
        {
            string activeClass = IsActive ? "class=\"active\"" : "";
            string url = string.IsNullOrEmpty(Url) ? Url : "";

            if (_linkedAnchors == null)
            {
                return $"<li {activeClass}><a href=\"{url}\"><i class=\"{IconClass}\"></i><span>{InnerText}</span></a></li>";
            }

            string text = "<li class=\"submenu\">{0}<ul>{1}</ul></li>";
            string importantText = $"<a href=\"#\"><i class=\"{this.IconClass}\"></i><span>{this.InnerText}</span> <span class=\"label label-important\">3</span></a>";
            string html = string.Empty;
            foreach (var item in _linkedAnchors)
            {
                html += item.Render();
            }

            return string.Format(text, importantText, html);
        }
    }
}
