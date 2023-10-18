using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.Services.Auth
{
    public interface IAuthService
    {
        Task<User> LogIn(string email, string password);
    }
}
