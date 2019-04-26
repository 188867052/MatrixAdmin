using Core.Web.Html;

namespace Core.Web.Sidebar
{
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
}