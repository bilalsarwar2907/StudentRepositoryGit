using StudentRepositoryGit.Models;
using StudentRepositoryGit.Repositories;

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
    }
}