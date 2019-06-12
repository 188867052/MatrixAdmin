using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class ConfigurationMap
        : IEntityTypeConfiguration<Core.Data.Entities.Configuration>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.Configuration> builder)
        {
            // table
            builder.ToTable("configuration", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Value)
                .HasColumnName("value")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Key)
                .HasColumnName("key")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
        }
    }
}
