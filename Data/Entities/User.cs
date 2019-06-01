using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class User
    {
        public User()
        {
            this.CreateByMenus = new HashSet<Menu>();
            this.CreateByRoles = new HashSet<Role>();
            this.UpdateByMenus = new HashSet<Menu>();
            this.UpdateByRoles = new HashSet<Role>();
            this.UserRoleMappings = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }

        public string LoginName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Avatar { get; set; }

        public int UserType { get; set; }

        public int IsLocked { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateByUserName { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UpdateByUserName { get; set; }

        public string Description { get; set; }

        public Guid CreateByUserId { get; set; }

        public Guid? UpdateByUserId { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDeleted { get; set; }

        public int UserStatusId { get; set; }

        public virtual UserStatus UserStatus { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }

        public virtual ICollection<Menu> UpdateByMenus { get; set; }

        public virtual ICollection<Menu> CreateByMenus { get; set; }

        public virtual ICollection<Role> CreateByRoles { get; set; }

        public virtual ICollection<Role> UpdateByRoles { get; set; }
    }
}
