using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Task
    {
        public Task()
        {
        }

        public Guid Id { get; set; }

        public int StatusId { get; set; }

        public int? PriorityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? DueDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public Guid? AssignedId { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual Status Status { get; set; }
    }
}
