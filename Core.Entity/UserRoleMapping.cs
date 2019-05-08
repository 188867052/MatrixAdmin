using System;

namespace Core.Entity
{
    /// <summary>
    /// 用户-角色映射
    /// </summary>
    public class UserRoleMapping
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户实体
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户实体
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime{ get; set; }
    }
}
