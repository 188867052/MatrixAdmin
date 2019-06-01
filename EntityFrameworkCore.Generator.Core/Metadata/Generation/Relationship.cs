using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Core.Metadata.Generation
{
    [DebuggerDisplay("Primary: {PrimaryEntity}, Property: {PropertyName}, Relationship: {RelationshipName}")]
    public class Relationship : ModelBase
    {
        public Relationship()
        {
            this.Properties = new PropertyCollection();
            this.PrimaryProperties = new PropertyCollection();
        }

        public string RelationshipName { get; set; }

        public Entity Entity { get; set; }

        public PropertyCollection Properties { get; set; }

        public string PropertyName { get; set; }

        public Cardinality Cardinality { get; set; }

        public Entity PrimaryEntity { get; set; }

        public PropertyCollection PrimaryProperties { get; set; }

        public string PrimaryPropertyName { get; set; }

        public Cardinality PrimaryCardinality { get; set; }

        public bool? CascadeDelete { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsMapped { get; set; }

        public bool IsOneToOne => this.Cardinality != Cardinality.Many && this.PrimaryCardinality != Cardinality.Many;
    }
}
