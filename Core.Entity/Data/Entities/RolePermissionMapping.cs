using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class RolePermissionMapping
    {
        public RolePermissionMapping()
        {
        }

        public int Id { get; set; }

        public string PermissionCode { get; set; }

        public DateTime CreateTime { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Permission CodePermission { get; set; }
    }
}
