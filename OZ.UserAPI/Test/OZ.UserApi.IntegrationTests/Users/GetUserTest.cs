using OZ.UserApi.Data.Users;
using OZ.UserApi.IntegrationTests.Users.Base;
using OZ.UserApi.Services.Users.Models;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OZ.UserApi.IntegrationTests.Users
{
    public class GetUserTest : IClassFixture<UserApiFactory>
    {
        private readonly HttpClient _client;
        private readonly List<UserEntity> _users;

        public GetUserTest(UserApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
            var userInitializer = new UserFactory(apiFactory.Services);
            _users = userInitializer.CreateAndInsertUserList(2).Result;
        }

        [Fact]
        public async Task GetUsers_UsersExist_UserList()
        {
            // Act
            var response = await _client.GetAsync($"api/v1.0/user");
            var userResponse = await response.Content.ReadFromJsonAsync<List<User>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            userResponse.ShouldNotBeNull();
            userResponse.Count.ShouldBe(_users.Count);
        }
    }
}
