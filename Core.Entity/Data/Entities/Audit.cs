using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Audit
    {
        public Audit()
        {
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int? UserId { get; set; }

        public int? TaskId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }
    }
}
