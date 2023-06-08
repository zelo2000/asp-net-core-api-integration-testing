namespace OZ.OrderApi.Services.Orders.Models
{
    public record Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? UserFullName { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }
    }
}
