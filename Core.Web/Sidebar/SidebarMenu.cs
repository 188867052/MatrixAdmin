using Core.Extension;

namespace Core.Web.Sidebar
{
    public class LinkedAnchor
    {
        public LinkedAnchor(Url url, string innerText, string @class = default)
        {
            this.Url = url;
            this.InnerText = innerText;
            this.Class = @class;
        }

        private string Class { get; }

        private string InnerText { get; }

        private Url Url { get; }

        public string Render()
        {
            return $"<li><a href=\"{this.Url.Render()}\">{this.InnerText}</a></li>";
        }
    }
}
