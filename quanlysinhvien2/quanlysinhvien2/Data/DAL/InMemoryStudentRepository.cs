using quanlysinhvien2.Data.Entities;

namespace quanlysinhvien2.Data.DAL;

public sealed class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<LopHoc> _classes =
    [
        new LopHoc { MaLop = "CNTT01", TenLop = "Kỹ thuật phần mềm 01" },
        new LopHoc { MaLop = "CNTT02", TenLop = "Trí tuệ nhân tạo 01" },
        new LopHoc { MaLop = "CNTT03", TenLop = "Khoa học dữ liệu 01" },
        new LopHoc { MaLop = "QTKD01", TenLop = "Quản trị kinh doanh 01" }
    ];

    private readonly List<SinhVien> _students = [];

    public InMemoryStudentRepository()
    {
        AddStudent(new SinhVien
        {
            MaSV = "SV001",
            HoTen = "Nguyễn Văn An",
            NgaySinh = new DateTime(2004, 5, 15),
            GioiTinh = "Nam",
            Email = "an.nguyen@example.com",
            SoDienThoai = "0912345678",
            MaLop = "CNTT01",
            Diem = 8.5m,
            TrangThai = "Đang học"
        });
        AddStudent(new SinhVien
        {
            MaSV = "SV002",
            HoTen = "Trần Thị Bình",
            NgaySinh = new DateTime(2004, 9, 22),
            GioiTinh = "Nữ",
            Email = "binh.tran@example.com",
            SoDienThoai = "0987654321",
            MaLop = "CNTT02",
            Diem = 9.0m,
            TrangThai = "Đang học"
        });
        AddStudent(new SinhVien
        {
            MaSV = "SV003",
            HoTen = "Lê Minh Châu",
            NgaySinh = new DateTime(2003, 12, 8),
            GioiTinh = "Khác",
            Email = "chau.le@example.com",
            SoDienThoai = "0901234567",
            MaLop = "QTKD01",
            Diem = 7.4m,
            TrangThai = "Bảo lưu"
        });
        AddStudent(new SinhVien
        {
            MaSV = "SV004",
            HoTen = "Đỗ Thị Hồng",
            NgaySinh = new DateTime(2004, 11, 30),
            GioiTinh = "Nữ",
            Email = "hong.do@example.com",
            SoDienThoai = "0777888999",
            MaLop = "CNTT03",
            Diem = 8.1m,
            TrangThai = "Đang học"
        });
    }

    public IReadOnlyList<LopHoc> GetClasses() => _classes.AsReadOnly();

    public IReadOnlyList<SinhVien> GetAllStudents() => _students.ToList().AsReadOnly();

    public SinhVien? GetStudentById(string studentId) =>
        _students.FirstOrDefault(student => string.Equals(student.MaSV, studentId.Trim(), StringComparison.OrdinalIgnoreCase));

    public void AddStudent(SinhVien student)
    {
        student.LopHoc = FindClass(student.MaLop);
        _students.Add(student);
        student.LopHoc?.DanhSachSinhVien.Add(student);
    }

    public void UpdateStudent(SinhVien student)
    {
        var existing = GetStudentById(student.MaSV);
        if (existing is null)
        {
            return;
        }

        existing.LopHoc?.DanhSachSinhVien.Remove(existing);
        student.LopHoc = FindClass(student.MaLop);
        var index = _students.IndexOf(existing);
        _students[index] = student;
        student.LopHoc?.DanhSachSinhVien.Add(student);
    }

    public void DeleteStudent(string studentId)
    {
        var student = GetStudentById(studentId);
        if (student is null)
        {
            return;
        }

        student.LopHoc?.DanhSachSinhVien.Remove(student);
        _students.Remove(student);
    }

    private LopHoc? FindClass(string classId) =>
        _classes.FirstOrDefault(classItem => string.Equals(classItem.MaLop, classId, StringComparison.OrdinalIgnoreCase));
}
