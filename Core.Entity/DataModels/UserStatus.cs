using System.Collections.Generic;

namespace Core.Entity.DataModels
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}