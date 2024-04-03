using student.Models;

namespace student.Services
{
    public interface ILibraryService
    {
        Task<List<Student>> GetStudentsAsync(); // Lấy tất cả Sinh viên
        Task<Student> GetStudentAsync(Guid id, bool includeStudentCourse = false); // Lấy thông tin của một Sinh viên cụ thể
        Task<Student> AddStudentAsync(Student student); // Thêm một Sinh viên mới
        Task<Student> UpdateStudentAsync(Student student); // Cập nhật thông tin của một Sinh viên
        Task<(bool, string)> DeleteStudentAsync(Student student); // Xóa một Sinh viên

        Task<List<Course>> GetCoursesAsync(); // Lấy tất cả các Khoá học
        Task<Course> GetCourseAsync(Guid id, bool includeStudentCourse = false); // Lấy thông tin của một Khoá học cụ thể
        Task<Course> AddCourseAsync(Course course); // Thêm một Khoá học mới
        Task<Course> UpdateCourseAsync(Course course); // Cập nhật thông tin của một Khoá học
        Task<(bool, string)> DeleteCourseAsync(Course course ); // Xóa một Khoá học

        Task<List<StudentCourse>> GetStudentCoursesAsync(); // Lấy tất cả các liên kết Sinh viên - Khoá học
        Task<StudentCourse> GetStudentCourseAsync(Guid studentId, Guid courseId); // Lấy thông tin của một liên kết Sinh viên - Khoá học cụ thể
        Task<StudentCourse> AddStudentCourseAsync(StudentCourse studentCourse); // Thêm một liên kết mới Sinh viên - Khoá học
        Task<StudentCourse> UpdateStudentCourseAsync(StudentCourse studentCourse); // Cập nhật thông tin của một liên kết Sinh viên - Khoá học
        Task<(bool, string)> DeleteStudentCourseAsync(Guid studentId, Guid courseId); // Xóa một liên kết Sinh viên - Khoá học
    }
}
