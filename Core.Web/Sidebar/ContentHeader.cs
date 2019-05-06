using System.Collections.Generic;

namespace Core.Web.Sidebar
{
    public class ContentHeader
    {
        private readonly string currentText;
        private IList<Anchor> linkedAnchors;

        public ContentHeader(string currentText = default)
        {
            this.currentText = currentText;
        }

        public void AddAnchor(Anchor linkedAnchor)
        {
            if (this.linkedAnchors == null)
            {
                this.linkedAnchors = new List<Anchor>();
            }

            this.linkedAnchors.Add(linkedAnchor);
        }

        public string Render()
        {
            string aa = default;
            foreach (var str in this.linkedAnchors)
            {
                aa += str.Render();
            }

            string current = default;
            if (!string.IsNullOrEmpty(this.currentText))
            {
                current = $"<a href=\"#\" class=\"current\">{this.currentText}</a>";
            }

            return $"<div id=\"content-header\"><div id=\"breadcrumb\">{aa}{current}</div></div>";
        }
    }
}
