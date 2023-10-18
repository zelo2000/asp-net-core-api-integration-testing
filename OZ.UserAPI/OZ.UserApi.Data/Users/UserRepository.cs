using Microsoft.EntityFrameworkCore;
using OZ.UserApi.Data.Base;
using OZ.UserApi.Data.Exceptions;

namespace OZ.UserApi.Data.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task SetUserImage(Guid userId, string? imageUrl);

        Task SetLastLogInDate(Guid userId, DateTime lastLogInDate);

        Task<UserEntity?> GetByEmailAndPasswordHash(string email, string passwordHash);
    }

    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(UserApiDbContext context) : base(context) { }

        public async Task<UserEntity?> GetByEmailAndPasswordHash(string email, string passwordHash)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
        }

        public async Task SetLastLogInDate(Guid userId, DateTime lastLogInDate)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new EntityNotFoundException($"Can not found user with id {userId}");

            user.LastLogIn = lastLogInDate;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task SetUserImage(Guid userId, string? imageUrl)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new EntityNotFoundException($"Can not found user with id {userId}");

            user.ImageUrl = imageUrl;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
        }
    }
}
