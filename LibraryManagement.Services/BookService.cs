using LibraryManagement.Models;
using LibraryManagement.Repositories;
using System.Text.RegularExpressions;

namespace LibraryManagement.Services
{
    /// <summary>
    /// Provides operations for managing books in the library.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// Purpose:
    /// This file defines the BookService class, which serves as a service layer for interacting
    /// with the book repository. It includes methods for retrieving, adding, updating, and
    /// deleting books. The service layer abstracts the data access layer and provides business
    /// logic operations for managing books.
    /// </remarks>

    public partial class BookService
    {
        private readonly IBookRepository _repository;
        private static readonly Regex IsbnRegex = Isbn();

        /// <summary>
        /// Initializes a new instance of BookService with the provided repository.
        /// </summary>
        /// <param name="repository">The book repository to use.</param>
        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all books from the repository.
        /// </summary>
        /// <returns>All books in the repository.</returns>
        public IEnumerable<Book> GetAllBooks() => _repository.GetAll();

        /// <summary>
        /// Gets a book by ISBN from the repository.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <returns>The book with the specified ISBN, or null if not found.</returns>
        public Book GetBookByIsbn(string isbn) => _repository.GetByIsbn(isbn);

        /// <summary>
        /// Gets a book by Id from the repository.
        /// </summary>
        /// <param name="isbn">The Id of the book.</param>
        /// <returns>The book with the specified Id, or null if not found.</returns>
        public Book GetBookById(int id) => _repository.GetById(id);

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        /// <param name="book">The book to add.</param>
        public Book AddBook(Book book)
        {
            if (!ValidateIsbn(book.ISBN))
            {
                throw new ArgumentException("Invalid ISBN format.");
            }

            return _repository.Add(book);
        }

        /// <summary>
        /// Updates an existing book in the repository.
        /// </summary>
        /// <param name="book">The book with updated information.</param>
        public void UpdateBook(Book book)
        {
            if (!ValidateIsbn(book.ISBN))
            {
                throw new ArgumentException("Invalid ISBN format.");
            }

            _repository.Update(book);
        }

        /// <summary>
        /// Deletes a book from the repository by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to delete.</param>
        public bool DeleteBookByIsbn(string isbn)
        {
            if (!ValidateIsbn(isbn))
            {
                throw new ArgumentException("Invalid ISBN format.");
            }

            return _repository.DeleteByIsbn(isbn);
        }

        /// <summary>
        /// Deletes a book from the repository by its Id.
        /// </summary>
        /// <param name="Id">The Id of the book to delete.</param>
        public bool DeleteBookById(int id)
        {
            return _repository.DeleteById(id);
        }

        private bool ValidateIsbn(string isbn)
        {
            return IsbnRegex.IsMatch(isbn);
        }

        [GeneratedRegex(@"^(97(8|9))?\d{1,5}-\d{1,7}-\d{1,7}-\d{1,7}-\d{1,3}$")]
        private static partial Regex Isbn();
    }
}