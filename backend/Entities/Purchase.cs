using System;
using System.ComponentModel.DataAnnotations;

namespace bookSansar.Entities
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal Amount { get; set; }

        public string? TransactionId { get; set; }
    }
} 