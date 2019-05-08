using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public partial class User
    {
        public Role Role
        {
            get
            {
                return UserRoleMapping?.Role;
            }
        }

        public UserRoleMapping UserRoleMapping
        {
            get
            {
               return UserRoles?.FirstOrDefault(u => u.User.Id == this.Id);
            }
        }

        public string RoleName => Role?.Name;

        public static void ModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(800);
                entity.Property(e => e.DisplayName).HasMaxLength(50);
                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(255);
                //entity.Property(e => e.UserStatusId).HasDefaultValueSql("((1))");
                entity.HasOne(d => d.UserStatus);
                //.WithMany(p => p.User)
                //.HasForeignKey(d => d.UserStatusId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK__User__UserStatus");
            });
        }
    }
}