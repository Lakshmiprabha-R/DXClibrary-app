using Moq;
using NUnit.Framework;
using System;
using Unity;
using System.IO;

namespace LibraryManagementSystem.Tests
{
    [TestFixture]
    public class LibraryManagementSystemTests
    {
        private Mock<ILibraryManagementSystemApp> libraryMock;

        [SetUp]
        public void Setup()
        {
            libraryMock = new Mock<ILibraryManagementSystemApp>();
        }

        [Test]
        public void ValidateUser_InvalidAuthenticaton_Returnstrue()
        {
            var username = "invalidUsername";
            var password = "invalidPassword";

            libraryMock.Setup(x => x.ValidateUser(username, password)).Returns(true);

            var result = libraryMock.Object.ValidateUser(username, password);

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateUser_InvalidAuthentication_Returnsfalse()
        {
            var username = "invalidUsername";
            var password = "invalidPassword";

            libraryMock.Setup(x => x.ValidateUser(username, password)).Returns(false);

            var result = libraryMock.Object.ValidateUser(username, password);

            Assert.IsFalse(result);
        }
        [Test]
        public void AddBook_WhenCalled_ShouldReturnTrue()
        {
            string title = "Book1";
            string author = "Author1";
            string publication = "abc";

            libraryMock.Setup(l => l.AddBook()).Returns(true);

            bool result = libraryMock.Object.AddBook();

            Assert.IsTrue(result);
        }

        [Test]
        public void EditBook_WhenBookExist_ShouldReturnTrue()
        {
            
            string title = "Book1";
            string author = "Author1";
            string publication = "abc";
            int stock = 0;

            libraryMock.Setup(l => l.EditBook()).Returns(true);

            bool result = libraryMock.Object.EditBook();

            libraryMock.Verify(l => l.EditBook(), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void EditBook_WhenBookNotFound_ShouldReturnFalse()
        {
           
            string title = "Book0";
            string author = "Author2";
            string publication = "xyz";
            int stock = 0;

            libraryMock.Setup(l => l.EditBook()).Returns(false);

            bool result = libraryMock.Object.EditBook();

            libraryMock.Verify(l => l.EditBook(), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteBook_WhenBookExists_ShouldReturnTrue()
        {
            int bookId = 4;

            libraryMock.Setup(l => l.DeleteBook()).Returns(true);

            bool result = libraryMock.Object.DeleteBook();

            libraryMock.Verify(l => l.DeleteBook(), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteBook_WhenBookNotFound_ShouldReturnFalse()
        {
            
            int bookId = 3;

            libraryMock.Setup(l => l.DeleteBook()).Returns(false);

            
            bool result = libraryMock.Object.DeleteBook();

            
            libraryMock.Verify(l => l.DeleteBook(), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddStudent_WhenCalled_ReturnsTrue()
        {
            
            string name = "prabha";
            int RollNo = 11;

            libraryMock.Setup(x => x.AddStudent()).Returns(true);

            bool result = libraryMock.Object.AddStudent();

           
            Assert.IsTrue(result);
        }
        [Test]
        public void DeleteStudent_StudentExists_ReturnsTrue()
        {
            
            int studentId = 4;
            libraryMock.Setup(x => x.DeleteStudent()).Returns(true);

            
            bool result = libraryMock.Object.DeleteStudent();

            
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteStudent_StudentNotFound_ReturnsFalse()
        {
           
            int studentId = 5;
            libraryMock.Setup(x => x.DeleteStudent()).Returns(false);

            
            bool result = libraryMock.Object.DeleteStudent();

            
            Assert.IsFalse(result);
        }

    }
}