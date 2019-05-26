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
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1");

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Key).HasMaxLength(50);
            });

            modelBuilder.Entity<Icon>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.CreateByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Custom).HasMaxLength(60);

                entity.Property(e => e.Size).HasMaxLength(20);

                entity.Property(e => e.UpdateByUserId).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message).HasColumnType("text");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.CreateByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.Icon).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Url).HasMaxLength(255);

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

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(20);

                entity.Property(e => e.ActionCode)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.CreateByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MenuId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Menu");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreateByUser)
                    .WithMany(p => p.RoleCreateByUser)
                    .HasForeignKey(d => d.CreateByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_User_CreateBy");

                entity.HasOne(d => d.ModifiedByUser)
                    .WithMany(p => p.RoleModifiedByUser)
                    .HasForeignKey(d => d.ModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_User_ModifiedBy");
            });

            modelBuilder.Entity<RolePermissionMapping>(entity =>
            {
                entity.ToTable("role_permission_mapping");

                entity.Property(e => e.PermissionCode)
                    .IsRequired()
                    .HasMaxLength(20);

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
                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasMaxLength(255);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasMaxLength(50);

                entity.Property(e => e.IsLocked).HasColumnName("is_locked");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasColumnName("login_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UserStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__UserStatus");
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
