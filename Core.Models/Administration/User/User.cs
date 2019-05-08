using Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        public string LoginName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

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
        public DateTime CreateTime { get; set; }

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
        public DateTime? UpdateTime { get; set; }

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
