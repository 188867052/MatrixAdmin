using System;

namespace Core.Entity
{
    public partial class RolePermissionMapping
    {
        public int Id { get; set; }

        public string PermissionCode { get; set; }

        public DateTime CreateTime { get; set; }

        public int? RoleId { get; set; }

        public virtual Permission PermissionCodeNavigation { get; set; }

        public virtual Role Role { get; set; }
    }
}
