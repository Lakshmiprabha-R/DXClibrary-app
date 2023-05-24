Create Database LibraryManagement

Use LibraryManagement

Create Table Users 
(
    UserId int identity PRIMARY KEY,
    Username VARCHAR(50),
    Password VARCHAR(50),
)
insert into Users values('prabha','prabha12'),('lakshmi','lpr3')
Select * from Users
Create Table Books 
(
    BookId int identity PRIMARY KEY,
    Title VARCHAR(50) ,
    Author VARCHAR(30) ,
    Publication VARCHAR(50),
    Stock INT 
)
Create Table Students 
(
    StudentId int identity PRIMARY KEY,
    RollNo VARCHAR(20),
    Name VARCHAR(40),
)
Create Table IssuedBooks
(
IssueId int identity primary key,
StudentId int,
BookId int,
foreign key (StudentId) references Students(StudentId),
foreign key (BookId) references Books(BookId),
)
Select * from Books
Select * from Students
Select * from IssuedBooks

drop Table IssuedBooks


