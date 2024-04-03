using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using student.Models;
using student.Services;

namespace student.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly ILibraryService _libraryService;
        public CourseController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse()
        {
            var courses = await _libraryService.GetCoursesAsync();
            if (courses == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "no courses in database");
            }
            return StatusCode(StatusCodes.Status200OK, courses);
        }
        [HttpPost]
        public async Task<ActionResult<Course>> AddCourse(Course course)
        {
            var dbCourse = await _libraryService.AddCourseAsync(course);
            if (dbCourse == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{course.CourseName} could not be added");
            }
            return CreatedAtAction("GetCourse", new {id = course.CourseId}, course);
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateCourse(Guid id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            Course dbCourse = await _libraryService.UpdateCourseAsync(course);
            if(dbCourse == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{course.CourseName}could not be updated");
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _libraryService.GetCourseAsync(id);
            (bool status, string message) = await _libraryService.DeleteCourseAsync(course);
            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
            return StatusCode(StatusCodes.Status200OK, course);
        }
    }
}
