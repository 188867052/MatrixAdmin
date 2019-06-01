using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class PermissionMap
        : IEntityTypeConfiguration<Core.Data.Entities.Permission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.Permission> builder)
        {
            // table
            builder.ToTable("permission", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.ActionCode)
                .IsRequired()
                .HasColumnName("action_code")
                .HasColumnType("nvarchar(80)")
                .HasMaxLength(80);

            builder.Property(t => t.Icon)
                .HasColumnName("icon")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnName("type")
                .HasColumnType("int");

            builder.Property(t => t.CreateTime)
                .IsRequired()
                .HasColumnName("create_time")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.CreateByUserName)
                .HasColumnName("create_by_user_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.UpdateTime)
                .HasColumnName("update_time")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.UpdateByUserName)
                .HasColumnName("update_by_user_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.IsEnable)
                .IsRequired()
                .HasColumnName("is_enable")
                .HasColumnType("bit");

            builder.Property(t => t.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasColumnType("bit");

            builder.Property(t => t.CreateByUserId)
                .IsRequired()
                .HasColumnName("create_by_user_id")
                .HasColumnType("int")
                .HasDefaultValueSql("((1))");

            builder.Property(t => t.UpdateByUserId)
                .IsRequired()
                .HasColumnName("update_by_user_id")
                .HasColumnType("int")
                .HasDefaultValueSql("((1))");

            builder.Property(t => t.MenuId)
                .IsRequired()
                .HasColumnName("menu_id")
                .HasColumnType("int")
                .HasDefaultValueSql("((1))");

            // relationships
            builder.HasOne(t => t.Menu)
                .WithMany(t => t.Permissions)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_Permission_Menu");
        }
    }
}
