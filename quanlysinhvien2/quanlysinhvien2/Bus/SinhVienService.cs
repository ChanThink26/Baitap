using quanlysinhvien2.Data.DAL;
using quanlysinhvien2.Data.Entities;

namespace quanlysinhvien2.Bus;

public sealed class SinhVienService(IStudentRepository repository)
{
    public IReadOnlyList<LopHoc> GetClasses() => repository.GetClasses();

    public IReadOnlyList<SinhVien> GetAllStudents() => repository.GetAllStudents();

    public SinhVien? GetStudentById(string studentId) => repository.GetStudentById(studentId);

    public IReadOnlyList<SinhVien> SearchStudents(string? keyword, string? classId, decimal? minimumScore)
    {
        var normalizedKeyword = keyword?.Trim();
        return repository.GetAllStudents()
            .Where(student => string.IsNullOrWhiteSpace(normalizedKeyword) ||
                student.MaSV.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                student.HoTen.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                student.Email.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                student.SoDienThoai.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase))
            .Where(student => string.IsNullOrWhiteSpace(classId) ||
                string.Equals(student.MaLop, classId, StringComparison.OrdinalIgnoreCase))
            .Where(student => !minimumScore.HasValue || student.Diem >= minimumScore.Value)
            .ToList();
    }

    public bool TryAddStudent(SinhVien student, out string error)
    {
        Normalize(student);
        if (!ValidateStudent(student, out error))
        {
            return false;
        }

        if (repository.GetStudentById(student.MaSV) is not null)
        {
            error = "Mã sinh viên đã tồn tại.";
            return false;
        }

        repository.AddStudent(student);
        error = string.Empty;
        return true;
    }

    public bool TryUpdateStudent(SinhVien student, out string error)
    {
        Normalize(student);
        if (!ValidateStudent(student, out error))
        {
            return false;
        }

        if (repository.GetStudentById(student.MaSV) is null)
        {
            error = "Không tìm thấy sinh viên cần sửa.";
            return false;
        }

        repository.UpdateStudent(student);
        error = string.Empty;
        return true;
    }

    public bool TryDeleteStudent(string studentId, out string error)
    {
        if (repository.GetStudentById(studentId) is null)
        {
            error = "Không tìm thấy sinh viên cần xóa.";
            return false;
        }

        repository.DeleteStudent(studentId);
        error = string.Empty;
        return true;
    }

    private bool ValidateStudent(SinhVien student, out string error)
    {
        if (!student.IsValid(out var validationResults))
        {
            error = string.Join(Environment.NewLine, validationResults.Select(result => result.ErrorMessage));
            return false;
        }

        if (!repository.GetClasses().Any(classItem =>
                string.Equals(classItem.MaLop, student.MaLop, StringComparison.OrdinalIgnoreCase)))
        {
            error = "Lớp học được chọn không tồn tại.";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private static void Normalize(SinhVien student)
    {
        student.MaSV = student.MaSV.Trim();
        student.HoTen = student.HoTen.Trim();
        student.GioiTinh = student.GioiTinh.Trim();
        student.Email = student.Email.Trim();
        student.SoDienThoai = student.SoDienThoai.Trim();
        student.MaLop = student.MaLop.Trim();
        student.TrangThai = student.TrangThai.Trim();
    }
}
