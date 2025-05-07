using System.Collections.Generic;
using System.Threading.Tasks;
using bookSansar.DTO;
using bookSansar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookSansar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Create a new review (needs login)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewResponseDTO>> CreateReview(CreateReviewDTO reviewDto)
        {
            var userId = User.FindFirst("sub")?.Value;
            var review = await _reviewService.CreateReviewAsync(reviewDto, userId);
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        // Get a single review by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReview(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return review;
        }

        // Get all reviews for a book
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<ReviewResponseDTO>>> GetBookReviews(int bookId)
        {
            var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId);
            return Ok(reviews);
        }

        // Get average rating for a book
        [HttpGet("book/{bookId}/average")]
        public async Task<ActionResult<double>> GetBookAverageRating(int bookId)
        {
            var averageRating = await _reviewService.GetAverageRatingForBookAsync(bookId);
            return Ok(averageRating);
        }

        // Update a review (needs login)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ReviewResponseDTO>> UpdateReview(int id, CreateReviewDTO reviewDto)
        {
            var userId = User.FindFirst("sub")?.Value;
            var review = await _reviewService.UpdateReviewAsync(id, reviewDto, userId);
            
            if (review == null)
                return NotFound();

            return review;
        }

        // Delete a review (needs login)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = User.FindFirst("sub")?.Value;
            await _reviewService.DeleteReviewAsync(id, userId);
            return NoContent();
        }
    }
} 