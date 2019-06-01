using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class SchemaVersionsMap
        : IEntityTypeConfiguration<Core.Data.Entities.SchemaVersions>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.SchemaVersions> builder)
        {
            // table
            builder.ToTable("SchemaVersions", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ScriptName)
                .IsRequired()
                .HasColumnName("ScriptName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.Applied)
                .IsRequired()
                .HasColumnName("Applied")
                .HasColumnType("datetime");

            // relationships
        }
    }
}
