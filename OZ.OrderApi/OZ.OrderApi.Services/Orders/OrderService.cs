using OZ.OrderApi.Contracts.Orders;
using OZ.OrderApi.Services.Exceptions;
using OZ.OrderApi.Services.Orders.Entities;
using OZ.OrderApi.Services.Orders.Mappers;
using OZ.OrderApi.Services.Users;

namespace OZ.OrderApi.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly List<OrderEntity> _orders;
        private readonly IUserApiClient _userApiClient;

        public OrderService(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
            _orders = BuildOrders();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            var entity = _orders.FirstOrDefault(o => o.Id == orderId);

            if (entity == null)
                return null;

            var order = entity.ToDomain();

            var user = await _userApiClient.GetById(entity.UserId);
            if (user != null)
            {
                order.UserFullName = user.FullName;
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrderByUserId(Guid userId)
        {
            var user = await _userApiClient.GetById(userId)
                ?? throw new EntityNotFoundException($"User with id {userId} not found");

            var entities = _orders.Where(o => o.UserId == userId);
            var orders = entities.Select(e =>
            {
                var order = e.ToDomain();
                order.UserFullName = user.FullName;
                return order;
            });

            return orders;
        }

        private List<OrderEntity> BuildOrders()
        {
            var productId1 = Guid.NewGuid();
            var productId21 = Guid.NewGuid();
            var productId22 = Guid.NewGuid();
            var userId = Guid.Parse("52d7c2a5-3b7f-4f6e-a7ff-08db684d3460");

            return new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = Guid.Parse("c7925cce-d16c-44c7-b930-8ebd30c16a3b"),
                    UserId = userId,
                    Items = new List<OrderItemEntity>
                    {
                        new OrderItemEntity
                        {
                            Id = Guid.NewGuid(),
                            Amount = 1,
                            ProductId = productId1,
                            Product = new ProductEntity
                            {
                                Id = productId1,
                                Name = "Phone",
                                Price = 200
                            }
                        }
                    }
                },
                new OrderEntity
                {
                    Id = Guid.Parse("9b5ce05e-caae-40c2-a94e-16a125840590"),
                    UserId = userId,
                    Items = new List<OrderItemEntity>
                    {
                        new OrderItemEntity
                        {
                            Id = Guid.NewGuid(),
                            Amount = 2,
                            ProductId = productId21,
                            Product = new ProductEntity
                            {
                                Id = productId21,
                                Name = "Keyboard",
                                Price = 150
                            }
                        },
                        new OrderItemEntity
                        {
                            Id = Guid.NewGuid(),
                            Amount = 3,
                            ProductId = productId22,
                            Product = new ProductEntity
                            {
                                Id = productId22,
                                Name = "USB connector",
                                Price = 40
                            }
                        }
                    }
                }
            };
        }
    }
}