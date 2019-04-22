namespace Core.Web.Sidebar
{
    public class LinkedAnchor
    {
        public LinkedAnchor(string url, string innerText, string @class = default)
        {
            this.Url = url;
            this.InnerText = innerText;
            this.Class = @class;
        }
        public string Class { get; set; }
        public string InnerText { get; set; }

        public string Url { get; set; }

        public string Render()
        {
            return $"<li><a href=\"{this.Url}\">{this.InnerText}</a></li>";
        }
    }

    public class Anchor
    {
        public Anchor(string url, string displayText, string toolTip, string iconClass, string @class)
        {
            this.Url = url;
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
            return $"<a href=\"{Url}\" class=\"{Class}\" title=\"{ToolTip}\"><i class=\"{IconClass}\"></i>{DisplayText}</a>";
        }
    }

    public class CurrentAnchor : IRender
    {
        public CurrentAnchor(string displayText, string toolTip, string @class)
        {
            this.Class = @class;
            this.DisplayText = displayText;

        }
        public string Class { get; set; }
        public string DisplayText { get; set; }


        public string Render()
        {
            return $"<a href=\"#\" class=\"{Class}\">{DisplayText}</a>";
        }
    }

    public interface IRender
    {
        string Render();
    }
}
