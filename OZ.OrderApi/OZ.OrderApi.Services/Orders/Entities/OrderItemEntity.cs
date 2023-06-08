namespace OZ.OrderApi.Services.Orders.Entities
{
    public class OrderItemEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public ProductEntity Product { get; set; }

        public int Amount { get; set; }
    }
}
