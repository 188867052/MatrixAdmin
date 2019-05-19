using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class User
    {
        public User()
        {
            MenuCreateByUser = new HashSet<Menu>();
            MenuUpdateByUser = new HashSet<Menu>();
            RoleCreateByUser = new HashSet<Role>();
            RoleModifiedByUser = new HashSet<Role>();
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public int UserType { get; set; }
        public int IsLocked { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateByUserName { get; set; }
        public string Description { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid? UpdateByUserId { get; set; }
        public bool IsEnable { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int UserStatusId { get; set; }

        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<Menu> MenuCreateByUser { get; set; }
        public virtual ICollection<Menu> MenuUpdateByUser { get; set; }
        public virtual ICollection<Role> RoleCreateByUser { get; set; }
        public virtual ICollection<Role> RoleModifiedByUser { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
