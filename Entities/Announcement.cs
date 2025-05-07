using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookSansar.Entities
{
    [Table("Announcements")]
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5-200 characters")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Content must be between 10-1000 characters")]
        [Column(TypeName = "text")]
        public string Content { get; set; } = null!;

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}