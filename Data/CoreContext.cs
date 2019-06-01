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

        public virtual DbSet<Core.Data.Entities.RolePermissionMapping> RolePermissionMappings { get; set; }

        public virtual DbSet<Core.Data.Entities.Permission> Permissions { get; set; }

        public virtual DbSet<Core.Data.Entities.Role> Roles { get; set; }

        public virtual DbSet<Core.Data.Entities.UserRoleMapping> UserRoleMappings { get; set; }

        public virtual DbSet<Core.Data.Entities.User> Users { get; set; }

        public virtual DbSet<Core.Data.Entities.UserStatus> UserStatuses { get; set; }

        public virtual DbSet<Core.Data.Entities.MultiplePrimaryKeyTable> MultiplePrimaryKeyTables { get; set; }

        public virtual DbSet<Core.Data.Entities.Icon> Icons { get; set; }

        public virtual DbSet<Core.Data.Entities.Menu> Menus { get; set; }

        public virtual DbSet<Core.Data.Entities.Configuration> Configurations { get; set; }

        public virtual DbSet<Core.Data.Entities.Log> Logs { get; set; }

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
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.UserRoleMappingMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Mapping.UserStatusMap());
        }
    }
}
