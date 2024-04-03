using System.ComponentModel.DataAnnotations;

namespace student.Models
{
    public class Course
    {
        [Key] // Đây là thuộc tính để chỉ ra rằng CourseId là khóa chính của Course
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }

}
