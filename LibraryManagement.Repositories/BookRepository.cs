using LibraryManagement.Models;
using LibraryManagement.Repositories;
using System.Collections.Generic;
using System.Linq;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public IEnumerable<Book> GetAll() => _books;

    public Book GetByIsbn(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            throw new ArgumentException("ISBN cannot be null or empty.", nameof(isbn));
        }
        return _books.FirstOrDefault(b => b.ISBN == isbn);
    }

    public void Add(Book book)
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
    }

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

    public void Delete(string isbn)
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

        _books.Remove(book);
    }
}
