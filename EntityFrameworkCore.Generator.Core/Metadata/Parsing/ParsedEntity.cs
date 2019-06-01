using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Core.Metadata.Parsing
{
    [DebuggerDisplay("Table: {TableName}, Entity: {EntityClass}, Mapping: {MappingClass}")]
    public class ParsedEntity
    {
        public ParsedEntity()
        {
            this.Properties = new List<ParsedProperty>();
            this.Relationships = new List<ParsedRelationship>();
        }

        public string EntityClass { get; set; }

        public string MappingClass { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }

        public List<ParsedProperty> Properties { get; private set; }

        public List<ParsedRelationship> Relationships { get; private set; }
    }
}