// Student.cs

using System.ComponentModel.DataAnnotations;

namespace ExampleNamespace
{
    public class Student
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 20,
            ErrorMessage = "Name must be between 20 and 50 characters.")]
        public string Name { get; set; }

        [Range(0, 10)]
        public double DiemToan { get; set; }

        [Range(0, 10)]
        public double Ly { get; set; }

        [Range(0, 10)]
        public double Hoa { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Student()
        {
        }

        public Student(string studentId, string name, double diemToan,
                       double ly, double hoa, string email)
        {
            StudentId = studentId;
            Name = name;
            DiemToan = diemToan;
            Ly = ly;
            Hoa = hoa;
            Email = email;
        }

        public double DiemTrungBinh()
        {
            return (DiemToan + Ly + Hoa) / 3;
        }

        public override string ToString()
        {
            return $"ID: {StudentId} | Name: {Name} | " +
                   $"Toan: {DiemToan} | Ly: {Ly} | Hoa: {Hoa} | " +
                   $"Email: {Email} | DTB: {DiemTrungBinh():F2}";
        }
    }
}