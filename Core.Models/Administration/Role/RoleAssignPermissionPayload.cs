using System.Collections.Generic;

namespace Core.Model.Administration.Role
{
    /// <summary>
    /// 角色分配权限的请求载体类
    /// </summary>
    public class RoleAssignPermissionPayload
    {
        /// <summary>
        /// 
        /// </summary>
        public RoleAssignPermissionPayload()
        {
            Permissions = new List<string>();
        }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
