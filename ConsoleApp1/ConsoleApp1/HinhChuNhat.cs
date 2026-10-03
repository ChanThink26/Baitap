using System;
using System.Collections.Generic;

// ================= INTERFACE =================
public interface IHinh
{
    double GetDienTich();
    double GetChuVi();
    void Nhap();
    void HienThi();
}


// ================= HÌNH TRÒN =================
public class HinhTron : IHinh
{
    private double banKinh;

    public double BanKinh
    {
        get { return banKinh; }
        set
        {
            if (value > 0)
                banKinh = value;
            else
                throw new ArgumentException("Bán kính phải lớn hơn 0!");
        }
    }

    // Constructor
    public HinhTron()
    {
        BanKinh = 1;
    }

    public HinhTron(double banKinh)
    {
        BanKinh = banKinh;
    }

    public double GetDienTich()
    {
        return Math.PI * BanKinh * BanKinh;
    }

    public double GetChuVi()
    {
        return 2 * Math.PI * BanKinh;
    }

    public void Nhap()
    {
        while (true)
        {
            try
            {
                Console.Write("Nhập bán kính: ");
                BanKinh = double.Parse(Console.ReadLine());
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void HienThi()
    {
        Console.WriteLine("\n--- HÌNH TRÒN ---");
        Console.WriteLine("Bán kính: " + BanKinh);
        Console.WriteLine("Diện tích: " + GetDienTich());
        Console.WriteLine("Chu vi: " + GetChuVi());
    }
}


// ================= HÌNH CHỮ NHẬT =================
public class HinhChuNhat : IHinh
{
    private double chieuDai;
    private double chieuRong;

    public double ChieuDai
    {
        get { return chieuDai; }
        set
        {
            if (value > 0)
                chieuDai = value;
            else
                throw new ArgumentException("Chiều dài phải lớn hơn 0!");
        }
    }

    public double ChieuRong
    {
        get { return chieuRong; }
        set
        {
            if (value > 0)
                chieuRong = value;
            else
                throw new ArgumentException("Chiều rộng phải lớn hơn 0!");
        }
    }

    // Constructor
    public HinhChuNhat()
    {
        ChieuDai = 1;
        ChieuRong = 1;
    }

    public HinhChuNhat(double chieuDai, double chieuRong)
    {
        ChieuDai = chieuDai;
        ChieuRong = chieuRong;
    }

    public double GetDienTich()
    {
        return ChieuDai * ChieuRong;
    }

    public double GetChuVi()
    {
        return (ChieuDai + ChieuRong) * 2;
    }

    public void Nhap()
    {
        while (true)
        {
            try
            {
                Console.Write("Nhập chiều dài: ");
                ChieuDai = double.Parse(Console.ReadLine());

                Console.Write("Nhập chiều rộng: ");
                ChieuRong = double.Parse(Console.ReadLine());

                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void HienThi()
    {
        Console.WriteLine("\n--- HÌNH CHỮ NHẬT ---");
        Console.WriteLine("Chiều dài: " + ChieuDai);
        Console.WriteLine("Chiều rộng: " + ChieuRong);
        Console.WriteLine("Diện tích: " + GetDienTich());
        Console.WriteLine("Chu vi: " + GetChuVi());
    }
}


// ================= HÌNH TAM GIÁC =================
public class HinhTamGiac : IHinh
{
    private double a;
    private double b;
    private double c;

    public double A
    {
        get { return a; }
        set
        {
            if (value > 0)
                a = value;
            else
                throw new ArgumentException("Cạnh A phải lớn hơn 0!");
        }
    }

    public double B
    {
        get { return b; }
        set
        {
            if (value > 0)
                b = value;
            else
                throw new ArgumentException("Cạnh B phải lớn hơn 0!");
        }
    }

    public double C
    {
        get { return c; }
        set
        {
            if (value > 0)
                c = value;
            else
                throw new ArgumentException("Cạnh C phải lớn hơn 0!");
        }
    }

    // Constructor
    public HinhTamGiac()
    {
        A = 3;
        B = 4;
        C = 5;
    }

    public HinhTamGiac(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;

        if (!IsTamGiac())
            throw new ArgumentException("Ba cạnh không tạo thành tam giác!");
    }

    // Kiểm tra 3 cạnh có tạo thành tam giác không
    public bool IsTamGiac()
    {
        return A + B > C &&
               A + C > B &&
               B + C > A;
    }

    public double GetChuVi()
    {
        return A + B + C;
    }

    public double GetDienTich()
    {
        double p = GetChuVi() / 2;

        return Math.Sqrt(
            p * (p - A) * (p - B) * (p - C)
        );
    }

    public void Nhap()
    {
        while (true)
        {
            try
            {
                Console.Write("Nhập cạnh A: ");
                A = double.Parse(Console.ReadLine());

                Console.Write("Nhập cạnh B: ");
                B = double.Parse(Console.ReadLine());

                Console.Write("Nhập cạnh C: ");
                C = double.Parse(Console.ReadLine());

                if (IsTamGiac())
                    break;

                Console.WriteLine(
                    "Ba cạnh không tạo thành tam giác! Vui lòng nhập lại."
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void HienThi()
    {
        Console.WriteLine("\n--- HÌNH TAM GIÁC ---");
        Console.WriteLine("Cạnh A: " + A);
        Console.WriteLine("Cạnh B: " + B);
        Console.WriteLine("Cạnh C: " + C);
        Console.WriteLine("Diện tích: " + GetDienTich());
        Console.WriteLine("Chu vi: " + GetChuVi());
    }
}


// ================= PROGRAM =================
class Program
{
    static void Main(string[] args)
    {
        // Danh sách kiểu interface
        List<IHinh> danhSach = new List<IHinh>();

        // Hình tròn
        Console.WriteLine("===== NHẬP HÌNH TRÒN =====");
        IHinh hinhTron = new HinhTron();
        hinhTron.Nhap();
        danhSach.Add(hinhTron);

        // Hình chữ nhật
        Console.WriteLine("\n===== NHẬP HÌNH CHỮ NHẬT =====");
        IHinh hinhChuNhat = new HinhChuNhat();
        hinhChuNhat.Nhap();
        danhSach.Add(hinhChuNhat);

        // Hình tam giác
        Console.WriteLine("\n===== NHẬP HÌNH TAM GIÁC =====");
        IHinh hinhTamGiac = new HinhTamGiac();
        hinhTamGiac.Nhap();
        danhSach.Add(hinhTamGiac);

        // ================= ĐA HÌNH =================
        Console.WriteLine("\n\n========== KẾT QUẢ ==========");

        foreach (IHinh hinh in danhSach)
        {
            hinh.HienThi();
            Console.WriteLine("-----------------------------");
        }

        Console.ReadKey();
    }
}