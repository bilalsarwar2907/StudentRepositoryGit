using StudentRepositoryGit.Models;
using StudentRepositoryGit.Repositories;
using System.Xml.Linq;

namespace StudentRepositoryGit.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Get_EmptyRepository_ReturnsEmptyList()
        {
            // Arrange
            var repository = new StudentsRepository();

            // Act
            var result = repository.Get();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetById_ValidId_ReturnsStudent()
        {
            // Arrange
            var repository = new StudentsRepository();
            var student = new Student { Name = "Alice", BirthYear = 2000 };
            var added = repository.Add(student);

            // Act
            var result = repository.GetById(added.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice", result.Name);
            Assert.Equal(2000, result.BirthYear);
        }
        [Fact]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Arrange
            var repository = new StudentsRepository();
            // Act
            var result = repository.GetById(999);
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void Add_ValidStudent_ReturnsAddedStudent()
        {
            //Arrange
            var repository = new StudentsRepository();

            var student = new Student { Name = "Bob", BirthYear = 1995 };
            var added = repository.Add(student);

            // Assert
            Assert.NotNull(added);
            Assert.Equal(1, added.Id);
            Assert.Equal("Bob", added.Name);
        }
        [Fact]
        public void Delete_ValidStudent_ReturnsRemoveStudent()
        {
            //Arrange
            var repository = new StudentsRepository();

            var student = new Student { Name = "Bob", BirthYear = 1995 };
            var addedStudent = repository.Add(student);

            // Act

            var studentToRemove = repository.Delete(addedStudent.Id);

            // Assert
            Assert.NotNull(studentToRemove);
            Assert.Equal(1, studentToRemove.Id);
            Assert.Equal("Bob", studentToRemove.Name);
            Assert.Equal(1995, studentToRemove.BirthYear);

        }
        [Fact]
        public void Delete_InvalidStudent_ReturnsNull()
        {
            //Arrange
            var repository = new StudentsRepository();
            // Act
            var studentToRemove = repository.Delete(999);
            // Assert
            Assert.Null(studentToRemove);

        }
        [Fact]
        public void Update_ValidStudent_ReturnsUpdatedStudent()
        {
            //Arrange
            var repository = new StudentsRepository();
            var student = new Student { Name = "Bob", BirthYear = 1995 };
            var addedStudent = repository.Add(student);
            var updatedData = new Student { Name = "Robert", BirthYear = 1994 };
            // Act
            var updatedStudent = repository.Update(addedStudent.Id, updatedData);
            // Assert
            Assert.NotNull(updatedStudent);
            Assert.Equal(addedStudent.Id, updatedStudent.Id);
            Assert.Equal("Robert", updatedStudent.Name);
            Assert.Equal(1994, updatedStudent.BirthYear);
        }
        [Fact]
        public void Filter_ValidStudent_Birthyear()
        {
            // Arrange
            var repository = new StudentsRepository();

            var student1 = new Student { Name = "Bob", BirthYear = 1990 };
            var student2 = new Student { Name = "Alice", BirthYear = 2000 };
            var student3 = new Student { Name = "Charlie", BirthYear = 2010 };

            repository.Add(student1);
            repository.Add(student2);
            repository.Add(student3);

            // Act
            var result = repository.Get(2005, null, null);

            // Assert
            // (You will check the count and which students are returned)
            //Assert.Equal(2, result.Count);
            Assert.Contains(student1, result);
            Assert.Contains(student2, result);
            Assert.DoesNotContain(student3, result);
        }
    }
}