using Microsoft.AspNetCore.Http;
using OZ.UserApi.Data.Users;
using OZ.UserApi.Services.Images;

namespace OZ.UserApi.Services.UserImages
{
    public class UserImageService : IUserImageService
    {
        private readonly IStorageService _storageService;
        private readonly IUserRepository _userRepository;

        public UserImageService(IStorageService storageService, IUserRepository userRepository)
        {
            _storageService = storageService;
            _userRepository = userRepository;
        }

        public async Task<string> UploadImage(Guid userId, IFormFile file)
        {
            var fileName = $"{userId}{Path.GetExtension(file.FileName)}";
            fileName = await _storageService.UploadFile(fileName, file);
            await _userRepository.SetUserImage(userId, fileName);
            return fileName;
        }

        public async Task DeleteImage(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception($"Can not found user with id {userId}");

            if (!string.IsNullOrWhiteSpace(user.ImageUrl))
            {
                await _storageService.DeleteFile(user.ImageUrl);
                await _userRepository.SetUserImage(userId, null);
            }
        }
    }
}
