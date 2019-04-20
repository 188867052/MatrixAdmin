using System;
using System.Text;

namespace Core.Web
{
    public class LinkedAnchor
    {
        public LinkedAnchor(string url, string innerText)
        {
            this.Url = url;
            this.InnerText = innerText;
        }

        public string InnerText { get; set; }

        public string Url { get; set; }

        public string Render()
        {
            return $"<li><a href=\"{this.Url}\">{this.InnerText}</a></li>";
        }
    }
}
