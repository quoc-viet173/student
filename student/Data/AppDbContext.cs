using Microsoft.EntityFrameworkCore;
using student.Models;
using System.Reflection.Emit;

namespace student.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set;}
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<StudentCourse>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentCourses);



            builder.Entity<StudentCourse>()
                .HasOne(x => x.Course)
               .WithMany(x => x.StudentCourses);

            new DbInitializer(builder).Seed();
        }
    }
}
