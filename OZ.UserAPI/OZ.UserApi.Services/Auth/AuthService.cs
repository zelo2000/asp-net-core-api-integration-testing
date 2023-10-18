using OZ.UserApi.Data.Users;
using OZ.UserApi.Services.Auth.Helpers;
using OZ.UserApi.Services.Users.Mappers;
using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashGenerator _hashGenerator;

        public AuthService(IUserRepository userRepository, IHashGenerator hashGenerator)
        {
            _userRepository = userRepository;
            _hashGenerator = hashGenerator;
        }

        public async Task<User> LogIn(string email, string password)
        {
            var passwordHash = _hashGenerator.GetPasswordHash(password);
            var user = await _userRepository.GetByEmailAndPasswordHash(email, passwordHash)
                ?? throw new ApplicationException($"Login failed for user with email {email}");

            await _userRepository.SetLastLogInDate(user.Id, DateTime.UtcNow);
            return user.ToDomain();
        }
    }
}
