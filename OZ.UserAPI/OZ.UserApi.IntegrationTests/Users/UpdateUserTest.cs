using OZ.UserApi.Services.Users.Models;
using System.Net.Http.Json;
using System.Net;
using Shouldly;
using OZ.UserApi.IntegrationTests.Users.Base;
using OZ.UserApi.Data.Users;

namespace OZ.UserApi.IntegrationTests.Users
{
    public class UpdateUserTest : IClassFixture<UserApiFactory>
    {
        private readonly HttpClient _client;
        private readonly List<UserEntity> _users;

        public UpdateUserTest(UserApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
            var userInitializer = new UserFactory(apiFactory.Services);
            _users = userInitializer.CreateAndInsertUserList().Result;
        }

        [Fact]
        public async Task Update_UpdateUser_DataIsValid()
        {
            // Arrange
            var user = _users.First();
            var payload = new UserPayload
            {
                Id = user.Id,
                FirstName = "FirstName",
                LastName = "LastName",
                Email = UserEmail.Default,
            };

            // Act
            var response = await _client.PutAsJsonAsync("api/v1.0/user", payload);
            var userResponse = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            userResponse.ShouldNotBeNull();
            userResponse.FirstName.ShouldBe(payload.FirstName);
            userResponse.LastName.ShouldBe(payload.LastName);
            userResponse.Email.ShouldBe(payload.Email);
        }

        [Fact]
        public async Task Update_NotFound_UserDoNotExist()
        {
            // Arrange
            var user = _users.First();
            var payload = new UserPayload
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            // Act
            var response = await _client.PutAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe($"User with id {payload.Id} not found");
        }

        [Fact]
        public async Task Update_BadRequest_UserIdIsNull()
        {
            // Arrange
            var user = _users.First();
            var payload = new UserPayload
            {
                Id = null,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            // Act
            var response = await _client.PutAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe($"User id is null");
        }

        [Fact]
        public async Task Update_ValidationError_InvalidEmail()
        {
            // Arrange
            var user = _users.First();
            var payload = new UserPayload
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = "email"
            };

            // Act
            var response = await _client.PutAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe("'Email' is not a valid email address.");
        }

        [Fact]
        public async Task Update_ValidationError_EmptyFirstName()
        {
            // Arrange
            var user = _users.First();
            var payload = new UserPayload
            {
                Id = user.Id,
                FirstName = "",
                LastName = user.LastName,
                Email = user.Email
            };

            // Act
            var response = await _client.PutAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe("'First Name' must not be empty.");
        }

        [Fact]
        public async Task Update_ValidationError_EmptyLastName()
        {
            // Arrange
            var user = _users.First();
            var payload = new UserPayload
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = "",
                Email = user.Email,
            };

            // Act
            var response = await _client.PutAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe("'Last Name' must not be empty.");
        }
    }
}
