namespace Application.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        public int Quantity { get; set; }
    }
}
