using Shouldly;
using System.ComponentModel;

namespace OZ.OrderApi.ClientTests
{
    public class OrderApiClientTest : BaseIntegrationTests
    {
        public OrderApiClientTest() : base() { }

        [Fact]
        public async Task GetOrderByIdAsync_Order()
        {
            // Arrange
            var orderId = Guid.Parse("c7925cce-d16c-44c7-b930-8ebd30c16a3b");

            // Act
            var order = await Client.Order_GetOrderByIdAsync(orderId, "1.0");

            // Assert
            order.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetOrderByUserIdAsync_OrderList()
        {
            // Arrange
            var userId = Guid.Parse("52d7c2a5-3b7f-4f6e-a7ff-08db684d3460");

            // Act 
            var order = await Client.Order_GetOrderByUserIdAsync(userId, "1.0");

            // Assert
            order.Count.ShouldBe(2);
        }
    }
}