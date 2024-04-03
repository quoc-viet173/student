using System.ComponentModel.DataAnnotations;

namespace student.Models
{
    public class StudentCourse
    {
        [Key]
        public Guid StudentId { get; set; }

        [Key]
        public Guid CourseId { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }

}
