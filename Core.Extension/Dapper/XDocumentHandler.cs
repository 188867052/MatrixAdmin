using System.Xml.Linq;

namespace Core.Extension.Dapper
{
    internal sealed class XDocumentHandler : XmlTypeHandler<XDocument>
    {
        protected override XDocument Parse(string xml) => XDocument.Parse(xml);

        protected override string Format(XDocument xml) => xml.ToString();
    }
}
