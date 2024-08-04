using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.App
{
    /// <summary>
    /// Provides the menu and user interface for interacting with the Library Management System.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// Purpose:
    /// This file defines the Menu class, which is responsible for displaying the main menu,
    /// handling user inputs, and invoking appropriate operations related to book management.
    /// </remarks>
    public class Menu
    {
        private readonly BookService _bookService;

        public Menu(BookService bookService)
        {
            _bookService = bookService;
        }

        #region Public Methods

        public void DisplayMainMenu()
        {
            DisplayInstructions();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("       Library Management System     ");
                Console.WriteLine("=====================================\n");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Update an existing book by ISBN or Id");
                Console.WriteLine("3. Delete a book by ISBN or Id");
                Console.WriteLine("4. List all books");
                Console.WriteLine("5. View details of a specific book by ISBN or Id");
                Console.WriteLine("6. Exit\n");
                Console.Write("Enter your choice: ");
                var option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        ManageAddBook();
                        break;
                    case "2":
                        ManageUpdateBook();
                        break;
                    case "3":
                        ManageDeleteBook();
                        break;
                    case "4":
                        ListAllBooks();
                        break;
                    case "5":
                        ManageViewBookDetails();
                        break;
                    case "6":
                        Console.WriteLine("Exiting the application. Thank You!");
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Manages the process of adding a new book to the system.
        /// </summary>
        private void ManageAddBook()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add a New Book");
                Console.WriteLine("==============\n");

                try
                {
                    var book = new Book
                    {
                        Title = Helper.GetValidInput("Enter Title: "),
                        Author = Helper.GetValidInput("Enter Author: "),
                        ISBN = Helper.GetValidIsbn(),
                        Year = Helper.GetValidYear()
                    };

                    var addedBook = _bookService.AddBook(book);
                    Console.WriteLine($"\nBook added successfully with the ID: {addedBook.Id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (!PromptForAnotherOperation()) break;
            }
        }

        /// <summary>
        /// Manages the process of updating an existing book in the system.
        /// </summary>
        private void ManageUpdateBook()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update an Existing Book");
                Console.WriteLine("=======================\n");

                var book = GetBookByUserChoice();
                if (book == null)
                {
                    Console.WriteLine("\nBook not found.");
                    if (!PromptForAnotherOperation()) break;
                    continue;
                }

                try
                {
                    UpdateBookDetails(book);
                    _bookService.UpdateBook(book);
                    Console.WriteLine("\nBook updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (!PromptForAnotherOperation()) break;
            }
        }

        /// <summary>
        /// Manages the process of deleting a book from the system.
        /// </summary>
        private void ManageDeleteBook()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Delete a Book");
                Console.WriteLine("=============\n");

                var book = GetBookByUserChoice();
                if (book == null)
                {
                    Console.WriteLine("\nBook not found.");
                }
                else
                {
                    try
                    {
                        var success = _bookService.DeleteBookById(book.Id) || _bookService.DeleteBookByIsbn(book.ISBN);
                        Console.WriteLine(success ? "\nBook deleted successfully." : "\nBook could not be deleted.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }
                }

                if (!PromptForAnotherOperation()) break;
            }
        }

        /// <summary>
        /// Manages the process of viewing details of a specific book.
        /// </summary>
        private void ManageViewBookDetails()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("View Book Details");
                Console.WriteLine("=================\n");

                var book = GetBookByUserChoice();
                if (book != null)
                {
                    Console.WriteLine($"\nTitle: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    Console.WriteLine($"ISBN: {book.ISBN}");
                    Console.WriteLine($"Year: {book.Year}");
                }
                else
                {
                    Console.WriteLine("\nBook not found.");
                }

                if (!PromptForAnotherOperation()) break;
            }
        }

        /// <summary>
        /// Lists all books available in the system.
        /// </summary>
        private void ListAllBooks()
        {
            Console.Clear();
            Console.WriteLine("List of All Books");
            Console.WriteLine("=================\n");

            var books = _bookService.GetAllBooks();
            if (books == null || !books.Any())
            {
                Console.WriteLine("No books available.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Year: {book.Year}");
                }
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        /// <summary>
        /// Prompts the user to choose a book by ID or ISBN and retrieves the book details.
        /// </summary>
        /// <returns>The selected book or null if not found.</returns>
        /// <summary>
        /// Lists all books available in the system.
        /// </summary>
        private Book GetBookByUserChoice()
        {
            Console.WriteLine("Would you like to search for the book by ID or ISBN?");
            Console.WriteLine("1. By ID");
            Console.WriteLine("2. By ISBN");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            Book book = null;

            if (choice == "1")
            {
                var id = Helper.GetValidInput("Enter Book ID: ");
                if (int.TryParse(id, out var bookId))
                {
                    book = _bookService.GetBookById(bookId);
                }
            }
            else if (choice == "2")
            {
                var isbn = Helper.GetValidIsbn();
                book = _bookService.GetBookByIsbn(isbn);
            }

            return book;
        }

        /// <summary>
        /// Prompts the user to choose a book by ID or ISBN and retrieves the book details.
        /// </summary>
        /// <returns>The selected book or null if not found.</returns>
        private void UpdateBookDetails(Book book)
        {
            Console.WriteLine($"\nCurrent ISBN: {book.ISBN}");
            var newIsbn = Helper.GetValidIsbn();
            if (!string.IsNullOrWhiteSpace(newIsbn))
            {
                book.ISBN = newIsbn;
            }

            book.Title = Helper.GetValidInput("Enter new Title: ");
            book.Author = Helper.GetValidInput("Enter new Author: ");
            book.Year = Helper.GetValidYear();
        }

        /// <summary>
        /// Prompts the user to perform another operation within the same section.
        /// </summary>
        /// <returns>True if the user wants to perform another operation; otherwise, false.</returns>
        private static bool PromptForAnotherOperation()
        {
            Console.WriteLine("\nDo you want to perform another operation in this section? (Y/N)");
            var choice = Console.ReadLine()?.ToUpper();
            return choice == "Y";
        }

        /// <summary>
        /// Displays simple instructions for using the Library Management System.
        /// </summary>
        private static void DisplayInstructions()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine(" Welcome to the Library Management System ");
            Console.WriteLine("=====================================\n");
            Console.WriteLine("Instructions:");
            Console.WriteLine("1. You can add, update, delete, list, and view books.");
            Console.WriteLine("2. Follow the prompts for each option to enter the required details.");
            Console.WriteLine("3. Make sure to enter valid data, especially for ISBN and Year.");
            Console.WriteLine("4. To exit, select the appropriate option from the main menu.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        #endregion
    }
}
