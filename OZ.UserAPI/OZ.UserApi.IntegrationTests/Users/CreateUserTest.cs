using AutoFixture;
using Microsoft.AspNetCore.Http;
using OZ.UserApi.IntegrationTests.Users.Base;
using OZ.UserApi.Services.Users.Models;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OZ.UserApi.IntegrationTests.Users
{
    public class CreateUserTest : IClassFixture<UserApiFactory>
    {
        private readonly HttpClient _client;
        private readonly Fixture _fixture;

        public CreateUserTest(UserApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Create_CreateUser_DataIsValid()
        {
            // Arrange
            var payload = _fixture.Build<UserPayload>()
                .With(u => u.Email, UserEmail.Default)
                .Create();

            // Act
            var response = await _client.PostAsJsonAsync("api/v1.0/user", payload);
            var userResponse = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            userResponse.ShouldNotBeNull();
            userResponse.FirstName.ShouldBe(payload.FirstName);
            userResponse.LastName.ShouldBe(payload.LastName);
            userResponse.Email.ShouldBe(payload.Email);
        }

        [Fact]
        public async Task Create_ValidationError_InvalidEmail()
        {
            // Arrange
            var payload = _fixture.Build<UserPayload>().Create();

            // Act
            var response = await _client.PostAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe("'Email' is not a valid email address.");
        }

        [Fact]
        public async Task Create_ValidationError_EmptyFirstName()
        {
            // Arrange
            var payload = _fixture.Build<UserPayload>()
                .With(u => u.Email, UserEmail.Default)
                .With(u => u.FirstName, "")
                .Create();

            // Act
            var response = await _client.PostAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe("'First Name' must not be empty.");
        }

        [Fact]
        public async Task Create_ValidationError_EmptyLastName()
        {
            // Arrange
            var payload = _fixture.Build<UserPayload>()
                .With(u => u.Email, UserEmail.Default)
                .With(u => u.LastName, "")
                .Create();

            // Act
            var response = await _client.PostAsJsonAsync("api/v1.0/user", payload);
            var errorMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorMessage.ShouldNotBeNull();
            errorMessage.ShouldBe("'Last Name' must not be empty.");
        }
    }
}