using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class IconMap
        : IEntityTypeConfiguration<Core.Data.Entities.Icon>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.Icon> builder)
        {
            // table
            builder.ToTable("icon", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Code)
                .IsRequired()
                .HasColumnName("code")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Size)
                .HasColumnName("size")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.Color)
                .HasColumnName("color")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Custom)
                .HasColumnName(" custom")
                .HasColumnType("nvarchar(60)")
                .HasMaxLength(60);

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.CreateTime)
                .IsRequired()
                .HasColumnName("create_time")
                .HasColumnType("datetime2");

            builder.Property(t => t.CreateByUserName)
                .HasColumnName("create_by_user_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.UpdateTime)
                .HasColumnName("update_time")
                .HasColumnType("datetime2");

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

            // relationships
        }
    }
}
