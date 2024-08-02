using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Moq;

namespace LibraryManagement.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="BookService"/> class.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// Purpose:
    /// This file contains unit tests for the <see cref="BookService"/> class, which provides business logic
    /// for managing books. The tests validate the methods for adding, updating, deleting, and retrieving books
    /// while ensuring proper interactions with the mocked <see cref="IBookRepository"/>.
    /// </remarks>
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IBookRepository> _mockRepository;
        private BookService _bookService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockRepository.Object);
        }

        [Test]
        public void AddBook_ShouldAddBook_WhenValid()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };

            _bookService.AddBook(book);

            _mockRepository.Verify(r => r.Add(book), Times.Once, "Add method should be called once.");
        }

        [Test]
        public void UpdateBook_ShouldUpdateBook_WhenExists()
        {
            var book = new Book { Title = "Original Title", Author = "Original Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _mockRepository.Setup(r => r.GetByIsbn(book.ISBN)).Returns(book);

            _bookService.UpdateBook(new Book { Title = "Updated Title", Author = "Updated Author", ISBN = book.ISBN, Year = 2021 });

            _mockRepository.Verify(r => r.Update(It.Is<Book>(b => b.ISBN == book.ISBN && b.Title == "Updated Title")), Times.Once, "Update method should be called once with updated book.");
        }

        [Test]
        public void DeleteBook_ShouldCallDelete_WhenExists()
        {
            var isbn = "978-3-16-148410-0";

            _bookService.DeleteBook(isbn);

            _mockRepository.Verify(r => r.Delete(isbn), Times.Once, "Delete method should be called once.");
        }

        [Test]
        public void GetAllBooks_ShouldReturnBooks()
        {
            var books = new List<Book>
            {
                new Book { Title = "Book 1", Author = "Author 1", ISBN = "978-3-16-148410-1", Year = 2020 },
                new Book { Title = "Book 2", Author = "Author 2", ISBN = "978-3-16-148410-2", Year = 2021 }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(books);

            var result = _bookService.GetAllBooks();

            Assert.That(result, Is.EqualTo(books), "GetAllBooks should return the list of books.");
        }

        [Test]
        public void GetBookByIsbn_ShouldReturnBook_WhenExists()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _mockRepository.Setup(r => r.GetByIsbn(book.ISBN)).Returns(book);

            var result = _bookService.GetBookByIsbn(book.ISBN);

            Assert.That(result, Is.EqualTo(book), "GetBookByIsbn should return the book with the specified ISBN.");
        }
    }
}
