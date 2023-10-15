using Microsoft.AspNetCore.Http;

namespace OZ.UserApi.Services.Images
{
    /// <summary>
    /// Interface for working with blob storage.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Get file from storage
        /// </summary>
        /// <param name="name">File name.</param>
        /// <returns>File as byte array.</returns>
        public Task<byte[]> GetFile(string name);

        /// <summary>
        /// Upload file to storage.
        /// </summary>
        /// <param name="name">File name.</param>
        /// <param name="file">File.</param>
        /// <returns>File name.</returns>
        public Task<string> UploadFile(string name, IFormFile file);

        /// <summary>
        /// Delete file from storage.
        /// </summary>
        /// <param name="name">File name.</param>
        /// <returns>True if deleted and false if not.</returns>
        public Task<bool> DeleteFile(string name);
    }
}
