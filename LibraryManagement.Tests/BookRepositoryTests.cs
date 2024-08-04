using LibraryManagement.Models;
using LibraryManagement.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LibraryManagement.Tests
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private BookRepository _bookRepository;

        [SetUp]
        public void SetUp()
        {
            _bookRepository = new BookRepository();
        }

        [Test]
        public void Add_ShouldThrowException_WhenBookIsNull()
        {
            Book nullBook = null;

            var ex = Assert.Throws<ArgumentNullException>(() => _bookRepository.Add(nullBook));
            Assert.That(ex.ParamName, Is.EqualTo("book"));
        }

        [Test]
        public void Add_ShouldThrowException_WhenIsbnAlreadyExists()
        {
            var book1 = new Book { Title = "Book 1", Author = "Author 1", ISBN = "978-3-16-148410-0", Year = 2020 };
            _bookRepository.Add(book1);

            var book2 = new Book { Title = "Book 2", Author = "Author 2", ISBN = "978-3-16-148410-0", Year = 2021 };

            var ex = Assert.Throws<ArgumentException>(() => _bookRepository.Add(book2));
            Assert.That(ex.Message, Is.EqualTo("A book with the same ISBN already exists."));
        }

        [Test]
        public void Add_ShouldAssignAutoIncrementedId_WhenBookIsAdded()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };

            var addedBook = _bookRepository.Add(book);

            Assert.That(addedBook.Id, Is.EqualTo(1), "First book added should have Id = 1.");
        }

        [Test]
        public void GetByIsbn_ShouldReturnBook_WhenExists()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _bookRepository.Add(book);

            var result = _bookRepository.GetByIsbn("978-3-16-148410-0");

            Assert.That(result, Is.EqualTo(book));
        }

        [Test]
        public void GetById_ShouldReturnBook_WhenExists()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _bookRepository.Add(book);

            var result = _bookRepository.GetById(1);

            Assert.That(result, Is.EqualTo(book));
        }

        [Test]
        public void Update_ShouldThrowException_WhenBookIsNull()
        {
            Book nullBook = null;

            var ex = Assert.Throws<ArgumentNullException>(() => _bookRepository.Update(nullBook));
            Assert.That(ex.ParamName, Is.EqualTo("book"));
        }

        [Test]
        public void Update_ShouldThrowException_WhenBookDoesNotExist()
        {
            var book = new Book { Id = 999, Title = "Non-existent Book", Author = "Unknown", ISBN = "999-9-99-999999-9", Year = 2020 };

            var ex = Assert.Throws<KeyNotFoundException>(() => _bookRepository.Update(book));
            Assert.That(ex.Message, Is.EqualTo("Book not found."));
        }

        [Test]
        public void DeleteByIsbn_ShouldThrowException_WhenBookDoesNotExist()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => _bookRepository.DeleteByIsbn("978-3-16-148410-0"));
            Assert.That(ex.Message, Is.EqualTo("Book not found."));
        }

        [Test]
        public void DeleteByIsbn_ShouldReturnTrue_WhenBookIsDeleted()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _bookRepository.Add(book);

            var result = _bookRepository.DeleteByIsbn("978-3-16-148410-0");

            Assert.That(result, Is.True, "DeleteByIsbn should return true when deletion is successful.");
        }

        [Test]
        public void GetAll_ShouldReturnAllBooks()
        {
            var book1 = new Book { Title = "Book 1", Author = "Author 1", ISBN = "978-3-16-148410-0", Year = 2020 };
            var book2 = new Book { Title = "Book 2", Author = "Author 2", ISBN = "978-3-16-148410-1", Year = 2021 };

            _bookRepository.Add(book1);
            _bookRepository.Add(book2);

            var books = _bookRepository.GetAll();

            Assert.That(books, Has.Count.EqualTo(2));
            Assert.That(books, Contains.Item(book1));
            Assert.That(books, Contains.Item(book2));
        }

        [Test]
        public void DeleteById_ShouldThrowException_WhenBookDoesNotExist()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => _bookRepository.DeleteById(999));
            Assert.That(ex.Message, Is.EqualTo("Book not found."));
        }

        [Test]
        public void DeleteById_ShouldReturnTrue_WhenBookIsDeleted()
        {
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "978-3-16-148410-0", Year = 2020 };
            _bookRepository.Add(book);

            var result = _bookRepository.DeleteById(1);

            Assert.That(result, Is.True, "DeleteById should return true when deletion is successful.");
        }
    }
}
