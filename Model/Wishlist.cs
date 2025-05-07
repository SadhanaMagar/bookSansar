namespace Application.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }  

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public DateTime DateAdded { get; set; } 
    }
}
