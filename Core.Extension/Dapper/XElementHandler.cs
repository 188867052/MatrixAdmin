using System.Xml.Linq;

namespace Core.Extension.Dapper
{
    internal sealed class XElementHandler : XmlTypeHandler<XElement>
    {
        protected override XElement Parse(string xml) => XElement.Parse(xml);

        protected override string Format(XElement xml) => xml.ToString();
    }
}
