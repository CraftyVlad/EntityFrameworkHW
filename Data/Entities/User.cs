using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
