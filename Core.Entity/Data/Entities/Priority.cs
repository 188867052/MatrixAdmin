using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Priority
    {
        public Priority()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
