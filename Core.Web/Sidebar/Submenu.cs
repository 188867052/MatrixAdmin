using System.Collections.Generic;

namespace Core.Web.Sidebar
{
    public class SubMenu
    {
        private IList<LinkedAnchor> _linkedAnchors;

        public SubMenu(string iconClass, string url, string innerText, int importantCount = default, bool isSelected = default)
        {
            this.IconClass = iconClass;
            this.Url = url;
            this.InnerText = innerText;
            this.ImportantCount = importantCount;
            this.IsSelected = isSelected;
        }

        public bool IsSelected { get; set; }

        public void AddLinkButton(LinkedAnchor linkedAnchors)
        {
            if (this._linkedAnchors == null)
            {
                this._linkedAnchors = new List<LinkedAnchor>();
            }
            this._linkedAnchors.Add(linkedAnchors);
        }


        public int ImportantCount { get; set; }

        public string Url { get; set; }

        public string IconClass { get; set; }

        public string InnerText { get; set; }

        public string Render()
        {
            string activeClass = this.IsSelected ? "class=\"submenu active\"" : "class=\"submenu\"";
            string url = string.IsNullOrEmpty(this.Url) ? "#" : this.Url;

            if (_linkedAnchors == null)
            {
                return $"<li  {activeClass}><a href=\"{url}\"><i class=\"{this.IconClass}\"></i><span>{this.InnerText}</span></a></li>";
            }

            
            string text = "<li {0}>{1}<ul>{2}</ul></li>";
            string importantText = this.ImportantCount == 0 ? "" : $"<span class=\"label label-important\">{this.ImportantCount}</span>";
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
