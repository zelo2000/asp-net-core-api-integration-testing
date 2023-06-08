using Microsoft.EntityFrameworkCore;
using OZ.UserApi.Data.Users;

namespace OZ.UserApi.Data
{
    public class UserApiDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public UserApiDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUser();
        }
    }
}