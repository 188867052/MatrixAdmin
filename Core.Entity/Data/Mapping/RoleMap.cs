using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class RoleMap
        : IEntityTypeConfiguration<Core.Data.Entities.Role>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.Role> builder)
        {
            // table
            builder.ToTable("role", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.CreateTime)
                .IsRequired()
                .HasColumnName("create_time")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.CreateByUserName)
                .HasColumnName("create_by_user_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.UpdateTime)
                .IsRequired()
                .HasColumnName("update_time")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.UpdateByUserName)
                .HasColumnName("update_by_user_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.IsSuperAdministrator)
                .IsRequired()
                .HasColumnName("is_super_administrator")
                .HasColumnType("bit");

            builder.Property(t => t.IsEnable)
                .IsRequired()
                .HasColumnName("is_enable")
                .HasColumnType("bit");

            builder.Property(t => t.IsForbidden)
                .IsRequired()
                .HasColumnName("is_forbidden")
                .HasColumnType("bit");

            builder.Property(t => t.UpdateByUserId)
                .IsRequired()
                .HasColumnName("update_by_user_id")
                .HasColumnType("int");

            builder.Property(t => t.CreateByUserId)
                .IsRequired()
                .HasColumnName("create_by_user_id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.CreateByUser)
                .WithMany(t => t.CreateByRoles)
                .HasForeignKey(d => d.CreateByUserId)
                .HasConstraintName("FK_Role_User_CreateBy");

            builder.HasOne(t => t.UpdateByUser)
                .WithMany(t => t.UpdateByRoles)
                .HasForeignKey(d => d.UpdateByUserId)
                .HasConstraintName("FK_Role_User_ModifiedBy");
        }
    }
}
