using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class User
    {
        public User()
        {
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
        public DateTime? UpdateTime { get; set; }
        public string ModifiedByUserName { get; set; }
        public string Description { get; set; }
        public Guid CreatedByUserGuid { get; set; }
        public Guid? ModifiedByUserGuid { get; set; }
        public bool IsEnable { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int UserStatusId { get; set; }

        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
