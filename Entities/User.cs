// Entities/User.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookSansar.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }

        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; }

        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("membership_id")]
        public string MembershipId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Column("successful_orders_count")]
        public int SuccessfulOrdersCount { get; set; } = 0;

        [Column("is_email_verified")]
        public bool IsEmailVerified { get; set; } = false;

        [Column("verification_token")]
        public string? VerificationToken { get; set; }

        [Column("reset_token")]
        public string? ResetToken { get; set; }

        [Column("reset_token_expires")]
        public DateTime? ResetTokenExpires { get; set; }
    }
}