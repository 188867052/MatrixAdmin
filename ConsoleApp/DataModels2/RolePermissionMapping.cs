using System;

namespace ConsoleApp.DataModels2
{
    public partial class RolePermissionMapping
    {
        public string PermissionCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? RoleId { get; set; }
        public int Id { get; set; }

        public virtual Permission PermissionCodeNavigation { get; set; }
        public virtual Role Role { get; set; }
    }
}
