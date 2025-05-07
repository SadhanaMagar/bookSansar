// AnnouncementCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace bookSansar.DTO
{
    public class AnnouncementCreateDto
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime EndDate { get; set; }
    }
}

