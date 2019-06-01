using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class RolePermissionMappingMap
        : IEntityTypeConfiguration<Core.Data.Entities.RolePermissionMapping>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.RolePermissionMapping> builder)
        {
            // table
            builder.ToTable("role_permission_mapping", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.PermissionCode)
                .IsRequired()
                .HasColumnName("permission_code")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.CreateTime)
                .IsRequired()
                .HasColumnName("create_time")
                .HasColumnType("datetime2");

            builder.Property(t => t.RoleId)
                .HasColumnName("role_id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.CodePermission)
                .WithMany(t => t.CodeRolePermissionMappings)
                .HasForeignKey(d => d.PermissionCode)
                .HasConstraintName("FK_RolePermissionMapping_Permission_PermissionCode");

            builder.HasOne(t => t.Role)
                .WithMany(t => t.RolePermissionMappings)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RolePermissionMapping_Permission_Role");
        }
    }
}
