using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    /// <summary>
    /// Represents a book with its details.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// </remarks>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public required string Author { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Book Number (ISBN) of the book.
        /// </summary>
        /// <remarks>
        /// The ISBN is required.
        /// </remarks>
        public required string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the year the book was published.
        /// </summary>
        /// <remarks>
        /// The year must be a positive number.
        /// </remarks>
        public int Year { get; set; }
    }
}
