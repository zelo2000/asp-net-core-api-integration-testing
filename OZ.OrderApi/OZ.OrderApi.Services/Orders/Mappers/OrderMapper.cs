using OZ.OrderApi.Services.Orders.Entities;
using OZ.OrderApi.Services.Orders.Models;
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
