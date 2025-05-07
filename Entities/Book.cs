using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace bookSansar.Entities
{
    [Table("Books")]
    [Index(nameof(Title), IsUnique = false)]
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        // Foreign Keys
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = null!;

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        // Non-Foreign Fields
        [Required]
        [StringLength(50)]
        public string Format { get; set; } = null!;

        public DateTime PublicationDate { get; set; }

        public bool Availability { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }
    }
}
