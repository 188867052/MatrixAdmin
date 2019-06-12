using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class UserStatusMap
        : IEntityTypeConfiguration<Core.Data.Entities.UserStatus>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.UserStatus> builder)
        {
            // table
            builder.ToTable("user_status", "dbo");

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
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            // relationships
        }
    }
}
