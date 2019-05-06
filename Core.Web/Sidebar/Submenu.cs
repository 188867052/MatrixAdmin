using System.Collections.Generic;
using Core.Extension;

namespace Core.Web.Sidebar
{
    public class SubMenu
    {
        private IList<LinkedAnchor> _linkedAnchors;

        public SubMenu(string iconClass, Url url, string innerText, int importantCount = default, bool isSelected = default)
        {
            this.IconClass = iconClass;
            this.Url = url?.Render();
            this.InnerText = innerText;
            this.ImportantCount = importantCount;
            this.IsSelected = isSelected;
        }

        public bool IsSelected { get; set; }

        public int ImportantCount { get; set; }

        public string Url { get; set; }

        public string IconClass { get; set; }

        public string InnerText { get; set; }

        public void AddLinkButton(LinkedAnchor linkedAnchors)
        {
            if (this._linkedAnchors == null)
            {
                this._linkedAnchors = new List<LinkedAnchor>();
            }

            this._linkedAnchors.Add(linkedAnchors);
        }

        public string Render()
        {
            string activeClass = this.IsSelected ? "class=\"active\"" : string.Empty;
            string url = string.IsNullOrEmpty(this.Url) ? "#" : this.Url;

            if (this._linkedAnchors == null)
            {
                return $"<li {activeClass}><a href=\"{url}\"><i class=\"{this.IconClass}\"></i><span>{this.InnerText}</span></a></li>";
            }

            activeClass = this.IsSelected ? "class=\"submenu active\"" : "class=\"submenu\"";
            string text = "<li {0}>{1}<ul>{2}</ul></li>";
            string importantText = this.ImportantCount == 0 ? string.Empty : $"<span class=\"label label-important\">{this.ImportantCount}</span>";
            string header = $"<a href=\"#\"><i class=\"{this.IconClass}\"></i><span>{this.InnerText}</span>{importantText}</a>";
            string linkedAnchorHtml = string.Empty;
            foreach (LinkedAnchor item in this._linkedAnchors)
            {
                linkedAnchorHtml += item.Render();
            }

            return string.Format(text, activeClass, header, linkedAnchorHtml);
        }
    }
}
