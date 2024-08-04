# Library Management System

## Overview

The Library Management System is a .NET console application designed to manage a collection of books. It allows users to add, update, delete, list, and view book details. The application uses a repository pattern to handle data operations and follows SOLID principles for a clean and maintainable codebase.

## Features

- **Add a New Book**: Allows users to add new books to the system.
- **Update Existing Book**: Allows users to update details of an existing book by ISBN.
- **Delete a Book**: Allows users to remove a book from the system by ISBN.
- **List All Books**: Displays a list of all books in the system.
- **View Book Details**: Provides detailed information about a specific book by ISBN.

## Technologies

- **.NET Core**: The framework used for building the application.
- **NUnit**: Used for unit testing.
- **Moq**: Used for mocking dependencies in unit tests.

## Setup Instructions

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) (recommended version 2022 or later) with the .NET desktop development workload installed.

### Installation

1. **Clone the Repository**

   Open a terminal or command prompt and run the following commands:

   ```bash
   git clone https://github.com/balaji16464/LibraryManagementSystem.git
   cd LibraryManagementSystem

2.**Open in Visual Studio**

  To open Visual Studio  Select File > Open > Project/Solution.
  Navigate to the LibraryManagementSystem directory and open the LibraryManagementSystem.sln file.

**3. Restore Dependencies**

  In Visual Studio, restore the required NuGet packages by right-clicking on the solution in Solution Explorer and selecting Restore NuGet Packages.

**4. Build the Solution**

   Build the solution by selecting Build > Build Solution from the top menu or pressing Ctrl+Shift+B.

**5. Run the Application**

  Set LibraryManagement.App as the startup project (right-click on LibraryManagement.App in Solution Explorer and select Set as Startup Project).
  Press F5 to run the application in Debug mode.
  
**Usage**

1. **Add a Book:** Select the option to add a new book, then provide the required details such as title, author, ISBN, and publication year. 
2. **Update a Book:** Select the option to update a book, provide the ISBN of the book to be updated, and enter the new details.
3. **Delete a Book:** Select the option to delete a book by ISBN.
4. **List All Books:** View a list of all books currently in the system.
5. **View Book Details:** View detailed information about a book by entering its ISBN.

**Testing**

Unit tests are included to verify the functionality of the repository and service layers. To run the tests in Visual Studio:

Open the LibraryManagement.Tests project in Solution Explorer.
Select Test > Run All Tests from the top menu.

# Contact
For any questions or feedback, please contact balaji16464@gmail.com
