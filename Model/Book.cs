namespace Application.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
