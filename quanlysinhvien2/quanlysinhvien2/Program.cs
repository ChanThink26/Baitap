using quanlysinhvien2.Bus;
using quanlysinhvien2.Data.DAL;
using quanlysinhvien2.View;

namespace quanlysinhvien2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var repository = new InMemoryStudentRepository();
            var studentService = new SinhVienService(repository);
            Application.Run(new Form1(studentService));
        }
    }
}