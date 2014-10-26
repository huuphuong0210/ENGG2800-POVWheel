using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
namespace POVWheel
{
    static class Program
    {
        public static int ImageType; // 1 - Blackwhite | 2 - Grayscale | 3 - Color |  Image Type of Current Display Image
        public static Bitmap CurrentImage; // Current Image Data
        public static string CurrentFileName;
      
        public static System.Drawing.Bitmap OpenImage(String filePath, ref int originalW, ref int originalH)
        {
            //Reading Magic Number 
            int PictureType = DataAccess.FileHandling.readMagicNumber(filePath);

            //Store Image Type
            switch (PictureType)
            {
                case 1: // ASCII Black-White
                    ImageType = 1;
                    break;
                case 2: // ASCII Gray-Scale
                    ImageType = 2;
                    break;
                case 3: // ASCII Color Image
                    ImageType = 3;
                    break;
                case 4: // Binary Black-White
                    ImageType = 1;
                    break;
                case 5: // Binary Gray-Scale
                    ImageType = 2;
                    break;
                case 6: // Binary Color Image
                    ImageType = 3;
                    break;
            }

            //Reading Image Data
            System.Drawing.Bitmap imageBitmap = DataAccess.FileHandling.readData(filePath);

            originalW = imageBitmap.Width;
            originalH = imageBitmap.Height;
            CurrentImage = imageBitmap;

            //return CurrentImage;
            return GetImageForDipslay(imageBitmap);

        }

        public static System.Drawing.Bitmap GetImageForDipslay(Bitmap orginalImage)
        {
            if (orginalImage.Width == 360 && orginalImage.Height == 32) //Return Image If image size = 360x32
            {
                return orginalImage;
            }
            else if (orginalImage.Width <= 360 && orginalImage.Height <= 32) // Image size smaller than 360x32
            {
                Bitmap returnImage = new Bitmap(360, 32); //Prepare image for dipslay - adding background 
                using (Graphics g = Graphics.FromImage((Image)returnImage))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#646464")), new Rectangle(0, 0, 360, 32));
                    g.DrawImage(orginalImage, new Point((360 - orginalImage.Width) / 2, 0));
                }
                return returnImage;

            }
            else // Throw error when image lager than 360x32
            {
                Console.WriteLine("W: " + orginalImage.Width + "H: " + orginalImage.Height);
                throw new Exception("Image size is larger than 360x32 pixels");
            }
        }

        public static System.Drawing.Bitmap CreateNewImage(int imageType, int width, int heigh, string fileName)
        {
            CurrentFileName = fileName;
            ImageType = imageType;
            //Create new bitmap
            CurrentImage = new Bitmap(width, heigh);

            //Add white background
            Graphics g = Graphics.FromImage(CurrentImage);
            g.Clear(Color.White);

            //Create image for displaying
            return GetImageForDipslay(CurrentImage);
        }
        public static System.Drawing.Bitmap renderImageFromText(String textInput)
        {
            Font objFont = new Font("Arial", 26, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            Bitmap image = new Bitmap(360, 32);
            Graphics graphic = Graphics.FromImage(image);

            int intWidth = (int)Math.Floor(graphic.MeasureString(textInput, objFont).Width);
            int intHeight = (int)Math.Floor(graphic.MeasureString(textInput, objFont).Height);

            Console.WriteLine("OrginalW " + intWidth + " OrginalH " + intHeight);

            image = new System.Drawing.Bitmap(image, intWidth, intHeight);
          
            graphic = Graphics.FromImage(image);

            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            graphic.Clear(Color.White);

            //graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphic.DrawString(textInput, objFont, new SolidBrush(Color.FromArgb(0, 0, 0)), 0, 0);
            graphic.Flush();


            Bitmap returnImage;
            if (image.Width == 360 && image.Height == 32)
            {
                returnImage = image;
            }

            else if (image.Width <= 360 && image.Height <= 32)
            {
                returnImage = new Bitmap(360, 32);
                using (Graphics g = Graphics.FromImage((Image)returnImage))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.FillRectangle(System.Drawing.Brushes.White, new Rectangle(0, 0, 360, 32));
                    g.DrawImage(image, new Point((360 - image.Width) / 2, 0));
                }
            }
            else returnImage = resizeBitmap(image, 360, 32);

            CurrentImage = returnImage;
            ImageType = 2;

            return returnImage;
        }

        public static Bitmap getWheelPreview(Bitmap image, int w, int h)
        {
            //Check image size 360x32
            if (image.Width != 360 | image.Height != 32) return new Bitmap(w, h);
            //Create test image

            //Initialize the preview image
            int width = w;
            int heigh = h;
            Bitmap previewImage = new Bitmap(width, heigh);
            Graphics g = Graphics.FromImage(previewImage);

            //Bound rectangle
            Rectangle outerRectangle = new System.Drawing.Rectangle(0, 0, width - 1, heigh - 1);
            //g.DrawRectangle(System.Drawing.Pens.Red, outerRectangle);
            //g.DrawEllipse(System.Drawing.Pens.Black, outerRectangle);

            //Bound centre rectangle
            int innerSquareSize = 65;
            Rectangle innerRectangle = new Rectangle((width - 1) / 2 - innerSquareSize / 2, (heigh - 1) / 2 - innerSquareSize / 2, innerSquareSize, innerSquareSize);
            //g.DrawRectangle(System.Drawing.Pens.Red, innerRectangle);
            //g.DrawEllipse(System.Drawing.Pens.Black, innerRectangle);
            //g.DrawRectangle(System.Drawing.Pens.Red, rectangle);




            int extraCirleSize = (outerRectangle.Width - innerRectangle.Width) / 32;
            //Create 31 circle
            //Console.WriteLine("Outer Size: " + outerRectangle.Width + "Inner Size: " + innerRectangle.Width);
            //Console.WriteLine("ExtraSize " + extraCirleSize);


            int largestW = innerRectangle.Width + 32 * extraCirleSize;
            System.Windows.Vector origin = new System.Windows.Vector(0, 50); // Point down 90 degrees vector
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigh; y++)
                {

                    float length = (float)Math.Sqrt(Math.Pow(width / 2 - x, 2) + Math.Pow(width / 2 - y, 2));
                    int rowPosition = (int)Math.Ceiling((length - (float)innerSquareSize / 2) / ((float)extraCirleSize / 2));

                    if (length > largestW / 2 | length < innerSquareSize / 2)
                    {
                        //previewImage.SetPixel(x, y, System.Drawing.Color.Black);
                    }

                    else
                    {

                        //Row control
                        //if ((rowPosition % 2 == 1) && rowPosition < 33)
                        //    previewImage.SetPixel(x, y, System.Drawing.Color.Red);

                        //Column control
                        System.Windows.Vector vecTemp = new System.Windows.Vector(x - width / 2, y - width / 2);
                        int angle = (int)Math.Ceiling(System.Windows.Vector.AngleBetween(origin, vecTemp));
                        //Normalzie -minute angle
                        if (angle < 0) angle = 360 + angle;
                        else if (angle == 0) angle = 1;
                        if (rowPosition == 0) rowPosition = 1;
                        if (rowPosition != 16) rowPosition = 33 - rowPosition;
                        //if ((angle % 2 == 0) && angle < 0 && angle > -90 )
                        //previewImage.SetPixel(x, y, System.Drawing.Color.Yellow);

                        //Get pixel color from original image
                        //Console.WriteLine("Angle " + angle + "Row " + rowPosition);
                        System.Drawing.Color pixelColour = image.GetPixel(angle - 1, rowPosition - 1);

                        previewImage.SetPixel(x, y, pixelColour);
                    }



                }
            }
            g.DrawEllipse(System.Drawing.Pens.Gray, innerRectangle);

            //Rectangle Control
            Rectangle temp;
            for (int i = 32; i < 33; i++)
            {
                int newW = innerRectangle.Width + i * extraCirleSize;
                //Console.WriteLine(innerRectangle.Width + i * extraCirleSize);
                temp = new System.Drawing.Rectangle((width - 1) / 2 - newW / 2, (width - 1) / 2 - newW / 2, newW, newW);
                //g.DrawRectangle(System.Drawing.Pens.Red, temp);
                g.DrawEllipse(System.Drawing.Pens.Gray, temp);
            }

            //Create 361 line
            //float x1 = width / 2;
            //float y1 = heigh / 2;
            //for (float k = 180; k < 361; k++)
            //{
            //    float x2 = 300 * (float)Math.Cos((k / 180) * Math.PI) + width / 2;
            //    float y2 = 300 * (float)Math.Sin((k / 180) * Math.PI) + heigh / 2;
            //    Console.WriteLine("("+x2+"," + y2+")");
            //    g.DrawLine(System.Drawing.Pens.Black, width / 2, width / 2, x2, y2);
            //}

            // g.DrawRectangle(System.Drawing.Pens.Black,16,16,1,1 );
            //previewImage.SetPixel(16, 16, Color.Black);

            //Test
            //Point O = new Point(width - 1 / 2, width - 1 / 2);

            //System.Windows.Vector o = new System.Windows.Vector(0,-60);
            //System.Windows.Vector b = new System.Windows.Vector(60,0);
            //System.Windows.Vector c = new System.Windows.Vector(-60, 0);


            //Console.WriteLine("Angle " + System.Windows.Vector.AngleBetween(o, b));
            return previewImage;

        }

        public static Bitmap resizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }
            return result;
        }

        //Scale bitmap image to the size of 360x32 
        public static Bitmap resizeToSent(Bitmap Image)
        {
            return null;
        }




        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GUI.MainWindow Window = new GUI.MainWindow();
            Window.ClearFileInfor();
            Application.Run(Window);



            //

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
