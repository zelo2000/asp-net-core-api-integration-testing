namespace OZ.OrderApi.Services.Orders.Models
{
    public record OrderItem
    {
        public Guid OrderItemId { get; set; }
        public int OrderItemAmount { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }

        public double TotalPrice { get => ProductPrice * OrderItemAmount; }
    }
}
