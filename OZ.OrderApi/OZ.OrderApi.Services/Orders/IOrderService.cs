using OZ.OrderApi.Services.Orders.Models;

namespace OZ.OrderApi.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(Guid orderId);

        Task<IEnumerable<Order>> GetOrderByUserId(Guid userId);
    }
}