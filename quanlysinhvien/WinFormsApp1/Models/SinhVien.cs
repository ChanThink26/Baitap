using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WinFormsApp1.Models;

public sealed class SinhVien : IValidatableObject
{
    [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Mã sinh viên phải từ 3 đến 20 ký tự.")]
    [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Mã sinh viên chỉ gồm chữ cái và chữ số.")]
    public string MaSV { get; set; } = string.Empty;

    [Required(ErrorMessage = "Họ và tên không được để trống.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ và tên phải từ 2 đến 100 ký tự.")]
    public string HoTen { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime NgaySinh { get; set; } = DateTime.Today.AddYears(-18);

    [Required(ErrorMessage = "Giới tính không được để trống.")]
    [RegularExpression("^(Nam|Nữ)$", ErrorMessage = "Giới tính chỉ nhận Nam hoặc Nữ.")]
    public string GioiTinh { get; set; } = "Nam";

    [Required(ErrorMessage = "Email không được để trống.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(254, ErrorMessage = "Email không được vượt quá 254 ký tự.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Điện thoại không được để trống.")]
    [RegularExpression(@"^[0-9]{9,12}$", ErrorMessage = "Điện thoại phải gồm 9 đến 12 chữ số.")]
    public string DienThoai { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "10", ErrorMessage = "Điểm phải từ 0 đến 10.")]
    public decimal Diem { get; set; }

    [Required(ErrorMessage = "Trạng thái không được để trống.")]
    [StringLength(30)]
    public string TrangThai { get; set; } = "Đang học";

    [Required(ErrorMessage = "Vui lòng chọn lớp học.")]
    public LopHoc? LopHoc { get; set; }

    public bool IsValid(out List<ValidationResult> errors)
    {
        errors = [];
        return Validator.TryValidateObject(this, new ValidationContext(this), errors, validateAllProperties: true);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (NgaySinh.Date > DateTime.Today)
        {
            yield return new ValidationResult("Ngày sinh không được ở tương lai.", [nameof(NgaySinh)]);
        }

        if (!string.IsNullOrWhiteSpace(HoTen) && Regex.IsMatch(HoTen, @"\d"))
        {
            yield return new ValidationResult("Họ và tên không được chứa chữ số.", [nameof(HoTen)]);
        }
    }
}
