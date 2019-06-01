using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class MultiplePrimaryKeyTableMap
        : IEntityTypeConfiguration<Core.Data.Entities.MultiplePrimaryKeyTable>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.MultiplePrimaryKeyTable> builder)
        {
            // table
            builder.ToTable("multiple_primary_key_table", "dbo");

            // key
            builder.HasKey(t => new { t.Id, t.Name });

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
        }

    }
}
