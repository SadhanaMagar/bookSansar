using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookSansar.Entities
{
    // Represents a book review in the system
    public class Review
    {
        [Key]
        public int Id { get; set; }

        // ID of the user who wrote the review
        [Required]
        public string UserId { get; set; }

        // ID of the book being reviewed
        [Required]
        public int BookId { get; set; }

        // ID of the purchase that this review is for
        [Required]
        public int PurchaseId { get; set; }

        // Rating from 1 to 5 stars
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        // Review text (10-1000 characters)
        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Comment must be between 10 and 1000 characters")]
        public string Comment { get; set; }

        // When the review was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // When the review was last updated (if any)
        public DateTime? UpdatedAt { get; set; }

        // Link to the purchase this review is for
        [ForeignKey("PurchaseId")]
        public Purchase Purchase { get; set; }
    }
} 