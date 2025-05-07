using System;
using System.ComponentModel.DataAnnotations;

namespace bookSansar.DTO
{
    public class CreateReviewDTO
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
         
        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Comment must be between 10 and 1000 characters")]
        public string Comment { get; set; }
    }

    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int PurchaseId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 