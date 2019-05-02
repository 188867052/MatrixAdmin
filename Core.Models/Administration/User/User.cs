using Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsDeleted { get; set; }

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

        public virtual UserStatus UserStatus { get; set; }
        public int UserStatusId { get; internal set; }

        public static void ModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(800);
                entity.Property(e => e.DisplayName).HasMaxLength(50);
                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.UserStatusId).HasDefaultValueSql("((1))");
                entity.HasOne(d => d.UserStatus);
                //.WithMany(p => p.User)
                //.HasForeignKey(d => d.UserStatusId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK__User__UserStatus");
            });
        }
    }
}
