using System.ComponentModel.DataAnnotations;

namespace quanlysinhvien2.Data.Entities;

public class SinhVien : IValidatableObject
{
    [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Mã sinh viên phải có từ 2 đến 15 ký tự.")]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Mã sinh viên chỉ được chứa chữ cái và chữ số.")]
    public string MaSV { get; set; } = string.Empty;

    [Required(ErrorMessage = "Họ tên không được để trống.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ tên phải có từ 2 đến 100 ký tự.")]
    public string HoTen { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ngày sinh không được để trống.")]
    [DataType(DataType.Date)]
    public DateTime NgaySinh { get; set; } = DateTime.Today.AddYears(-18);

    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    [StringLength(10)]
    public string GioiTinh { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email không được để trống.")]
    [StringLength(100)]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Số điện thoại không được để trống.")]
    [RegularExpression(@"^[0-9+()\-\s]{8,20}$", ErrorMessage = "Số điện thoại phải có từ 8 đến 20 ký tự số.")]
    public string SoDienThoai { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn lớp.")]
    [StringLength(10)]
    public string MaLop { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "10", ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
    public decimal Diem { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn trạng thái.")]
    [StringLength(30)]
    public string TrangThai { get; set; } = "Đang học";

    public LopHoc? LopHoc { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (NgaySinh.Date > DateTime.Today)
        {
            yield return new ValidationResult("Ngày sinh không được lớn hơn ngày hiện tại.", [nameof(NgaySinh)]);
        }

        if (!string.IsNullOrWhiteSpace(GioiTinh) && GioiTinh is not ("Nam" or "Nữ" or "Khác"))
        {
            yield return new ValidationResult("Giới tính không hợp lệ.", [nameof(GioiTinh)]);
        }

        if (!string.IsNullOrWhiteSpace(TrangThai) && TrangThai is not ("Đang học" or "Bảo lưu" or "Đã tốt nghiệp"))
        {
            yield return new ValidationResult("Trạng thái sinh viên không hợp lệ.", [nameof(TrangThai)]);
        }
    }

    public bool IsValid(out List<ValidationResult> validationResults)
    {
        validationResults = [];
        return Validator.TryValidateObject(this, new ValidationContext(this), validationResults, validateAllProperties: true);
    }
}
