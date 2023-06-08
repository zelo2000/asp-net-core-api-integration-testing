using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.Services.Users
{
    /// <summary>
    /// User service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <returns>List of users</returns>
        Task<IEnumerable<User>> GetUsers();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        Task<User?> GetUserById(Guid userId);

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="payload">User payload</param>
        /// <returns>Created user</returns>
        Task<User> CreateUser(UserPayload payload);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="payload">User payload</param>
        /// <returns>Updated user</returns>
        Task<User> UpdateUser(UserPayload payload);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Task</returns>
        Task DeleteUser(Guid userId);
    }
}
