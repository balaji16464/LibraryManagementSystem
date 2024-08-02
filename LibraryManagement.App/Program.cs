using LibraryManagement.Repositories;
using LibraryManagement.Services;

namespace LibraryManagement.App
{
    /// <summary>
    /// Entry point for the Library Management Console Application.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// Purpose:
    /// This file contains the entry point for the Library Management System console application.
    /// It initializes the necessary components and displays the main menu to the user.
    /// </remarks>
    internal static class Program
    {
        /// <summary>
        /// Main method to start the application.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        /// <remarks>
        /// This method creates instances of <see cref="BookRepository"/> and <see cref="BookService"/>,
        /// and then initializes a <see cref="Menu"/> instance to handle user interactions.
        /// The main menu is displayed to the user for interaction.
        /// </remarks>
        static void Main()
        {
            var bookRepository = new BookRepository();
            var bookService = new BookService(bookRepository);
            var menu = new Menu(bookService);

            menu.DisplayMainMenu();
        }
    }
}
