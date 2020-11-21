using Business;
using DataAccess.Contract;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Student.Tests
{
    public class StudentServiceTests
    {
        #region Test Methods
        [Fact]
        public async Task GetAllAsync_ReturnsListOfStudents()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(this.GetStudents());
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            IEnumerable<DataAccess.Model.Student> expected = this.GetStudents();

            IEnumerable<DataAccess.Model.Student> actual = await studentService.GetAllAsync();

            actual.Should().NotBeNull();
            actual.Should().HaveCountGreaterThan(0);
            actual.Should().HaveCount(2);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync_StudentIdExists_ReturnsStudentInfo()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.GetAsync(It.IsAny<int>())).ReturnsAsync(this.GetStudent(It.IsAny<int>()));
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            DataAccess.Model.Student expected = this.GetStudent(It.IsAny<int>());

            DataAccess.Model.Student actual = await studentService.GetAsync(It.IsAny<int>());

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync_StudentIdDoesnotExist_ReturnsNullObject()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.GetAsync(It.IsAny<int>())).ReturnsAsync(this.GetStudent_DoesnotExit(It.IsAny<int>()));
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);

            DataAccess.Model.Student actual = await studentService.GetAsync(It.IsAny<int>());

            actual.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedStudentId()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<DataAccess.Model.Student>())).ReturnsAsync(1);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            int expected = 1;

            int actual = await studentService.CreateAsync(It.IsAny<DataAccess.Model.Student>());

            actual.Should().Be(expected);
        }

        [Fact]
        public async Task UpdateAsync_StudentExists_ReturnsTrue()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<DataAccess.Model.Student>())).ReturnsAsync(true);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            bool expected = true;

            bool actual = await studentService.UpdateAsync(It.IsAny<DataAccess.Model.Student>());

            actual.Should().Be(expected);
        }

        [Fact]
        public async Task UpdateAsync_StudentDoesnotExist_ReturnsFalse()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<DataAccess.Model.Student>())).ReturnsAsync(false);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            bool expected = false;

            bool actual = await studentService.UpdateAsync(It.IsAny<DataAccess.Model.Student>());

            actual.Should().Be(expected);
        }

        [Fact]
        public async Task DeleteAsync_StudentExists_ReturnsTrue()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            bool expected = true;

            bool actual = await studentService.DeleteAsync(It.IsAny<int>());

            actual.Should().Be(expected);
        }

        [Fact]
        public async Task DeleteAsync_StudentDoesnotExist_ReturnsFalse()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            bool expected = false;

            bool actual = await studentService.DeleteAsync(It.IsAny<int>());

            actual.Should().Be(expected);
        }
        #endregion

        #region Private Methods
        private IEnumerable<DataAccess.Model.Student> GetStudents()
        {
            return new List<DataAccess.Model.Student>
            {
                new DataAccess.Model.Student
                {
                    Id =  1,
                    Age = 1,
                    Career = "Career 1",
                    FirstName = "FirstName 1",
                    LastName = "LastName 1",
                    UserName = "UserName 1"
                },
                new DataAccess.Model.Student
                {
                    Id =  2,
                    Age = 2,
                    Career = "Career 2",
                    FirstName = "FirstName 2",
                    LastName = "LastName 2",
                    UserName = "UserName 2"
                }
            };
        }

        private DataAccess.Model.Student GetStudent(int Id)
        {
            return new DataAccess.Model.Student
            {
                Id = Id,
                Age = 1,
                Career = "Career 1",
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                UserName = "UserName 1"
            };
        }

        private DataAccess.Model.Student GetStudent_DoesnotExit(int Id)
        {
            return null;
        }
        #endregion
    }
}
