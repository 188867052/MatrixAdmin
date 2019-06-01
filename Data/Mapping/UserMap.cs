using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class UserMap
        : IEntityTypeConfiguration<Core.Data.Entities.User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.User> builder)
        {
            // table
            builder.ToTable("user", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.LoginName)
                .IsRequired()
                .HasColumnName("login_name")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.DisplayName)
                .HasColumnName("display_name")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Password)
                .HasColumnName("password")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.Avatar)
                .HasColumnName("avatar")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.UserType)
                .IsRequired()
                .HasColumnName("user_type")
                .HasColumnType("int");

            builder.Property(t => t.IsLocked)
                .IsRequired()
                .HasColumnName("is_locked")
                .HasColumnType("int");

            builder.Property(t => t.Status)
                .IsRequired()
                .HasColumnName("status")
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
                .IsRequired()
                .HasColumnName("update_time")
                .HasColumnType("datetime");

            builder.Property(t => t.UpdateByUserName)
                .HasColumnName("update_by_user_name")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(800)")
                .HasMaxLength(800);

            builder.Property(t => t.CreateByUserId)
                .IsRequired()
                .HasColumnName("create_by_user_id")
                .HasColumnType("uniqueidentifier");

            builder.Property(t => t.UpdateByUserId)
                .HasColumnName("update_by_user_id")
                .HasColumnType("uniqueidentifier");

            builder.Property(t => t.IsEnable)
                .IsRequired()
                .HasColumnName("is_enable")
                .HasColumnType("bit");

            builder.Property(t => t.IsDeleted)
                .IsRequired()
                .HasColumnName("is_deleted")
                .HasColumnType("bit");

            builder.Property(t => t.UserStatusId)
                .IsRequired()
                .HasColumnName("user_status_id")
                .HasColumnType("int")
                .HasDefaultValueSql("((1))");

            // relationships
            builder.HasOne(t => t.UserStatus)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserStatusId)
                .HasConstraintName("FK__user__user_status");
        }
    }
}
