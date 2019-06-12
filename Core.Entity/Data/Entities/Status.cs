using System;

namespace Core.Data.Entities
{
    public partial class Status
    {
        public Status()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public string CreateByUserName { get; set; }

        public DateTimeOffset UpdateTime { get; set; }

        public string UpdateBy { get; set; }

        public Byte[] RowVersion { get; set; }
    }
}
