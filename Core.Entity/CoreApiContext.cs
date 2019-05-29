using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Entity
{
    public partial class CoreApiContext : DbContext
    {
        public CoreApiContext()
        {
        }

        public CoreApiContext(DbContextOptions<CoreApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Icon> Icon { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MultiplePrimaryKeyTable> MultiplePrimaryKeyTable { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermissionMapping> RolePermissionMapping { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;App=EntityFrameworkCore;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1");

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.ToTable("configuration");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Icon>(entity =>
            {
                entity.ToTable("icon");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(50);

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50);

                entity.Property(e => e.CreateByUserId)
                    .HasColumnName("create_by_user_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateByUserName).HasColumnName("create_by_user_name");

                entity.Property(e => e.CreateTime).HasColumnName("create_time");

                entity.Property(e => e.Custom)
                    .HasColumnName(" custom")
                    .HasMaxLength(60);

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IsEnable).HasColumnName("is_enable");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(20);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateByUserId)
                    .HasColumnName("update_by_user_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateByUserName).HasColumnName("update_by_user_name");

                entity.Property(e => e.UpdateTime).HasColumnName("update_time");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LogLevel).HasColumnName("log_level");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.SqlOperateType).HasColumnName("sql_operate_type");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alias)
                    .HasColumnName("alias")
                    .HasMaxLength(255);

                entity.Property(e => e.CreateByUserId)
                    .HasColumnName("create_by_user_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateByUserName).HasColumnName("create_by_user_name");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(800);

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(128);

                entity.Property(e => e.IsDefaultRouter).HasColumnName("is_default_router");

                entity.Property(e => e.IsEnable).HasColumnName("is_enable");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ParentName).HasColumnName("parent_name");

                entity.Property(e => e.Sort).HasColumnName("sort");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateByUserId)
                    .HasColumnName("update_by_user_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateByUserName).HasColumnName("update_by_user_name");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(255);

                entity.HasOne(d => d.CreateByUser)
                    .WithMany(p => p.MenuCreateByUser)
                    .HasForeignKey(d => d.CreateByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_User_Create");

                entity.HasOne(d => d.UpdateByUser)
                    .WithMany(p => p.MenuUpdateByUser)
                    .HasForeignKey(d => d.UpdateByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_User");
            });

            modelBuilder.Entity<MultiplePrimaryKeyTable>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Name })
                    .HasName("pk_name");

                entity.ToTable("multiple_primary_key_table");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(20);

                entity.Property(e => e.ActionCode)
                    .IsRequired()
                    .HasColumnName("action_code")
                    .HasMaxLength(80);

                entity.Property(e => e.CreateByUserId)
                    .HasColumnName("create_by_user_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateByUserName).HasColumnName("create_by_user_name");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Icon).HasColumnName("icon");

                entity.Property(e => e.IsEnable).HasColumnName("is_enable");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UpdateByUserId)
                    .HasColumnName("update_by_user_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateByUserName).HasColumnName("update_by_user_name");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Menu");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateByUserId).HasColumnName("create_by_user_id");

                entity.Property(e => e.CreateByUserName).HasColumnName("create_by_user_name");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IsEnable).HasColumnName("is_enable");

                entity.Property(e => e.IsForbidden).HasColumnName("is_forbidden");

                entity.Property(e => e.IsSuperAdministrator).HasColumnName("is_super_administrator");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateByUserId).HasColumnName("update_by_user_id");

                entity.Property(e => e.UpdateByUserName).HasColumnName("update_by_user_name");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreateByUser)
                    .WithMany(p => p.RoleCreateByUser)
                    .HasForeignKey(d => d.CreateByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_User_CreateBy");

                entity.HasOne(d => d.UpdateByUser)
                    .WithMany(p => p.RoleUpdateByUser)
                    .HasForeignKey(d => d.UpdateByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_User_ModifiedBy");
            });

            modelBuilder.Entity<RolePermissionMapping>(entity =>
            {
                entity.ToTable("role_permission_mapping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime).HasColumnName("create_time");

                entity.Property(e => e.PermissionCode)
                    .IsRequired()
                    .HasColumnName("permission_code")
                    .HasMaxLength(20);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.PermissionCodeNavigation)
                    .WithMany(p => p.RolePermissionMapping)
                    .HasForeignKey(d => d.PermissionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermissionMapping_Permission_PermissionCode");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissionMapping)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermissionMapping_Permission_Role");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasMaxLength(255);

                entity.Property(e => e.CreateByUserId).HasColumnName("create_by_user_id");

                entity.Property(e => e.CreateByUserName).HasColumnName("create_by_user_name");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(800);

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasMaxLength(50);

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.IsEnable).HasColumnName("is_enable");

                entity.Property(e => e.IsLocked).HasColumnName("is_locked");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasColumnName("login_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateByUserId).HasColumnName("update_by_user_id");

                entity.Property(e => e.UpdateByUserName).HasColumnName("update_by_user_name");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserStatusId)
                    .HasColumnName("user_status_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user__user_status");
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.ToTable("user_role_mapping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleMapping_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleMapping_User");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("user_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
