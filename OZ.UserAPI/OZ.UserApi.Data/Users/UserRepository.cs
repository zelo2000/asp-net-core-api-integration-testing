using OZ.UserApi.Data.Base;

namespace OZ.UserApi.Data.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity> { }

    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(UserApiDbContext context) : base(context) { }
    }
}
