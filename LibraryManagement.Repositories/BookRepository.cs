using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    /// <summary>
    /// In-memory implementation of the book repository.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// </remarks>
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        /// <summary>
        /// Gets all books from the repository.
        /// </summary>
        /// <returns>A list of all books.</returns>
        public IEnumerable<Book> GetAll() => _books;

        /// <summary>
        /// Retrieves a book by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <returns>The book with the specified ISBN, or null if not found.</returns>
        /// <exception cref="ArgumentException">Thrown when ISBN is null or empty.</exception>
        public Book GetByIsbn(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be null or empty.", nameof(isbn));
            }
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }

        /// <summary>
        /// Retrieves a book by its Id.
        /// </summary>
        /// <param name="isbn">The Id of the book.</param>
        /// <returns>The book with the specified Id, or null if not found.</returns>
        /// <exception cref="ArgumentException">Thrown when Id is null or empty.</exception>
        public Book GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the book is null.</exception>
        /// <exception cref="ArgumentException">Thrown when a book with the same ISBN already exists.</exception>
        public Book Add(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (GetByIsbn(book.ISBN) != null)
            {
                throw new ArgumentException("A book with the same ISBN already exists.");
            }

            book.Id = _books.Count + 1; // Auto-increment ID
            _books.Add(book);

            return book;
        }

        /// <summary>
        /// Updates an existing book in the repository.
        /// </summary>
        /// <param name="book">The book with updated information.</param>
        /// <exception cref="ArgumentNullException">Thrown when the book is null.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the book is not found in the repository.</exception>
        public void Update(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            var existingBook = GetByIsbn(book.ISBN);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
        }

        /// <summary>
        /// Deletes a book from the repository by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to delete.</param>
        /// <exception cref="ArgumentException">Thrown when ISBN is null or empty.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the book is not found in the repository.</exception>
        public bool DeleteByIsbn(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be null or empty.", nameof(isbn));
            }

            var book = GetByIsbn(isbn);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            return _books.Remove(book);
        }

        /// <summary>
        /// Deletes a book from the repository by its Id.
        /// </summary>
        /// <param name="id">The id of the book to delete.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the book is not found in the repository.</exception>
        public bool DeleteById(int id)
        {
            var book = GetById(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            return _books.Remove(book);
        }
    }
}