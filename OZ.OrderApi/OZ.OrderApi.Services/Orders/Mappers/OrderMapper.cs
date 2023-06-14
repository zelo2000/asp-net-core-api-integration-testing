using OZ.OrderApi.Contracts.Orders;
using OZ.OrderApi.Services.Orders.Entities;
namespace OZ.OrderApi.Services.Orders.Mappers
{
    public static class OrderMapper
    {
        public static Order ToDomain(this OrderEntity entity)
        {
            return new Order
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Items = entity.Items.Select(i => new OrderItem
                {
                    OrderItemId = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    ProductPrice = i.Product.Price,
                    OrderItemAmount = i.Amount,
                })
            };
        }
    }
}
