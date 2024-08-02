using LibraryManagement.Models;
using LibraryManagement.Repositories;

public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Book> GetAllBooks() => _repository.GetAll();

    public Book GetBookByIsbn(string isbn) => _repository.GetByIsbn(isbn);

    public void AddBook(Book book)
    {
        _repository.Add(book);
    }

    public void UpdateBook(Book book)
    {
        _repository.Update(book);
    }

    public void DeleteBook(string isbn)
    {
        _repository.Delete(isbn);
    }
}
