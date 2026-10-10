using quanlysinhvien2.Data.Entities;

namespace quanlysinhvien2.Data.DAL;

public interface IStudentRepository
{
    IReadOnlyList<LopHoc> GetClasses();
    IReadOnlyList<SinhVien> GetAllStudents();
    SinhVien? GetStudentById(string studentId);
    void AddStudent(SinhVien student);
    void UpdateStudent(SinhVien student);
    void DeleteStudent(string studentId);
}
