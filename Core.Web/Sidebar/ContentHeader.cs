using System.Collections.Generic;

namespace Core.Web.Sidebar
{
    public class ContentHeader
    {
        public IList<Anchor> linkedAnchors;
        public string currentText;

        public ContentHeader(string currentText = default)
        {
            this.currentText = currentText;
        }

        public void AddAnchor(Anchor linkedAnchor)
        {
            if (linkedAnchors == null)
            {
                linkedAnchors = new List<Anchor>();
            }
            linkedAnchors.Add(linkedAnchor);
        }

        public string Render()
        {
            string aa = default;
            foreach (var str in linkedAnchors)
            {
                aa += str.Render();
            }

            string current = default;
            if (!string.IsNullOrEmpty(currentText))
            {
                current = $"<a href=\"#\" class=\"current\">{currentText}</a>";
            }

            return $"<div id=\"content-header\"><div id=\"breadcrumb\">{aa}{current}</div></div>";
        }
    }
}
