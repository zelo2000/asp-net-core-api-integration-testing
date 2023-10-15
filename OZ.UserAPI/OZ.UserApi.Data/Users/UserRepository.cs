using Microsoft.EntityFrameworkCore;
using OZ.UserApi.Data.Base;
using OZ.UserApi.Data.Exceptions;

namespace OZ.UserApi.Data.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task SetUserImage(Guid userId, string? imageUrl);
    }

    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(UserApiDbContext context) : base(context) { }

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
