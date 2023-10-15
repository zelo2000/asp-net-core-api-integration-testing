using OZ.UserApi.Data.Users;
using OZ.UserApi.IntegrationTests.Users.Base;
using Shouldly;
using System.Net;

namespace OZ.UserApi.IntegrationTests.Users
{
    public class DeleteUserTest : IClassFixture<UserApiFactory>
    {
        private readonly HttpClient _client;
        private readonly List<UserEntity> _users;

        public DeleteUserTest(UserApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
            var userInitializer = new UserFactory(apiFactory.Services);
            _users = userInitializer.CreateAndInsertUserList().Result;
        }

        [Fact]
        public async Task Delete_Successfully_UserExist()
        {
            // Arrange
            var user = _users.First();

            // Act
            var response = await _client.DeleteAsync($"api/v1.0/user/{user.Id}");
            var userResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            userResponse.ShouldBeEmpty();
        }

        [Fact]
        public async Task Update_NotFound_UserDoNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"api/v1.0/user/{userId}");
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe($"User with id {userId} not found");
        }
    }
}
