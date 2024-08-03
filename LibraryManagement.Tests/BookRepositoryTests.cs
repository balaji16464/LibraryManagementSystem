using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="BookRepository"/> class.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// Purpose:
    /// This file contains unit tests for the <see cref="BookRepository"/> class, which provides methods
    /// for managing books in the repository. The tests cover adding, updating, deleting, and retrieving
    /// books to ensure the correct functionality of repository methods.
    /// </remarks>
    [TestFixture]
    public class BookRepositoryTests
    {
        private BookRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new BookRepository();
        }

        [Test]
        public void Add_ShouldAddBookSuccessfully()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };

            _repository.Add(book);

            var result = _repository.GetByIsbn(book.ISBN);
            Assert.That(result, Is.Not.Null, "Book should be added.");
            Assert.That(result.Title, Is.EqualTo(book.Title), "Book title should match.");
        }

        [Test]
        public void Update_ShouldUpdateBookSuccessfully()
        {
            var book = new Book { Title = "Original Title", Author = "Original Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _repository.Add(book);

            var updatedBook = new Book { Title = "Updated Title", Author = "Updated Author", ISBN = book.ISBN, Year = 2021 };
            _repository.Update(updatedBook);

            var result = _repository.GetByIsbn(book.ISBN);
            Assert.That(result, Is.Not.Null, "Book should exist.");
            Assert.That(result.Title, Is.EqualTo("Updated Title"), "Book title should be updated.");
        }

        [Test]
        public void Delete_ShouldRemoveBookSuccessfully()
        {
            var book = new Book { Title = "Book to Delete", Author = "Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _repository.Add(book);

            _repository.DeleteByIsbn(book.ISBN);

            var result = _repository.GetByIsbn(book.ISBN);
            Assert.That(result, Is.Null, "Book should be removed.");
        }

        [Test]
        public void DeleteById_ShouldRemoveBookSuccessfully()
        {
            var book = new Book { Title = "Book to Delete", Author = "Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _repository.Add(book);

            _repository.DeleteById(book.Id);

            var result = _repository.GetById(book.Id);
            Assert.That(result, Is.Null, "Book should be removed.");
        }

        [Test]
        public void GetAll_ShouldReturnAllBooks()
        {
            _repository.Add(new Book { Title = "Book 1", Author = "Author 1", ISBN = "978-3-16-148410-1", Year = 2020 });
            _repository.Add(new Book { Title = "Book 2", Author = "Author 2", ISBN = "978-3-16-148410-2", Year = 2021 });

            var books = _repository.GetAll().ToList();

            Assert.That(books.Count, Is.EqualTo(2), "Should return two books.");
            Assert.That(books, Has.All.Property("Title").Not.Empty, "All books should have non-empty titles.");
        }
    }
}
