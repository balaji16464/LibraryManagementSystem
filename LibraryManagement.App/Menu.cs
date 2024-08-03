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

        /// <summary>
        /// Displays the main menu and handles user input for navigating to various operations.
        /// </summary>
        /// <remarks>
        /// This method displays the main menu options to the user and processes their selection. It
        /// invokes the appropriate methods for adding, updating, deleting, listing, and viewing book
        /// details. The menu continues to display until the user chooses to exit the application.
        /// </remarks>
        public void DisplayMainMenu()
        {
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

        /// <summary>
        /// Manages the process of adding a new book to the library.
        /// </summary>
        /// <remarks>
        /// This method clears the console and prompts the user to input details for a new book. It
        /// validates the inputs and attempts to add the book using the BookService. The user is asked
        /// if they want to perform another operation in this section, allowing for multiple additions.
        /// </remarks>
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
                    Console.WriteLine("\nBook added successfully with the id : " + addedBook.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (!PromptForAnotherOperation()) break;
            }
        }

        /// <summary>
        /// Manages the process of updating an existing book's details in the library.
        /// </summary>
        /// <remarks>
        /// This method clears the console and prompts the user to input the ISBN of the book they wish
        /// to update. If the book is found, the user can update its details. The method then updates
        /// the book using the BookService. The user is given the option to perform another operation
        /// within this section.
        /// </remarks>
        private void ManageUpdateBook()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update an Existing Book");
                Console.WriteLine("=======================\n");

                Console.WriteLine("Would you like to update the book by ID or ISBN?");
                Console.WriteLine("1. By ID");
                Console.WriteLine("2. By ISBN");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                Book book = null;

                try
                {
                    if (choice == "1")
                    {
                        var id = Helper.GetValidInput("Enter Book ID: ");
                        if (int.TryParse(id, out var bookId))
                        {
                            book = _bookService.GetBookById(bookId);
                        }

                        if (book != null)
                        {
                            Console.WriteLine($"\nCurrent ISBN: {book.ISBN}");
                            var newIsbn = Helper.GetValidIsbn();
                            if (!string.IsNullOrWhiteSpace(newIsbn))
                            {
                                book.ISBN = newIsbn;
                            }
                        }
                    }
                    else if (choice == "2")
                    {
                        var isbn = Helper.GetValidIsbn();
                        book = _bookService.GetBookByIsbn(isbn);
                    }

                    if (book == null)
                    {
                        Console.WriteLine("\nBook not found.");
                        if (!PromptForAnotherOperation()) break;
                        continue;
                    }

                    book.Title = Helper.GetValidInput("Enter new Title: ");
                    book.Author = Helper.GetValidInput("Enter new Author: ");
                    book.Year = Helper.GetValidYear();

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
        /// Manages the process of deleting a book from the library.
        /// </summary>
        /// <remarks>
        /// This method clears the console and prompts the user to input the ISBN of the book they wish
        /// to delete. It attempts to delete the book using the BookService and informs the user of the
        /// result. The user is prompted to perform another operation in this section.
        /// </remarks>
        private void ManageDeleteBook()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Delete a Book");
                Console.WriteLine("=============\n");

                Console.WriteLine("Would you like to delete the book by ID or ISBN?");
                Console.WriteLine("1. By ID");
                Console.WriteLine("2. By ISBN");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();


                try
                {

                    bool success = false;

                    if (choice == "1")
                    {
                        var id = Helper.GetValidInput("Enter Book ID: ");
                        if (int.TryParse(id, out var bookId))
                        {
                            success = _bookService.DeleteBookById(bookId);
                        }
                    }
                    else if (choice == "2")
                    {
                        var isbn = Helper.GetValidIsbn();
                        success = _bookService.DeleteBookByIsbn(isbn);
                    }


                    if (success)
                    {
                        Console.WriteLine("\nBook deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nBook not found or could not be deleted.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (!PromptForAnotherOperation()) break;
            }
        }

        /// <summary>
        /// Manages the process of viewing the details of a specific book.
        /// </summary>
        /// <remarks>
        /// This method clears the console and prompts the user to input the ISBN of the book they wish
        /// to view. It retrieves and displays the book's details if found. If the book is not found,
        /// the user is informed. The method allows the user to perform another operation in this section.
        /// </remarks>
        private void ManageViewBookDetails()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("View Book Details");
                Console.WriteLine("=================\n");

                Console.WriteLine("Would you like to view the book details by ID or ISBN?");
                Console.WriteLine("1. By ID");
                Console.WriteLine("2. By ISBN");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                Book book = null;

                try
                {

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
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }


                if (!PromptForAnotherOperation()) break;
            }
        }


        /// <summary>
        /// Lists all the books available in the library.
        /// </summary>
        /// <remarks>
        /// This method clears the console and retrieves a list of all books using the BookService.
        /// It displays the details of each book in the console. If no books are available, it informs
        /// the user. The user is prompted to return to the main menu after viewing the list of books.
        /// </remarks>
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
        /// Prompts the user to perform another operation in the current section.
        /// </summary>
        /// <returns>Returns true if the user wants to perform another operation, false otherwise.</returns>
        /// <remarks>
        /// This method prompts the user to indicate whether they want to perform another operation
        /// in the current section. It helps in keeping the user in the same section until they choose
        /// to exit or proceed to another task.
        /// </remarks>
        private static bool PromptForAnotherOperation()
        {
            Console.WriteLine("\nDo you want to perform another operation in this section? (Y/N)");
            var choice = Console.ReadLine()?.ToUpper();
            return choice == "Y";
        }
    }
}
