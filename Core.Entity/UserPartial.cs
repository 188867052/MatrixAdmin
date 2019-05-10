using System.Linq;
using Core.Entity.Enums;

namespace Core.Entity
{
    public partial class User
    {
        public Role Role => this.UserRoleMapping?.FirstOrDefault(o => o.UserId == this.Id)?.Role;

        public string RoleName => this.Role?.Name;

        public UserRoleEnum? UserRole => (UserRoleEnum?)this.Role?.Id;
    }
}
