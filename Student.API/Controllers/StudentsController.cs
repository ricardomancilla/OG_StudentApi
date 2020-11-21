using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Student.API.DTO;

namespace Student.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IMapper mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            this.studentService = studentService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("students")]
        [ProducesResponseType(typeof(IEnumerable<DataAccess.Model.Student>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await studentService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("student")]
        [ProducesResponseType(typeof(DataAccess.Model.Student), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync(int Id)
        {
            var result = await studentService.GetAsync(Id);

            if (result == null)
            {
                return NotFound($"Student with id {Id} was not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("student")]
        [ProducesResponseType(typeof(DataAccess.Model.Student), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("values provided are invalid.");
            }

            DataAccess.Model.Student student = mapper.Map<DataAccess.Model.Student>(studentDto);

            var result = await studentService.CreateAsync(student);

            student.Id = result;

            return Ok(student);
        }

        [HttpPut]
        [Route("student")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAsync(DataAccess.Model.Student student)
        {
            var result = await studentService.UpdateAsync(student);

            if (!result)
            {
                return NotFound($"Student with id {student.Id} was not found");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("student")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var result = await studentService.DeleteAsync(Id);

            if (!result)
            {
                return NotFound($"Student with id {Id} was not found");
            }

            return Ok(result);
        }
    }
}
