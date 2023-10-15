using Microsoft.AspNetCore.Http;

namespace OZ.UserApi.Services.UserImages
{
    public interface IUserImageService
    {
        Task<string> UploadImage(Guid userId, IFormFile file);

        Task DeleteImage(Guid userId);
    }
}
