namespace Application.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
