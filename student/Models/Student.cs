using System.ComponentModel.DataAnnotations;

namespace student.Models
{
    public class Student
    {
        [Key] // Đây là thuộc tính để chỉ ra rằng StudentId là khóa chính của Student
        public Guid StudentId { get; set; }
        public string Name { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }

}
