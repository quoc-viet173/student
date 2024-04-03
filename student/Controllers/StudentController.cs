using Microsoft.AspNetCore.Mvc;
using student.Models;
using student.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace student.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public StudentController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _libraryService.GetStudentsAsync();
            if (students == null || students.Count == 0)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No students in database");
            }
            return StatusCode(StatusCodes.Status200OK, students);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var dbStudent = await _libraryService.AddStudentAsync(student);
            if (dbStudent == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{student.Name} could not be added");
            }
            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
            var dbStudent = await _libraryService.UpdateStudentAsync(student);
            if (dbStudent == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{student.Name} could not be updated");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _libraryService.GetStudentAsync(id);
            (bool status, string message) = await _libraryService.DeleteStudentAsync(student);
            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
            return StatusCode(StatusCodes.Status200OK, student);
        }
    }
}
