using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OZ.UserApi.Data.Exceptions;
using OZ.UserApi.Services.Storage.Models;

namespace OZ.UserApi.Services.Images
{
    ///<inheritdoc/>
    public class StorageService : IStorageService
    {
        private readonly string _storageConnectionString;
        private readonly StorageConfiguration _storageOptions;
        private readonly BlobContainerClient _containerClient;

        public StorageService(IConfiguration configuration, IOptions<StorageConfiguration> storageOptions)
        {
            _storageConnectionString = configuration.GetConnectionString("AzureBlobStorage");
            _storageOptions = storageOptions.Value;

            // Create blob container client
            _containerClient = new BlobContainerClient(_storageConnectionString, _storageOptions.ContainerName);
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteFile(string name)
        {
            // Get blob client
            var blob = _containerClient.GetBlobClient(name);

            if (!blob.Exists())
            {
                throw new EntityNotFoundException($"Image with {name} was not exist");
            }

            // Delete image from blob
            var response = await blob.DeleteAsync();
            if (response.Status >= 200 && response.Status < 300)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        ///<inheritdoc/>
        public async Task<byte[]> GetFile(string name)
        {
            try
            {
                // Get blob client
                var blob = _containerClient.GetBlobClient(name);

                // Create stream
                using var stream = new MemoryStream();

                // Get response
                var response = await blob.DownloadToAsync(stream);
                if (response.Status >= 200 && response.Status < 300)
                {
                    var result = stream.ToArray();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch
            {
                throw;
            }
        }

        ///<inheritdoc/>
        public async Task<string> UploadFile(string name, IFormFile file)
        {
            try
            {
                // Create blob client
                BlobClient blob = _containerClient.GetBlobClient(name);

                // Open file stream
                using var fileStream = file.OpenReadStream();

                // Upload file from API
                await blob.UploadAsync(fileStream);

                return name;
            }
            catch
            {
                throw;
            }
        }
    }
}
