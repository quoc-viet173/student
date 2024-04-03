using Microsoft.EntityFrameworkCore;
using student.Data;
using student.Models;
namespace student.Services

{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;
        public LibraryService(AppDbContext db)
        {
            _db = db;
        }
        #region Courses

        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                return await _db.Courses.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Course> GetCourseAsync(Guid id, bool includeStudentCourse)
        {
            try
            {
                if (includeStudentCourse)
                {
                    return await _db.Courses.Include(c => c.StudentCourses)
                                            .FirstOrDefaultAsync(i => i.CourseId == id);
                }
                return await _db.Courses.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Course> AddCourseAsync(Course course)
        {
            try
            {
                await _db.Courses.AddAsync(course);
                await _db.SaveChangesAsync();
                return await _db.Courses.FindAsync(course.CourseId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Course> UpdateCourseAsync(Course course)
        {
            try
            {
                _db.Entry(course).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return course;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<(bool, string)> DeleteCourseAsync(Course course)
        {
            try
            {
                var dbCourse = await _db.Courses.FindAsync(course.CourseId);
                if (dbCourse == null)
                {
                    return (false, "course could not be found");
                }
                _db.Courses.Remove(course);
                await _db.SaveChangesAsync();
                return (true, "course got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion Courses

        #region Students
        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                return await _db.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Student> GetStudentAsync(Guid id, bool includeStudentCourse)
        {
            try
            {
                if (includeStudentCourse)
                {
                    return await _db.Students.Include(s => s.StudentCourses)
                                              .FirstOrDefaultAsync(i => i.StudentId == id);
                }
                return await _db.Students.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            try
            {
                await _db.Students.AddAsync(student);
                await _db.SaveChangesAsync();
                return await _db.Students.FindAsync(student.StudentId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Student> UpdateStudentAsync(Student student)
        {
            try
            {
                _db.Entry(student).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return student;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<(bool, string)> DeleteStudentAsync(Student student)
        {
            try
            {
                var dbStudent = await _db.Students.FindAsync(student.StudentId);
                if (dbStudent == null)
                {
                    return (false, "Student could not be found.");
                }
                _db.Students.Remove(dbStudent);
                await _db.SaveChangesAsync();
                return (true, "Student got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred. Error Message: {ex.Message}");
            }
        }

        #endregion Students

        #region StudentCourses
        public async Task<List<StudentCourse>> GetStudentCoursesAsync()
        {
            try
            {
                return await _db.StudentsCourses.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<StudentCourse> GetStudentCourseAsync(Guid studentId, Guid courseId)
        {
            try
            {
                return await _db.StudentsCourses.FindAsync(studentId, courseId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<StudentCourse> AddStudentCourseAsync(StudentCourse studentCourse)
        {
            try
            {
                await _db.StudentsCourses.AddAsync(studentCourse);
                await _db.SaveChangesAsync();
                return await _db.StudentsCourses.FindAsync(studentCourse.StudentId, studentCourse.CourseId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<StudentCourse> UpdateStudentCourseAsync(StudentCourse studentCourse)
        {
            try
            {
                _db.Entry(studentCourse).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return studentCourse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteStudentCourseAsync(Guid studentId, Guid courseId)
        {
            try
            {
                var studentCourse = await _db.StudentsCourses.FindAsync(studentId, courseId);
                if (studentCourse == null)
                {
                    return (false, "StudentCourse could not be found.");
                }
                _db.StudentsCourses.Remove(studentCourse);
                await _db.SaveChangesAsync();
                return (true, "StudentCourse got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred. Error Message: {ex.Message}");
            }
        }

       
        #endregion StudentCourses


    }
}
