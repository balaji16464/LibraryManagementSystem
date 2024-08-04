using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.Services;
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
        public void AddBook_ShouldThrowException_WhenIsbnIsInvalid()
        {
            var invalidBook = new Book { Title = "Test Book", Author = "Test Author", ISBN = "InvalidISBN", Year = 2020 };

            var ex = Assert.Throws<ArgumentException>(() => _bookService.AddBook(invalidBook));
            Assert.That(ex.Message, Is.EqualTo("Invalid ISBN format."));
            _mockRepository.Verify(r => r.Add(It.IsAny<Book>()), Times.Never, "Add method should not be called for an invalid book.");
        }

        [Test]
        public void AddBook_ShouldAddBook_WhenValid()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };

            _bookService.AddBook(book);

            _mockRepository.Verify(r => r.Add(book), Times.Once, "Add method should be called once.");
        }

        [Test]
        public void UpdateBook_ShouldThrowException_WhenIsbnIsInvalid()
        {
            var book = new Book { Title = "Original Title", Author = "Original Author", ISBN = "InvalidISBN", Year = 2020 };

            var ex = Assert.Throws<ArgumentException>(() => _bookService.UpdateBook(book));
            Assert.That(ex.Message, Is.EqualTo("Invalid ISBN format."));
            _mockRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Never, "Update method should not be called for an invalid ISBN.");
        }

        [Test]
        public void UpdateBook_ShouldUpdateBook_WhenExists()
        {
            var existingBook = new Book { Title = "Original Title", Author = "Original Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _mockRepository.Setup(r => r.GetByIsbn(existingBook.ISBN)).Returns(existingBook);

            var updatedBook = new Book { Title = "Updated Title", Author = "Updated Author", ISBN = existingBook.ISBN, Year = 2021 };

            _bookService.UpdateBook(updatedBook);

            _mockRepository.Verify(r => r.Update(It.Is<Book>(b => b.ISBN == updatedBook.ISBN && b.Title == "Updated Title")), Times.Once, "Update method should be called once with updated book.");
        }

        [Test]
        public void DeleteBookByIsbn_ShouldThrowException_WhenIsbnIsInvalid()
        {
            var invalidIsbn = "InvalidISBN";

            var ex = Assert.Throws<ArgumentException>(() => _bookService.DeleteBookByIsbn(invalidIsbn));
            Assert.That(ex.Message, Is.EqualTo("Invalid ISBN format."));
            _mockRepository.Verify(r => r.DeleteByIsbn(It.IsAny<string>()), Times.Never, "Delete method should not be called for an invalid ISBN.");
        }

        [Test]
        public void DeleteBookByIsbn_ShouldCallDelete_WhenExists()
        {
            var isbn = "978-3-16-148410-0";

            _mockRepository.Setup(r => r.DeleteByIsbn(isbn)).Returns(true);

            var result = _bookService.DeleteBookByIsbn(isbn);

            Assert.That(result, Is.True, "DeleteBookByIsbn should return true if deletion was successful.");
            _mockRepository.Verify(r => r.DeleteByIsbn(isbn), Times.Once, "Delete method should be called once.");
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

        [Test]
        public void GetBookByIsbn_ShouldReturnNull_WhenBookDoesNotExist()
        {
            var isbn = "978-3-16-148410-0";
            _mockRepository.Setup(r => r.GetByIsbn(isbn)).Returns((Book)null);

            var result = _bookService.GetBookByIsbn(isbn);

            Assert.That(result, Is.Null, "GetBookByIsbn should return null if no book is found with the specified ISBN.");
        }

        [Test]
        public void GetBookById_ShouldReturnBook_WhenExists()
        {
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _mockRepository.Setup(r => r.GetById(book.Id)).Returns(book);

            var result = _bookService.GetBookById(book.Id);

            Assert.That(result, Is.EqualTo(book), "GetBookById should return the book with the specified ID.");
        }

        [Test]
        public void GetBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            var id = 1;
            _mockRepository.Setup(r => r.GetById(id)).Returns((Book)null);

            var result = _bookService.GetBookById(id);

            Assert.That(result, Is.Null, "GetBookById should return null if no book is found with the specified ID.");
        }
    }
}
