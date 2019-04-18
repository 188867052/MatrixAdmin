using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Api.Entities
{
    /// <summary>
    /// 角色权限关系表
    /// </summary>
    public class RolePermissionMapping
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string RoleCode { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string PermissionCode { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 权限实体
        /// </summary>
        public Permission Permission { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
