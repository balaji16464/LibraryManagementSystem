---

### **Library Management System - Design Document**

#### **Author:** Balaji Thiruvenkadam  
#### **Date:** August 4, 2024

---

## **1. Overview**

The Library Management System (LMS) is a console-based application designed to manage books in a library. The system allows users to perform CRUD (Create, Read, Update, Delete) operations on books, including adding new books, updating existing ones, deleting them, and viewing their details. The application uses a layered architecture with separation of concerns, following best practices in software design.

## **2. Objectives**

- Implement a simple and effective system for managing a library's book inventory.
- Ensure clean code practices, separation of concerns, and proper design patterns.
- Enable easy testing through unit tests and a well-structured codebase.

## **3. Architecture & Design**

### **3.1. Layered Architecture**

1. **Presentation Layer (LibraryManagement.App)**
   - **Responsibilities:** 
     - Provide a console-based user interface.
     - Handle user inputs and display outputs.
     - Interact with the service layer to perform business operations.
   - **Key Class:** `Menu.cs`

2. **Service Layer (LibraryManagement.Services)**
   - **Responsibilities:** 
     - Implement business logic for managing books.
     - Validate data and handle exceptions.
     - Interact with the repository layer for data access.
   - **Key Class:** `BookService.cs`

3. **Repository Layer (LibraryManagement.Repositories)**
   - **Responsibilities:** 
     - Provide data access and manipulation functionalities.
     - Store books in an in-memory data structure.
     - Implement CRUD operations.
   - **Key Class:** `BookRepository.cs`
   - **Interface:** `IBookRepository.cs`

4. **Model Layer (LibraryManagement.Models)**
   - **Responsibilities:** 
     - Define the data models used across the application.
   - **Key Class:** `Book.cs`

5. **Testing Layer (LibraryManagement.Tests)**
   - **Responsibilities:** 
     - Implement unit tests to validate the functionality of service and repository layers.
     - Ensure code quality and reliability through automated testing.
   - **Framework:** NUnit with Moq

### **3.2. Core Components**

- **Book.cs**
  - Represents a book with properties such as `Id`, `Title`, `Author`, `ISBN`, and `Year`.

- **BookRepository.cs**
  - An in-memory repository that stores and manages `Book` objects.

- **BookService.cs**
  - Contains business logic for adding, updating, deleting, and retrieving books.
  - Handles validation, including ISBN format validation and duplicate checks.

- **Menu.cs**
  - Provides the console UI and handles user interactions, invoking the appropriate service methods based on user input.

### **3.3. Key Features**

- **Add Book:** Users can add a new book with title, author, ISBN, and year details.
- **Update Book:** Users can update the details of an existing book by providing its ISBN or ID.
- **Delete Book:** Users can delete a book by its ISBN or ID.
- **View Book Details:** Users can view detailed information about a specific book.
- **List All Books:** Users can list all books currently in the system.

## **4. Error Handling & Validation**

- **Validation:** Input validation for ISBN, title, author, and year is handled in the service layer.
- **Exceptions:** Custom exceptions and error messages are used to guide users and developers.

## **5. Testing**

- **Unit Tests:** NUnit and Moq are used to write and execute unit tests, ensuring the reliability of the repository and service layers.

---

This design document provides an overview of the entire Library Management System, its structure, key components, and features. The application follows a clean and modular architecture, making it easy to maintain, extend, and test.