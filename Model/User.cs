namespace Application.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string Email { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
