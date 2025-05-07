using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookSansar.DTO;
using bookSansar.Entities;
using bookSansar.Repositories;
using Microsoft.EntityFrameworkCore;

namespace bookSansar.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ApplicationDbContext _context;

        public ReviewService(IReviewRepository reviewRepository, ApplicationDbContext context)
        {
            _reviewRepository = reviewRepository;
            _context = context;
        }

        // Create a new review - checks if user has purchased the book
        public async Task<ReviewResponseDTO> CreateReviewAsync(CreateReviewDTO reviewDto, string userId)
        {
            // Verify that the purchase exists and belongs to the user
            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(p => p.Id == reviewDto.PurchaseId && 
                                        p.UserId == userId && 
                                        p.BookId == reviewDto.BookId);

            if (purchase == null)
            {
                throw new InvalidOperationException("Cannot review a book that hasn't been purchased.");
            }

            // Check if a review already exists for this purchase
            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.PurchaseId == reviewDto.PurchaseId);

            if (existingReview != null)
            {
                throw new InvalidOperationException("A review already exists for this purchase.");
            }

            var review = new Review
            {
                UserId = userId,
                BookId = reviewDto.BookId,
                PurchaseId = reviewDto.PurchaseId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            var createdReview = await _reviewRepository.CreateAsync(review);
            return MapToDTO(createdReview);
        }

        // Get a single review by its ID
        public async Task<ReviewResponseDTO> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            return review != null ? MapToDTO(review) : null;
        }

        // Get all reviews for a specific book
        public async Task<IEnumerable<ReviewResponseDTO>> GetReviewsByBookIdAsync(int bookId)
        {
            var reviews = await _reviewRepository.GetByBookIdAsync(bookId);
            return reviews.Select(MapToDTO);
        }

        // Update an existing review - only if user owns it
        public async Task<ReviewResponseDTO> UpdateReviewAsync(int id, CreateReviewDTO reviewDto, string userId)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null || existingReview.UserId != userId)
                return null;

            // Verify that the purchase belongs to the user
            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(p => p.Id == reviewDto.PurchaseId && 
                                        p.UserId == userId && 
                                        p.BookId == reviewDto.BookId);

            if (purchase == null)
            {
                throw new InvalidOperationException("Cannot review a book that hasn't been purchased.");
            }

            existingReview.Rating = reviewDto.Rating;
            existingReview.Comment = reviewDto.Comment;
            existingReview.UpdatedAt = DateTime.UtcNow;

            var updatedReview = await _reviewRepository.UpdateAsync(existingReview);
            return MapToDTO(updatedReview);
        }

        // Delete a review - only if user owns it
        public async Task DeleteReviewAsync(int id, string userId)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review != null && review.UserId == userId)
            {
                await _reviewRepository.DeleteAsync(id);
            }
        }

        // Calculate average rating for a book
        public async Task<double> GetAverageRatingForBookAsync(int bookId)
        {
            return await _reviewRepository.GetAverageRatingForBookAsync(bookId);
        }

        // Convert Review entity to DTO for API response
        private static ReviewResponseDTO MapToDTO(Review review)
        {
            return new ReviewResponseDTO
            {
                Id = review.Id,
                UserId = review.UserId,
                BookId = review.BookId,
                PurchaseId = review.PurchaseId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            };
        }
    }
} 