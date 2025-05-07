using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using Application.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/wishlist/1 (Fetch wishlist items for a specific user)
        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(int userId)
        {
            var wishlist = await _context.Wishlists
                .Include(w => w.Book)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            if (!wishlist.Any())
                return NotFound(new { message = "Wishlist is empty or not found for the user." });

            var wishlistDto = wishlist.Select(w => new
            {
                WishlistId = w.WishlistId,
                BookId = w.BookId,
                BookTitle = w.Book?.Title ?? "Unknown",
                BookAuthor = w.Book?.Author ?? "Unknown",
                BookPrice = w.Book?.Price ?? 0,
                AddedDate = w.DateAdded
            }).ToList();

            return Ok(wishlistDto);
        }

        // POST: api/wishlist/add (Add a book to a user's wishlist)
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromQuery] int userId, [FromQuery] int bookId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found." });

            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                return NotFound(new { message = "Book not found." });

            var existingItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == bookId);

            if (existingItem != null)
                return BadRequest(new { message = "Book is already in your wishlist." });

            var wishlistItem = new Wishlist
            {
                UserId = userId,
                BookId = bookId,
                DateAdded = DateTime.UtcNow
            };

            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Book added to wishlist successfully.", wishlistItemId = wishlistItem.WishlistId });
        }

        // POST: api/wishlist/remove/1 (Remove a book from a user's wishlist)
        [HttpPost("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.Wishlists.FindAsync(id);
            if (item == null)
                return NotFound(new { message = "Wishlist item not found." });

            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Book removed from wishlist successfully." });
        }
    }
}
