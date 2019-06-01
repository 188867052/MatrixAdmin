using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class UserRole
    {
        public UserRole()
        {
        }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
