using System.Linq;
using Core.Entity.Enums;

namespace Core.Entity
{
    public partial class User
    {
        public Role Role => this.RoleMapping?.Role;

        public UserRoleMapping RoleMapping => this.UserRoleMapping?.FirstOrDefault(o => o.UserId == this.Id);

        public string RoleName => this.Role?.Name;

        public UserRoleEnum? UserRole => (UserRoleEnum?)this.Role?.Id;
    }
}
