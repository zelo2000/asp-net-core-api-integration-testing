﻿using Microsoft.Extensions.Options;
using OZ.UserApi.Data.Exceptions;
using OZ.UserApi.Data.Users;
using OZ.UserApi.Services.Storage.Models;
using OZ.UserApi.Services.Users.Mappers;
using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.Services.Users
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly StorageConfiguration _storageOptions;

        public UserService(IUserRepository userRepository, IOptions<StorageConfiguration> storageOptions)
        {
            _userRepository = userRepository;
            _storageOptions = storageOptions.Value;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => u.ToDomain().WithImageUrl(_storageOptions));
        }

        /// <inheritdoc/>
        public async Task<User?> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user?.ToDomain().WithImageUrl(_storageOptions);
        }

        /// <inheritdoc/>
        public async Task<User> CreateUser(UserPayload payload)
        {
            var entity = payload.ToEntity();
            entity.PasswordHash = payload.Password;

            var user = await _userRepository.AddAsync(entity);
            return user.ToDomain().WithImageUrl(_storageOptions);
        }

        /// <inheritdoc/>
        public async Task<User> UpdateUser(UserPayload payload)
        {
            if (payload?.Id == null)
                throw new ApplicationException("User id is null");

            var entity = await _userRepository.GetByIdAsync(payload.Id.Value)
                ?? throw new EntityNotFoundException($"User with id {payload.Id.Value} not found");

            entity.FirstName = payload.FirstName;
            entity.LastName = payload.LastName;
            entity.Email = payload.Email;

            var user = await _userRepository.UpdateAsync(entity);
            return user.ToDomain().WithImageUrl(_storageOptions);
        }

        /// <inheritdoc/>
        public async Task DeleteUser(Guid userId)
        {
            var entity = await _userRepository.GetByIdAsync(userId)
                ?? throw new EntityNotFoundException($"User with id {userId} not found");

            await _userRepository.DeleteAsync(entity);
        }
    }
}
