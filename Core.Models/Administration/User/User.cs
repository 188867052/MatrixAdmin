using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        //[DefaultValue("newid()")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)", Order = 10)]
        public string LoginName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string DisplayName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(255)", Order = 100)]
        public string Avatar { get; set; }

        public UserTypeEnum UserType { get; set; }

        public IsLockedEnum IsLocked { get; set; }

        public UserStatusEnum Status { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid CreatedByUserGuid { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// 最近修改者ID
        /// </summary>
        public Guid? ModifiedByUserGuid { get; set; }

        /// <summary>
        /// 最近修改者姓名
        /// </summary>
        public string ModifiedByUserName { get; set; }

        /// <summary>
        /// 用户描述信息
        /// </summary>
        [Column(TypeName = "nvarchar(800)")]
        public string Description { get; set; }

        /// <summary>
        /// 用户的角色集合
        /// </summary>
        public ICollection<UserRoleMapping> UserRoles { get; set; }
    }
}
