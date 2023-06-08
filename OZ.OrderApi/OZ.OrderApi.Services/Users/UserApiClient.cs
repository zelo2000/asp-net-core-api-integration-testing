using OZ.OrderApi.Services.Users.Models;
using System.Net;
using System.Net.Http.Json;

namespace OZ.OrderApi.Services.Users
{
    public interface IUserApiClient
    {
        Task<User?> GetById(Guid userId);
    }

    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;

        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User?> GetById(Guid userId)
        {
            var response = await _httpClient.GetAsync($"user/{userId}");

            return response.StatusCode == HttpStatusCode.OK
                ? await response.Content.ReadFromJsonAsync<User>()
                : null;
        }
    }
}
