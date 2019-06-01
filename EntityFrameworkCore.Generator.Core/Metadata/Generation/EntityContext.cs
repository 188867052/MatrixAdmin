using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Core.Metadata.Generation
{
    [DebuggerDisplay("Class: {ContextClass}, Database: {DatabaseName}")]
    public class EntityContext : ModelBase
    {
        public EntityContext()
        {
            this.Entities = new EntityCollection();
        }

        public string ContextNamespace { get; set; }

        public string ContextClass { get; set; }

        public string ContextBaseClass { get; set; }

        public string DatabaseName { get; set; }

        public EntityCollection Entities { get; set; }
    }
}
