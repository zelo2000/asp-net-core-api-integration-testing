using Microsoft.EntityFrameworkCore;

namespace OZ.UserApi.Data.Users
{
    internal static class UserEntityConfiguration
    {
        public static ModelBuilder ConfigureUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("User", "dbo");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirstName)
                    .HasMaxLength(150)
                    .IsRequired(false);

                entity.Property(x => x.LastName)
                    .HasMaxLength(150)
                    .IsRequired(false);

                entity.Property(x => x.Email)
                    .IsRequired()
                .HasMaxLength(320);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("getutcdate()");

                entity.Property(x => x.ModifiedAt)
                    .HasDefaultValueSql("getutcdate()");
            });

            return modelBuilder;
        }
    }
}
