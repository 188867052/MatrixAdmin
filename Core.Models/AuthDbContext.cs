using Core.Entity;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Permission;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
            this.Guid = Guid.NewGuid();
        }

        /// <summary>
        /// Dapper
        /// </summary>
        public DbConnection Dapper => this.Database.GetDbConnection();

        public Guid Guid { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<Menu> Menu { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public DbSet<Icon> Icon { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        public DbSet<Log.Log> Log { get; set; }

        /// <summary>
        /// 用户-角色多对多映射
        /// </summary>
        public DbSet<UserRoleMapping> UserRoleMapping { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Entity.Permission> Permission { get; set; }

        /// <summary>
        /// 角色-权限多对多映射
        /// </summary>
        public DbSet<RolePermissionMapping> RolePermissionMapping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbQuery<PermissionWithAssignProperty> PermissionWithAssignProperty { get; set; }

        public virtual DbSet<UserStatus> UserStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbQuery<PermissionWithMenu> PermissionWithMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(x => x.Id).IsUnique();
            });


            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.UserId,
                    RoleCode = x.RoleId
                });

                //entity.HasOne(x => x.User)
                //    .WithMany(x => x.UserRoles)
                //    .HasForeignKey(x => x.UserGuid)
                //    .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(x => x.Role)
                //    .WithMany(x => x.UserRoles)
                //    .HasForeignKey(x => x.RoleCode)
                //    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Entity.Permission>(entity =>
            {
                entity.HasIndex(x => x.Id)
                    .IsUnique();

                entity.HasOne(x => x.Menu)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.MenuGuid);
            });

            modelBuilder.Entity<RolePermissionMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    RoleId = x.RoleId,
                    x.PermissionCode
                });

                entity.HasOne(x => x.Role)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Permission)
                    .WithMany(x => x.Roles)
                    .HasForeignKey(x => x.PermissionCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            Entity.User.ModelBuilder(modelBuilder);


            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
