using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class LogMap
        : IEntityTypeConfiguration<Core.Data.Entities.Log>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.Log> builder)
        {
            // table
            builder.ToTable("log", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.CreateTime)
                .IsRequired()
                .HasColumnName("create_time")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.Message)
                .HasColumnName("message")
                .HasColumnType("text");

            builder.Property(t => t.LogLevel)
                .IsRequired()
                .HasColumnName("log_level")
                .HasColumnType("int");

            builder.Property(t => t.SqlOperateType)
                .IsRequired()
                .HasColumnName("sql_operate_type")
                .HasColumnType("int");

            // relationships
        }
    }
}
