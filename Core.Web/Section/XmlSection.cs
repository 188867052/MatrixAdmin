using System;
using System.Collections.Generic;
using System.Text;
using Core.Web.Html;

namespace Core.Web.Section
{
    public class XmlSection : IRender
    {
        public XmlSection(string id, string sectionName)
        {
            this.Id = id;
            this.SectionName = sectionName;
        }

        private string Id { get; }

        private string SectionName { get; }

        private Node Node { get; set; }

        public string Render()
        {
            Dictionary<Attribute, string> dictionary = new Dictionary<Attribute, string>
            {
                { Attribute.v_html, this.Id },
                { Attribute.id, this.Id}
            };
            Node childNode = new Node(TagName.code, dictionary, $"{{{this.Id}}}");
            Node node = new Node(TagName.pre);
            node.AddChildNode(childNode);
            this.Node = node;

            Guid guid = Guid.NewGuid();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div class='panel panel-default'>");
            stringBuilder.Append("<div class='panel-heading col-lg-12'>");
            Node h4Node = new Node(TagName.h4, Attribute.@class, "panel-title text-center");
            Dictionary<Attribute, string> dictionary2 = new Dictionary<Attribute, string>
            {
                { Attribute.data_toggle, "collapse" },
                { Attribute.data_parent, "#accordion" },
                { Attribute.href, $"#{guid}" }
            };
            h4Node.AddChildNode(new Node(TagName.a, dictionary2, this.SectionName));
            stringBuilder.Append(h4Node.Render());
            stringBuilder.Append("</div>");
            stringBuilder.Append(new Node(TagName.div, new Dictionary<Attribute, string> { { Attribute.id, guid.ToString() }, { Attribute.@class, "panel-collapse collapse in" } }, new Node(TagName.p).Render() + this.Node.Render()).Render());
            stringBuilder.Append("</div>");
            return stringBuilder.ToString();
        }
    }
}
