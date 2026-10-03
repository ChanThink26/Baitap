using System.ComponentModel.DataAnnotations;

namespace WinFormsApp1.Models;

public sealed class LopHoc : IValidatableObject
{
    [Required(ErrorMessage = "Mã lớp không được để trống.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Mã lớp phải từ 2 đến 20 ký tự.")]
    public string MaLop { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên lớp phải từ 2 đến 100 ký tự.")]
    public string TenLop { get; set; } = string.Empty;

    [Range(1, 200, ErrorMessage = "Sĩ số tối đa phải từ 1 đến 200.")]
    public int SiSoToiDa { get; set; } = 60;

    public List<SinhVien> SinhViens { get; set; } = [];

    public bool IsValid(out List<ValidationResult> errors)
    {
        errors = [];
        return Validator.TryValidateObject(this, new ValidationContext(this), errors, validateAllProperties: true);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SinhViens.Count > SiSoToiDa)
        {
            yield return new ValidationResult("Sĩ số sinh viên vượt quá sĩ số tối đa của lớp.", [nameof(SinhViens), nameof(SiSoToiDa)]);
        }
    }

    public override string ToString() => TenLop;
}
