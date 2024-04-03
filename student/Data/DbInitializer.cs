using Microsoft.EntityFrameworkCore;
using student.Models;

namespace student.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;
        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }
        public void Seed() 
        {
            _builder.Entity<Student>(a =>
            {
                a.HasData(new Student
                {
                    StudentId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                    Name = "Vietnq",
                });
            });
            _builder.Entity<StudentCourse>(b =>
            {
                b.HasData(new StudentCourse
                {
                    StudentId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                    CourseId = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                });
            });
            _builder.Entity<Course>(c =>
            {
                c.HasData(new Course
                {
                    CourseId = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                    CourseName = "Vitcon",
                    Description = "vvv",
                });
            });
        }
    }
}
