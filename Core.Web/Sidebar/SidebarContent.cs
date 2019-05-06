namespace Core.Web.Sidebar
{
    public class SidebarContent
    {
        public SidebarContent(string text, double width, string stat, string @class)
        {
            this.Text = text;
            this.Width = width;
            this.Stat = stat;
            this.Class = @class;
        }

        public string Class { get; set; }

        public string Text { get; set; }

        public double Width { get; set; }

        public string Stat { get; set; }

        public string Render()
        {
            string content = $"<li class=\"content\">" +
                                 $"<span>{this.Text}</span>" +
                                     $"<div class=\"{this.Class}\">" +
                                       $"<div style = \"width: {this.Width:P};\" class=\"bar\"></div>" +
                                     $"</div>" +
                                 $"<span class=\"percent\">{this.Width:P}</span>" +
                                 $"<div class=\"stat\">{this.Stat}</div>" +
                             $"</li>";

            return content;
        }
    }
}
