using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public class Program
    {
        private static string connectionString = "Data Source=US-FGRQ8S3;Initial Catalog=LibraryManagement;User Id =sa;password=Lakshmiprabha@2001";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Library Management System!");

            while (true)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Console.WriteLine("Thank you for using the Library Management System.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void Login()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (AuthenticateUser(username, password))
            {
                Console.WriteLine("Login successful!");
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        private static bool AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Add book details");
                Console.WriteLine("2. Edit book details");
                Console.WriteLine("3. Delete book details");
                Console.WriteLine("4. Add student details");
                Console.WriteLine("5. Edit student details");
                Console.WriteLine("6. Delete student details");
                Console.WriteLine("7. Issue book to student");
                Console.WriteLine("8. Return book from student");
                Console.WriteLine("9. Search books by Author Name or Publication Name");
                Console.WriteLine("10. Search student by Student Roll No");
                Console.WriteLine("11. Show the number of students with books");
                Console.WriteLine("12. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBookDetails();
                        break;
                    case "2":
                        EditBookDetails();
                        break;
                    case "3":
                        DeleteBookDetails();
                        break;
                    case "4":
                        AddStudentDetails();
                        break;
                    case "5":
                        EditStudentDetails();
                        break;
                    case "6":
                        DeleteStudentDetails();
                        break;
                    case "7":
                        IssueBookToStudent();
                        break;
                    case "8":
                        ReturnBookFromStudent();
                        break;
                    case "9":
                        SearchBooks();
                        break;
                    case "10":
                        SearchStudents();
                        break;
                    case "11":
                        ShowStudentsWithBooks();
                        break;
                    case "12":
                        Console.WriteLine("Thank you for using the Library Management System.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void AddBookDetails()
        {
            Console.WriteLine("\nAdd Book Details:");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.Write("Publication: ");
            string publication = Console.ReadLine();
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Books (Title, Author, Publication, Stock) VALUES (@Title, @Author, @Publication, @Stock)", connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                command.Parameters.AddWithValue("@Publication", publication);
                command.Parameters.AddWithValue("@Stock", stock);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book details added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add book details.");
                }
            }
        }

        private static void EditBookDetails()
        {
            Console.WriteLine("\nEdit Book Details:");
            Console.Write("Enter Book Id: ");
            int bookId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand("SELECT * FROM Books WHERE BookId = @BookId", connection);
                selectCommand.Parameters.AddWithValue("@BookId", bookId);

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Current Book Details:");
                        Console.WriteLine("Title: " + reader["Title"]);
                        Console.WriteLine("Author: " + reader["Author"]);
                        Console.WriteLine("Publication: " + reader["Publication"]);
                        Console.WriteLine("Stock: " + reader["Stock"]);
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                        return;
                    }
                }

                Console.WriteLine("\nEnter new book details:");
                Console.Write("Title: ");
                string title = Console.ReadLine();
                Console.Write("Author: ");
                string author = Console.ReadLine();
                Console.Write("Publication: ");
                string publication = Console.ReadLine();
                Console.Write("Stock: ");
                int stock = int.Parse(Console.ReadLine());

                SqlCommand updateCommand = new SqlCommand("UPDATE Books SET Title = @Title, Author = @Author, Publication = @Publication, Stock = @Stock WHERE BookId = @BookId", connection);
                updateCommand.Parameters.AddWithValue("@Title", title);
                updateCommand.Parameters.AddWithValue("@Author", author);
                updateCommand.Parameters.AddWithValue("@Publication", publication);
                updateCommand.Parameters.AddWithValue("@Stock", stock);
                updateCommand.Parameters.AddWithValue("@BookId", bookId);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book details updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update book details.");
                }
            }
        }

        private static void DeleteBookDetails()
        {
            Console.WriteLine("\nDelete Book Details:");
            Console.Write("Enter Book Id: ");
            int bookId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Books WHERE BookId = @BookId", connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book details deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete book details.");
                }
            }
        }

        private static void AddStudentDetails()
        {
            Console.WriteLine("\nAdd Student Details:");
            Console.Write("Roll No: ");
            string rollNo = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Students (RollNo, Name) VALUES (@RollNo, @Name)", connection);
                command.Parameters.AddWithValue("@RollNo", rollNo);
                command.Parameters.AddWithValue("@Name", name);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Student details added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add student details.");
                }
            }
        }

        private static void EditStudentDetails()
        {
            Console.WriteLine("\nEdit Student Details:");
            Console.Write("Enter Student Id: ");
            int studentId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand("SELECT * FROM Students WHERE StudentId = @StudentId", connection);
                selectCommand.Parameters.AddWithValue("@StudentId", studentId);

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Current Student Details:");
                        Console.WriteLine("Roll No: " + reader["RollNo"]);
                        Console.WriteLine("Name: " + reader["Name"]);
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }
                }

                Console.WriteLine("\nEnter new student details:");
                Console.Write("Roll No: ");
                string rollNo = Console.ReadLine();
                Console.Write("Name: ");
                string name = Console.ReadLine();

                SqlCommand updateCommand = new SqlCommand("UPDATE Students SET RollNo = @RollNo, Name = @Name WHERE StudentId = @StudentId", connection);
                updateCommand.Parameters.AddWithValue("@RollNo", rollNo);
                updateCommand.Parameters.AddWithValue("@Name", name);
                updateCommand.Parameters.AddWithValue("@StudentId", studentId);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Student details updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update student details.");
                }
            }
        }

        private static void DeleteStudentDetails()
        {
            Console.WriteLine("\nDelete Student Details:");
            Console.Write("Enter Student Id: ");
            int studentId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Students WHERE StudentId = @StudentId", connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Student details deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete student details.");
                }
            }
        }

        private static void IssueBookToStudent()
        {
            Console.WriteLine("\nIssue Book to Student:");
            Console.Write("Enter Book Id: ");
            int bookId = int.Parse(Console.ReadLine());
            Console.Write("Enter Student Id: ");
            int studentId = int.Parse(Console.ReadLine());
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                SqlCommand bookCommand = new SqlCommand("SELECT * FROM Books WHERE BookId = @BookId AND Stock > 0", connection);
                bookCommand.Parameters.AddWithValue("@BookId", bookId);
               

                using (SqlDataReader bookReader = bookCommand.ExecuteReader())
                {
                    if (!bookReader.Read())
                    {
                        Console.WriteLine("Book not found or out of stock.");
                        return;
                    }
                }

                // Check if the student exists
                SqlCommand studentCommand = new SqlCommand("SELECT * FROM Students WHERE StudentId = @StudentId", connection);
                studentCommand.Parameters.AddWithValue("@StudentId", studentId);

                using (SqlDataReader studentReader = studentCommand.ExecuteReader())
                {
                    if (!studentReader.Read())
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }
                }

               
                SqlCommand issueCommand = new SqlCommand("INSERT INTO IssuedBooks (BookId, StudentId) VALUES (@BookId, @StudentId)", connection);
                issueCommand.Parameters.AddWithValue("@BookId", bookId);
                issueCommand.Parameters.AddWithValue("@StudentId", studentId);

                int rowsAffected = issueCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book issued successfully.");
                    
                    SqlCommand updateStockCommand = new SqlCommand("UPDATE Books SET Stock = Stock - 1 WHERE BookId = @BookId", connection);
                    updateStockCommand.Parameters.AddWithValue("@BookId", bookId);
                    updateStockCommand.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Failed to issue the book.");
                }
            }
        }

        private static void ReturnBookFromStudent()
        {
            Console.WriteLine("\nReturn Book from Student:");
            Console.Write("Enter Book Id: ");
            int bookId = int.Parse(Console.ReadLine());
            Console.Write("Enter Student Id: ");
            int studentId = int.Parse(Console.ReadLine());
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the book is issued to the student
                SqlCommand issuedCommand = new SqlCommand("SELECT * FROM IssuedBooks WHERE BookId = @BookId AND StudentId = @StudentId ", connection);
                issuedCommand.Parameters.AddWithValue("@BookId", bookId);
                issuedCommand.Parameters.AddWithValue("@StudentId", studentId);
                

                using (SqlDataReader issuedReader = issuedCommand.ExecuteReader())
                {
                    if (!issuedReader.Read())
                    {
                        Console.WriteLine("Book is not issued to the student.");
                        return;
                    }
                }

                // Return the book
                SqlCommand returnCommand = new SqlCommand("DELETE FROM IssuedBooks WHERE BookId = @BookId AND StudentId = @StudentId", connection);
                returnCommand.Parameters.AddWithValue("@BookId", bookId);
                returnCommand.Parameters.AddWithValue("@StudentId", studentId);

                int rowsAffected = returnCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book returned successfully.");
                    // Increase the stock of the book by 1
                    SqlCommand updateStockCommand = new SqlCommand("UPDATE Books SET Stock = Stock + 1 WHERE BookId = @BookId", connection);
                    updateStockCommand.Parameters.AddWithValue("@BookId", bookId);
                    updateStockCommand.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Failed to return the book.");
                }
            }
        }

        private static void SearchBooks()
        {
            Console.WriteLine("\nSearch Books:");
            Console.Write("Enter Author or Publication Name: ");
            string searchQuery = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Books WHERE Author LIKE @SearchQuery OR Publication LIKE @SearchQuery", connection);
                command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Search Results:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("BookId\tTitle\t\tAuthor\t\tPublication\tStock");
                    Console.WriteLine("--------------------------------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["BookId"]}\t{reader["Title"]}\t\t{reader["Author"]}\t\t{reader["Publication"]}\t{reader["Stock"]}");
                    }
                }
            }
        }

        private static void SearchStudents()
        {
            Console.WriteLine("\nSearch Students:");
            Console.Write("Enter Student Roll No: ");
            string searchQuery = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students WHERE RollNo = @SearchQuery", connection);
                command.Parameters.AddWithValue("@SearchQuery", searchQuery);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Search Results:");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("StudentId\tRollNo\t\tName");
                    Console.WriteLine("-------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["StudentId"]}\t\t{reader["RollNo"]}\t\t{reader["Name"]}");
                    }
                }
            }
        }

        private static void ShowStudentsWithBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM IssuedBooks ", connection);

                try
                {
                    int studentCount = Convert.ToInt32(command.ExecuteScalar());
                    Console.WriteLine("Number of students with books: " + studentCount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

    }
}
