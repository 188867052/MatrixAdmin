using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            CodeRolePermissionMappings = new HashSet<RolePermissionMapping>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ActionCode { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateByUserName { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateByUserName { get; set; }

        public bool IsEnable { get; set; }

        public bool Status { get; set; }

        public int CreateByUserId { get; set; }

        public int UpdateByUserId { get; set; }

        public int MenuId { get; set; }

        public virtual ICollection<RolePermissionMapping> CodeRolePermissionMappings { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
