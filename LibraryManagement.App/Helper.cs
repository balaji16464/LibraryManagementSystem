using System.Text.RegularExpressions;

namespace LibraryManagement.App
{
    /// <summary>
    /// Provides utility methods for user input validation in the Library Management System.
    /// </summary>
    /// <remarks>
    /// Author: Balaji Thiruvenkadam
    /// Created: 4 August 2024
    /// Purpose:
    /// This file contains static helper methods used for validating user inputs such as year,
    /// general input, and ISBN. It ensures that inputs meet specific criteria before they
    /// are processed further in the application.
    /// </remarks>
    public static class Helper
    {
        /// <summary>
        /// Prompts the user to enter a year and validates the input.
        /// </summary>
        /// <returns>Returns a valid year as an integer.</returns>
        /// <remarks>
        /// This method continuously prompts the user to enter a year until a valid 4-digit year
        /// that is not greater than the current year is provided. It ensures that the year entered
        /// is a positive integer and matches the expected format.
        /// </remarks>
        public static int GetValidYear()
        {
            int year;
            int currentYear = DateTime.Now.Year; // Get the current year
            while (true)
            {
                Console.Write("Enter Year (e.g., 2000, 1995): ");
                if (int.TryParse(Console.ReadLine(), out year))
                {
                    if (year > 0 && year <= currentYear && year.ToString().Length == 4)
                    {
                        return year;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input. Please enter a valid 4-digit year not greater than {currentYear}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid year.");
                }
            }
        }

        /// <summary>
        /// Prompts the user for input and ensures that the input is not empty or whitespace.
        /// </summary>
        /// <param name="prompt">The message to display to the user.</param>
        /// <returns>Returns a non-empty user input string.</returns>
        /// <remarks>
        /// This method repeatedly prompts the user to enter a value until a non-empty string is provided.
        /// It helps ensure that the application receives valid input from the user.
        /// </remarks>
        public static string GetValidInput(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the user to enter an ISBN and validates the format of the input.
        /// </summary>
        /// <returns>Returns a valid ISBN as a string.</returns>
        /// <remarks>
        /// This method prompts the user to enter an ISBN and validates it using a regular expression.
        /// It ensures that the ISBN follows the standard format and is not empty or whitespace.
        /// If the ISBN format is invalid, the user is prompted to try again.
        /// </remarks>
        public static string GetValidIsbn()
        {
            string isbn;
            var isbnPattern = @"^(97(8|9)-?)?\d{1,5}-\d{1,7}-\d{1,7}-[\dX]$";
            while (true)
            {
                Console.Write("Enter ISBN (e.g., 978-3-16-148410-0): ");
                isbn = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(isbn) && Regex.IsMatch(isbn, isbnPattern))
                {
                    return isbn;
                }

                Console.WriteLine("Invalid ISBN format. Please enter a valid ISBN.");
            }
        }
    }
}
