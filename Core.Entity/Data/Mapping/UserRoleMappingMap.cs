using Microsoft.EntityFrameworkCore;

namespace Core.Data.Mapping
{
    public partial class UserRoleMappingMap
        : IEntityTypeConfiguration<Core.Data.Entities.UserRoleMapping>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Data.Entities.UserRoleMapping> builder)
        {
            // table
            builder.ToTable("user_role_mapping", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("int");

            builder.Property(t => t.RoleId)
                .IsRequired()
                .HasColumnName("role_id")
                .HasColumnType("int");

            builder.Property(t => t.CreateTime)
                .IsRequired()
                .HasColumnName("create_time")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            // relationships
            builder.HasOne(t => t.Role)
                .WithMany(t => t.UserRoleMappings)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRoleMapping_Role");

            builder.HasOne(t => t.User)
                .WithMany(t => t.UserRoleMappings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRoleMapping_User");
        }
    }
}
