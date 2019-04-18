using System;

namespace Core.Api.Entities
{
    /// <summary>
    /// 用户-角色映射
    /// </summary>
    public class UserRoleMapping
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// 用户实体
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
