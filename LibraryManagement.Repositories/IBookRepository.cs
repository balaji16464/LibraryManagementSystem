using System.Collections.Generic;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetByIsbn(string isbn);
        void Add(Book book);
        void Update(Book book);
        void Delete(string isbn);
    }
}
