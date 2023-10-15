using OZ.UserApi.Data.Users;
using OZ.UserApi.IntegrationTests.Users.Base;
using OZ.UserApi.Services.Users.Models;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OZ.UserApi.IntegrationTests.Users
{
    public class GetUserByIdTest : IClassFixture<UserApiFactory>
    {
        private readonly HttpClient _client;
        private readonly List<UserEntity> _users;

        public GetUserByIdTest(UserApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
            var userInitializer = new UserFactory(apiFactory.Services);
            _users = userInitializer.CreateAndInsertUserList(2).Result;
        }

        [Fact]
        public async Task GetUserById_User_UserIdExist()
        {
            // Arrange
            var user = _users.First();

            // Act
            var response = await _client.GetAsync($"api/v1.0/user/{user.Id}");
            var userResponse = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            userResponse.ShouldNotBeNull();
            userResponse.Id.ShouldBe(user.Id);
        }

        [Fact]
        public async Task GetUserById_EmptyResponse_UserIdDoNotExist()
        {
            // Act
            var response = await _client.GetAsync($"api/v1.0/user/{Guid.NewGuid()}");
            var userResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
            userResponse.ShouldBeEmpty();
        }
    }
}
