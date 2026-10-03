// StudentDAO.cs

using System.ComponentModel.DataAnnotations;

namespace ExampleNamespace
{
    public class StudentDAO
    {
        private List<Student> students = new List<Student>();

        public bool Add(Student student)
        {
            if (student == null || Exists(student.StudentId))
                return false;

            if (!Validate(student))
                return false;

            students.Add(student);
            return true;
        }

        public bool Edit(Student student)
        {
            if (student == null || !Validate(student))
                return false;

            Student oldStudent = GetById(student.StudentId);

            if (oldStudent == null)
                return false;

            oldStudent.Name = student.Name;
            oldStudent.DiemToan = student.DiemToan;
            oldStudent.Ly = student.Ly;
            oldStudent.Hoa = student.Hoa;
            oldStudent.Email = student.Email;

            return true;
        }

        public bool Delete(string id)
        {
            Student student = GetById(id);

            if (student == null)
                return false;

            students.Remove(student);
            return true;
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public Student? GetById(string id)
        {
            return students.FirstOrDefault(
                s => s.StudentId.Equals(
                    id,
                    StringComparison.OrdinalIgnoreCase));
        }

        public List<Student> GetByName(string name)
        {
            return students
                .Where(s => s.Name.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool Exists(string id)
        {
            return students.Any(
                s => s.StudentId.Equals(
                    id,
                    StringComparison.OrdinalIgnoreCase));
        }

        public int Count()
        {
            return students.Count;
        }

        public List<Student> SortByName()
        {
            return students
                .OrderBy(s => s.Name)
                .ToList();
        }

        public List<Student> SortByDiemTrungBinh()
        {
            return students
                .OrderByDescending(s => s.DiemTrungBinh())
                .ToList();
        }

        public List<Student> GetPassStudents()
        {
            return students
                .Where(s => s.DiemTrungBinh() >= 5)
                .ToList();
        }

        public Student? GetBestStudent()
        {
            return students
                .OrderByDescending(s => s.DiemTrungBinh())
                .FirstOrDefault();
        }

        public bool Validate(Student student)
        {
            var context = new ValidationContext(student);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(
                student,
                context,
                results,
                true);
        }
    }
}