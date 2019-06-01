using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class MenuMap
        : IEntityTypeConfiguration<Core.Data.Entities.Menu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.Menu> builder)
        {
            // table
            builder.ToTable("menu", "dbo");

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

            builder.Property(t => t.Url)
                .HasColumnName("url")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.Alias)
                .HasColumnName("alias")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.Icon)
                .HasColumnName("icon")
                .HasColumnType("nvarchar(128)")
                .HasMaxLength(128);

            builder.Property(t => t.ParentName)
                .HasColumnName("parent_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Level)
                .IsRequired()
                .HasColumnName("level")
                .HasColumnType("int");

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(800)")
                .HasMaxLength(800);

            builder.Property(t => t.Sort)
                .IsRequired()
                .HasColumnName("sort")
                .HasColumnType("int");

            builder.Property(t => t.IsDefaultRouter)
                .IsRequired()
                .HasColumnName("is_default_router")
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

            builder.Property(t => t.ParentId)
                .IsRequired()
                .HasColumnName("parent_id")
                .HasColumnType("int")
                .HasDefaultValueSql("((1))");

            // relationships
            builder.HasOne(t => t.UpdateByUser)
                .WithMany(t => t.UpdateByMenus)
                .HasForeignKey(d => d.UpdateByUserId)
                .HasConstraintName("FK_Menu_User");

            builder.HasOne(t => t.CreateByUser)
                .WithMany(t => t.CreateByMenus)
                .HasForeignKey(d => d.CreateByUserId)
                .HasConstraintName("FK_Menu_User_Create");
        }
    }
}
