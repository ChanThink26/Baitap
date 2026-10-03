using ExampleNamespace;

class Program
{
    static void Main(string[] args)
    {
        StudentDAO dao = new StudentDAO();

        dao.Add(new Student(
            "SV001",
            "Nguyen Van An Nguyen",
            8.5, 7.5, 9.0,
            "an@gmail.com"));

        dao.Add(new Student(
            "SV002",
            "Tran Thi Binh Nguyen",
            7.0, 8.0, 7.5,
            "binh@gmail.com"));

        dao.Add(new Student(
            "SV003",
            "Le Van Cuong Nguyen",
            9.0, 9.5, 8.5,
            "cuong@gmail.com"));

        Console.WriteLine("DANH SACH SINH VIEN:");

        foreach (Student s in dao.GetAll())
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("\nTIM THEO ID:");

        Student? student = dao.GetById("SV002");

        if (student != null)
            Console.WriteLine(student);

        Console.WriteLine("\nTIM THEO TEN:");

        foreach (Student s in dao.GetByName("Binh"))
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("\nSUA:");

        bool edit = dao.Edit(new Student(
            "SV002",
            "Tran Thi Binh Nguyen",
            9.0, 9.0, 9.5,
            "binh_new@gmail.com"));

        Console.WriteLine(edit ? "Sua thanh cong" : "Sua that bai");

        Console.WriteLine("\nXOA:");

        bool delete = dao.Delete("SV001");

        Console.WriteLine(delete ? "Xoa thanh cong" : "Xoa that bai");

        Console.WriteLine("\nDANH SACH SAU KHI SUA XOA:");

        foreach (Student s in dao.GetAll())
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("\nSAP XEP THEO DIEM:");

        foreach (Student s in dao.SortByDiemTrungBinh())
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("\nSINH VIEN DIEM CAO NHAT:");

        Student? best = dao.GetBestStudent();

        if (best != null)
            Console.WriteLine(best);

        Console.WriteLine($"\nSO LUONG SINH VIEN: {dao.Count()}");
    }
}