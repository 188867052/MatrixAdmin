using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Data
{
    public partial class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Core.Data.Entities.RolePermissionMapping> RolePermissionMapping { get; set; }

        public virtual DbSet<Core.Data.Entities.Permission> Permission { get; set; }

        public virtual DbSet<Core.Data.Entities.Role> Role { get; set; }

        public virtual DbSet<Core.Data.Entities.UserRoleMapping> UserRoleMapping { get; set; }

        public virtual DbSet<Core.Data.Entities.User> User { get; set; }

        public virtual DbSet<Core.Data.Entities.UserStatus> UserStatus { get; set; }

        public virtual DbSet<Core.Data.Entities.MultiplePrimaryKeyTable> MultiplePrimaryKeyTable { get; set; }

        public virtual DbSet<Core.Data.Entities.Status> Status { get; set; }

        public virtual DbSet<Core.Data.Entities.Icon> Icon { get; set; }

        public virtual DbSet<Core.Data.Entities.Menu> Menu { get; set; }

        public virtual DbSet<Core.Data.Entities.Configuration> Configuration { get; set; }

        public virtual DbSet<Core.Data.Entities.Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.ConfigurationMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.IconMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.LogMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.MenuMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.MultiplePrimaryKeyTableMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.PermissionMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.RoleMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.RolePermissionMappingMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.UserRoleMappingMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.UserStatusMap());
        }
    }
}
