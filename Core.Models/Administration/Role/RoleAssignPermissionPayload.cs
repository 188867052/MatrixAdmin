using System.Collections.Generic;

namespace Core.Model.Administration.Role
{
    /// <summary>
    /// 角色分配权限的请求载体类.
    /// </summary>
    public class RoleAssignPermissionPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleAssignPermissionPayload"/> class.
        /// </summary>
        public RoleAssignPermissionPayload()
        {
            this.Permissions = new List<string>();
        }

        /// <summary>
        /// 角色编码.
        /// </summary>
        public int RoleCode { get; set; }

        /// <summary>
        /// 权限列表.
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
