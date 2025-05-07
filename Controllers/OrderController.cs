using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using Application.Models;
using Application.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .ToListAsync();

            var orderDtos = orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,  
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.BookId,
                    ProductName = oi.Book.Title,
                    Quantity = oi.Quantity,
                    Price = oi.Book.Price
                }).ToList()
            }).ToList();

            return Ok(orderDtos);
        }


        
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromQuery] int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Book)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest("Cart is empty.");

            decimal totalAmount = cartItems.Sum(ci => ci.Book.Price * ci.Quantity);

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Pending", 
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    BookId = ci.BookId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully.", orderId = order.OrderId });
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            
            if (order.Status == "Cancelled")
                return BadRequest(new { message = "Order is already cancelled." });

        
            order.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order marked as cancelled successfully.", orderId = order.OrderId, status = order.Status });
        }
    }
}
