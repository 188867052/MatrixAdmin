using System;
using System.Data.Common;
using Core.Model.Administration.Icon;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Permission;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Permission> Permission { get; set; }

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
                    x.UserGuid,
                    x.RoleCode
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

            modelBuilder.Entity<Permission>(entity =>
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
                    x.RoleCode,
                    x.PermissionCode
                });

                entity.HasOne(x => x.Role)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.RoleCode)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Permission)
                    .WithMany(x => x.Roles)
                    .HasForeignKey(x => x.PermissionCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.Property(e => e.Avatar).HasMaxLength(255);

            //    entity.Property(e => e.Description).HasMaxLength(800);

            //    entity.Property(e => e.DisplayName).HasMaxLength(50);

            //    entity.Property(e => e.LoginName)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //    entity.Property(e => e.Password).HasMaxLength(255);

            //    entity.Property(e => e.UserStatusId).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.UserStatus);
            //    //.WithMany(p => p.User)
            //    //.HasForeignKey(d => d.UserStatusId)
            //    //.OnDelete(DeleteBehavior.ClientSetNull)
            //    //.HasConstraintName("FK__User__UserStatus");
            //});
            Administration.User.User.ModelBuilder(modelBuilder);


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
