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

        private string Class { get; }

        private string InnerText { get; }

        private string Url { get; }

        public string Render()
        {
            return $"<li><a href=\"{this.Url}\">{this.InnerText}</a></li>";
        }
    }
}
