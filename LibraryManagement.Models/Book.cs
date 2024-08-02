using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title is required and cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author is required and cannot exceed 100 characters.")]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Year must be a positive number.")]
        public int Year { get; set; }
    }
}
