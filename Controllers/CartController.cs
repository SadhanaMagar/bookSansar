using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using Application.Models;
using Application.DTOs;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems(int userId)
        {
            var items = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Book)
                .ToListAsync();

            if (!items.Any())
                return NotFound("No items found in the cart.");

            var result = items.Select(ci => new CartItemDto
            {
                UserId = ci.UserId,
                BookId = ci.BookId,
                Quantity = ci.Quantity
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] CartItemDto dto)
        {
            var existing = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == dto.UserId && ci.BookId == dto.BookId);

            if (existing != null)
            {
                existing.Quantity += dto.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = dto.UserId,
                    BookId = dto.BookId,
                    Quantity = dto.Quantity
                };

                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok("Item added to cart.");
        }

     
[HttpDelete("remove")]
public async Task<IActionResult> RemoveFromCart([FromQuery] int userId, [FromQuery] int bookId)
{

    var item = await _context.CartItems
        .FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == bookId);

  
    if (item == null)
        return NotFound("Cart item not found.");

  
    _context.CartItems.Remove(item);
    
   
    await _context.SaveChangesAsync();

  
    return Ok("Item removed from cart.");
}

    }
}
