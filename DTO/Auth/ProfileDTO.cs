// DTO/Auth/ProfileDTO.cs
using System.ComponentModel.DataAnnotations;

namespace bookSansar.DTO.Auth
{
    public class ProfileDTO
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}