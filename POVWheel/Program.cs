using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
namespace POVWheel
{
    static class Program
    {
        public static System.Drawing.Bitmap renderImageFromText(String textInput)
        {
            Font objFont = new Font("Arial", 24, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            Bitmap image = new Bitmap(360, 32);
            Graphics graphic = Graphics.FromImage(image);

            Bitmap objBmpImage = new Bitmap(1, 1);
            int intWidth = (int)graphic.MeasureString(textInput, objFont).Width;
            int intHeight = (int)graphic.MeasureString(textInput, objFont).Height;

            image = new System.Drawing.Bitmap(image, intWidth, intHeight);
            graphic = Graphics.FromImage(image);

            graphic.Clear(Color.White);
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphic.DrawString(textInput, objFont, new SolidBrush(Color.FromArgb(0, 0, 0)), 0, 0);
            graphic.Flush();

            return image;
        }

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
