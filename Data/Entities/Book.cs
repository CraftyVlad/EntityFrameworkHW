using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(100)]
        public string Publisher { get; set; }

        public int PageCount { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public int PublicationYear { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePercentage { get; set; }
        public decimal SalePrice { get; set; }

        public int BooksInStock { get; set; }
        public int BooksSold { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsSequel { get; set; }
        public string? SequelTo { get; set; }
        public bool IsReserved { get; set; }

    }
}
