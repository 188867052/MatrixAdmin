using Core.Extension;

namespace Core.Web.Sidebar
{
    public class Anchor
    {
        public Anchor(Url url, string displayText, string toolTip, string iconClass, string @class)
        {
            this.Url = url.Render();
            this.DisplayText = displayText;
            this.Class = @class;
            this.ToolTip = toolTip;
            this.IconClass = iconClass;
        }

        public string ToolTip { get; set; }

        public string Class { get; set; }

        public string IconClass { get; set; }

        public string DisplayText { get; set; }

        public string Url { get; set; }

        public string Render()
        {
            return $"<a href=\"{this.Url}\" class=\"{this.Class}\" title=\"{this.ToolTip}\"><i class=\"{this.IconClass}\"></i>{this.DisplayText}</a>";
        }
    }
}