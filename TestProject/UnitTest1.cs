using NUnit.Framework;

namespace LibraryManagementSystem.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        [Test]
        public void AddBook_ValidBook_BookAdded()
        {
            // Arrange
            Library library = new Library();
            Books book = new Books { Id = 1, Title = "Book 1", Author = "Author 1", Publication = "Publication 1", Quantity = 5 };

            // Act
            library.AddBook(book);

            // Assert
            Assert.AreEqual(1, library.GetNumberOfBooks());
        }

       

    }
}
