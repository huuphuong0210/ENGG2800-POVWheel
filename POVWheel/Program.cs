using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace POVWheel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());

            //Console.Write(DataAccess.FileHandling.readMagicNumber(@"C:\Users\HuuPhuong\Desktop\demofile.pbm"));
            //int magicNumber;
            //int width;
            //int height;
            //int temp;
            //string filePath = @"C:\Users\HuuPhuong\Desktop\demofile.pbm";
            //System.Windows.Media.Imaging.BitmapImage image = new System.Windows.Media.Imaging.BitmapImage();
            //DataAccess.FileHandling.readData(filePath, image);
            //Console.ReadLine();
            
        }
    }
}
