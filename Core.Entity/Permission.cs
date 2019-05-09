using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Permission
    {
        public Permission()
        {
            this.RolePermissionMapping = new HashSet<RolePermissionMapping>();
        }

        public string Id { get; set; }
        public Guid MenuGuid { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedByUserName { get; set; }
        public Guid CreatedByUserGuid { get; set; }
        public Guid? ModifiedByUserGuid { get; set; }
        public bool IsEnable { get; set; }
        public bool Status { get; set; }

        public virtual Menu MenuGu { get; set; }
        public virtual ICollection<RolePermissionMapping> RolePermissionMapping { get; set; }
    }
}
