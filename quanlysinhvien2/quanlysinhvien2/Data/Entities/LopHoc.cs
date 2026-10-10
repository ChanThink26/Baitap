using System.ComponentModel.DataAnnotations;

namespace quanlysinhvien2.Data.Entities;

public class LopHoc
{
    [Required(ErrorMessage = "Mã lớp không được để trống.")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Mã lớp phải có từ 2 đến 10 ký tự.")]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Mã lớp chỉ được chứa chữ cái và chữ số.")]
    public string MaLop { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên lớp phải có từ 2 đến 100 ký tự.")]
    public string TenLop { get; set; } = string.Empty;

    public List<SinhVien> DanhSachSinhVien { get; set; } = [];

    public bool IsValid(out List<ValidationResult> validationResults)
    {
        validationResults = [];
        return Validator.TryValidateObject(this, new ValidationContext(this), validationResults, validateAllProperties: true);
    }
}
