using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Core.Metadata.Parsing
{
    [DebuggerDisplay("Entity: {EntityClass}, Property: {ContextProperty}")]
    public class ParsedEntitySet
    {
        public string EntityClass { get; set; }

        public string ContextProperty { get; set; }
    }
}