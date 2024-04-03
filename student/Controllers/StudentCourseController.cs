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
    public class StudentCourseController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public StudentCourseController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentCourses()
        {
            var studentCourses = await _libraryService.GetStudentCoursesAsync();
            if (studentCourses == null || studentCourses.Count == 0)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No student courses in database");
            }
            return StatusCode(StatusCodes.Status200OK, studentCourses);
        }

        [HttpPost]
        public async Task<ActionResult<StudentCourse>> AddStudentCourse(StudentCourse studentCourse)
        {
            var dbStudentCourse = await _libraryService.AddStudentCourseAsync(studentCourse);
            if (dbStudentCourse == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"StudentCourse could not be added");
            }
            return CreatedAtAction("GetStudentCourse", new { studentId = studentCourse.StudentId, courseId = studentCourse.CourseId }, studentCourse);
        }

        [HttpPut("{studentId}/{courseId}")]
        public async Task<IActionResult> UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourse studentCourse)
        {
            if (studentId != studentCourse.StudentId || courseId != studentCourse.CourseId)
            {
                return BadRequest();
            }
            var dbStudentCourse = await _libraryService.UpdateStudentCourseAsync(studentCourse);
            if (dbStudentCourse == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"StudentCourse could not be updated");
            }
            return NoContent();
        }

        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteStudentCourse(Guid studentId, Guid courseId)
        {
            (bool status, string message) = await _libraryService.DeleteStudentCourseAsync(studentId, courseId);
            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
            return StatusCode(StatusCodes.Status200OK, $"StudentCourse with StudentId: {studentId} and CourseId: {courseId} deleted successfully.");
        }
    }
}
