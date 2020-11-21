using AutoMapper;
using Business;
using DataAccess.Contract;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Student.API.Controllers;
using Student.API.DTO;
using Student.Tests.Mapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Student.Tests
{
    public class StudentApiTests
    {
        #region Constants
        private const string StudentNotFound = "Student with id {0} was not found";
        #endregion

        #region Test Methods
        [Fact]
        public async Task GetAllAsync_ReturnsOkAndListOfStudents()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(this.GetStudents());
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());
            var expected = this.GetStudents();

            var result = await studentController.GetAllAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsAssignableFrom<IEnumerable<DataAccess.Model.Student>>(okResult.Value);

            actual.Should().NotBeNull();
            actual.Should().HaveCountGreaterThan(0);
            actual.Should().HaveCount(2);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkAndStudentInfo()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.GetAsync(It.IsAny<int>())).ReturnsAsync(this.GetStudent(It.IsAny<int>()));
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());
            var expected = this.GetStudent(It.IsAny<int>());

            var result = await studentController.GetAsync(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsAssignableFrom<DataAccess.Model.Student>(okResult.Value);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFound()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.GetAsync(It.IsAny<int>())).ReturnsAsync(this.GetStudent_DoesnotExit(It.IsAny<int>()));
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());

            var result = await studentController.GetAsync(It.IsAny<int>());
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            var actual = Assert.IsAssignableFrom<string>(okResult.Value);

            actual.Should().NotBeNullOrWhiteSpace();
            actual.Should().Be(string.Format(StudentNotFound, It.IsAny<int>()));
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedAndStudentInfo()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.CreateAsync(this.GetStudent(It.IsAny<int>()))).ReturnsAsync(It.IsAny<int>());
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());
            var expected = this.GetStudent(It.IsAny<int>());

            var result = await studentController.CreateAsync(this.GetStudentDto());
            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            var actual = Assert.IsAssignableFrom<DataAccess.Model.Student>(okResult.Value);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_StudentExists_ReturnsNoContent()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<DataAccess.Model.Student>())).ReturnsAsync(true);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());
            
            var result = await studentController.UpdateAsync(It.IsAny<DataAccess.Model.Student>());
            var actual = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_StudentDoesnotExist_ReturnsNotFound()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<DataAccess.Model.Student>())).ReturnsAsync(false);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());

            var result = await studentController.UpdateAsync(this.GetStudent(It.IsAny<int>()));
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            var actual = Assert.IsAssignableFrom<string>(okResult.Value);

            actual.Should().NotBeNullOrWhiteSpace();
            actual.Should().Be(string.Format(StudentNotFound, It.IsAny<int>()));
        }

        [Fact]
        public async Task DeleteAsync_StudentExists_ReturnsNoContent()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());

            var result = await studentController.DeleteAsync(It.IsAny<int>());
            var actual = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_StudentDoesnotExist_ReturnsNotFound()
        {
            Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            IStudentService studentService = new StudentService(studentRepositoryMock.Object);
            var studentController = new StudentsController(studentService, this.CreateMapper());

            var result = await studentController.DeleteAsync(It.IsAny<int>());
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            var actual = Assert.IsAssignableFrom<string>(okResult.Value);

            actual.Should().NotBeNullOrWhiteSpace();
            actual.Should().Be(string.Format(StudentNotFound, It.IsAny<int>()));
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

        private StudentDto GetStudentDto()
        {
            return new StudentDto
            {
                Age = 1,
                Career = "Career 1",
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                UserName = "UserName 1"
            };
        }

        private StudentDto GetStudentDtoBadModel()
        {
            return new StudentDto
            {
                Age = 0,
                Career = null,
                FirstName = null,
                LastName = null,
                UserName = null
            };
        }

        private IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            }).CreateMapper();
        }
        #endregion
    }
}
