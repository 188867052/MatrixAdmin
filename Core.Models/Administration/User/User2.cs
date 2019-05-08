using System.Linq;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class User
    {
        public Role.Role Role
        {
            get
            {
                UserRoleMapping userRoleMapping = UserRoles?.FirstOrDefault(u => u.User.Id == this.Id);
                return userRoleMapping?.Role;
            }
        }

        public string RoleName => Role?.Name;
    }
}