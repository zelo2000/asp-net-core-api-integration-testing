using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using OZ.OrderApi.Services.Orders.Models;
using OZ.OrderApi.Services.Users.Models;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace OZ.OrderApi.IntegrationTests.Orders
{
    public class GetOrderByUserIdTest : IClassFixture<OrderApiFactory>
    {
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly WireMockServer _wireMockServer;

        private readonly Guid _userId = Guid.Parse("52d7c2a5-3b7f-4f6e-a7ff-08db684d3460");

        public GetOrderByUserIdTest(OrderApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
            _wireMockServer = apiFactory.Services.GetRequiredService<WireMockServer>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetOrderByUserId_OrderList_OrderIdExist()
        {
            // Arrange
            var user = _fixture.Build<User>()
                .With(u => u.Id, _userId)
                .Create();

            _wireMockServer.Given(Request.Create().WithPath($"/user/{_userId}"))
                .RespondWith(Response.Create().WithBodyAsJson(user).WithStatusCode(HttpStatusCode.OK));

            // Act
            var response = await _client.GetAsync($"api/v1.0/order/user/{_userId}");
            var userResponse = await response.Content.ReadFromJsonAsync<List<Order>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            userResponse.ShouldNotBeNull();
            userResponse.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderByUserId_OrderWithoutUser_UserDoNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = _fixture.Build<User>()
                .With(u => u.Id, _userId)
                .Create();

            _wireMockServer.Given(Request.Create().WithPath($"/user/{userId}"))
                .RespondWith(Response.Create().WithBodyAsJson(user).WithStatusCode(HttpStatusCode.OK));

            // Act
            var response = await _client.GetAsync($"api/v1.0/order/user/{_userId}");
            var userResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            userResponse.ShouldBe($"User with id {_userId} not found");
        }
    }
}
