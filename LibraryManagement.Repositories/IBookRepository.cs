using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    /// <summary>
    /// Defines the contract for book repository operations.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// </remarks>
    public interface IBookRepository
    {
        /// <summary>
        /// Retrieves all books from the repository.
        /// </summary>
        /// <returns>A collection of all books.</returns>
        IEnumerable<Book> GetAll();

        /// <summary>
        /// Retrieves a book by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <returns>The book with the specified ISBN, or null if not found.</returns>
        Book GetByIsbn(string isbn);

        /// <summary>
        /// Retrieves a book by its Id.
        /// </summary>
        /// <param name="id">The Id of the book.</param>
        /// <returns>The book with the specified Id, or null if not found.</returns>
        Book GetById(int id);

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        /// <param name="book">The book to add.</param>
        Book Add(Book book);

        /// <summary>
        /// Updates an existing book in the repository.
        /// </summary>
        /// <param name="book">The book with updated information.</param>
        void Update(Book book);

        /// <summary>
        /// Deletes a book from the repository by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to delete.</param>
        bool DeleteByIsbn(string isbn);

        /// <summary>
        /// Deletes a book from the repository by its Id.
        /// </summary>
        /// <param name="id">The id of the book to delete.</param>
        bool DeleteById(int id);
    }
}
