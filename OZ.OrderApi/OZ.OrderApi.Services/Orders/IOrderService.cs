using OZ.OrderApi.Contracts.Orders;

namespace OZ.OrderApi.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(Guid orderId);

        Task<IEnumerable<Order>> GetOrderByUserId(Guid userId);
    }
}