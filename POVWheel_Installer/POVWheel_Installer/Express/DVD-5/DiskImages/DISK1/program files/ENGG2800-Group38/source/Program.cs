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
        //[Image Type of Current Display Image] 1 - Blackwhite | 2 - Grayscale | 3 - Color 
        public static int CurrentImageType;
        //Current display image bitmap
        public static Bitmap CurrentImage;
        //Current image's file_name
        public static string CurrentFileName; 

        public static Bitmap OpenImage(String filePath, ref int originalW, ref int originalH)
        {
            //Reading Magic Number 
            int pictureType = DataAccess.FileHandling.ReadMagicNumber(filePath);

            //Store Image Type
            switch (pictureType)
            {
                case 1: // ASCII Black-White
                    CurrentImageType = 1;
                    break;
                case 2: // ASCII Gray-Scale
                    CurrentImageType = 2;
                    break;
                case 3: // ASCII Color Image
                    CurrentImageType = 3;
                    break;
                case 4: // Binary Black-White
                    CurrentImageType = 1;
                    break;
                case 5: // Binary Gray-Scale
                    CurrentImageType = 2;
                    break;
                case 6: // Binary Color Image
                    CurrentImageType = 3;
                    break;
            }

            //Reading Image Data
            System.Drawing.Bitmap imageBitmap = DataAccess.FileHandling.ReadImageData(filePath);

            originalW = imageBitmap.Width;
            originalH = imageBitmap.Height;

            //Set Global Image 
            CurrentImage = imageBitmap;

            //Create image for displaying
            return GetImageToDipslay(imageBitmap);

        }

        public static Bitmap GetImageToDipslay(Bitmap orginalImage)
        {
            if (orginalImage.Width == 360 && orginalImage.Height == 32) //Return Image If image size = 360x32
            {
                return orginalImage;
            }
            else if (orginalImage.Width <= 360 && orginalImage.Height <= 32) // Image size smaller than 360x32
            {
                Bitmap returnImage = new Bitmap(360, 32); //Prepare image for dipslay - Adding background 

                using (Graphics g = Graphics.FromImage((Image)returnImage))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    //Adding Background
                    g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#646464")), new Rectangle(0, 0, 360, 32));
                    g.DrawImage(orginalImage, new Point((360 - orginalImage.Width) / 2, 0));
                }
                return returnImage;

            }
            else // Throw error when image lager than 360x32
            {
                throw new Exception("Image size is larger than 360x32 pixels");
            }
        }

        public static Bitmap CreateNewImage(int imageType, int width, int heigh, string fileName)
        {
            CurrentFileName = fileName;
            CurrentImageType = imageType;

            //Create new bitmap
            CurrentImage = new Bitmap(width, heigh);

            //Add white background
            Graphics graphic = Graphics.FromImage(CurrentImage);
            graphic.Clear(Color.White);

            //Create image for displaying
            return GetImageToDipslay(CurrentImage);
        }

        public static Bitmap RenderImageFromText(String textInput)
        {
            Font fontArial = new Font("Arial", 26, FontStyle.Bold, GraphicsUnit.Pixel);
            //Create Bitmap for measuring the size of string
            Bitmap image = new Bitmap(360, 32);
            Graphics graphic = Graphics.FromImage(image);

            //Measuring the size of the text
            int intWidth = (int)Math.Floor(graphic.MeasureString(textInput, fontArial).Width);
            int intHeight = (int)Math.Floor(graphic.MeasureString(textInput, fontArial).Height);

            //Create new image bitmap base on calculated size
            image = new Bitmap(image, intWidth, intHeight);
            graphic = Graphics.FromImage(image);

            //Setup the rendering options
            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            graphic.Clear(Color.Black);

            //graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //Create White Color Brush for drawing
            SolidBrush blackBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            //Draw the string on image
            graphic.DrawString(textInput, fontArial, blackBrush, 0, 0);
            graphic.Flush();

            Bitmap returnImage;

            //Scale image size to 360x32
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
                    g.FillRectangle(System.Drawing.Brushes.Black, new Rectangle(0, 0, 360, 32));
                    g.DrawImage(image, new Point((360 - image.Width) / 2, 0));
                }
            }
            else returnImage = ResizeBitmap(image, 360, 32);

            //Set the Program Current Image
            CurrentImage = returnImage;
            CurrentImageType = 2;

            return returnImage;
        }

        public static Bitmap GetWheelPreviewImage(Bitmap image, int width, int height)
        {
            //The size of the input image is different form 360x32 throw error;
            if (image.Width != 360 | image.Height != 32) throw new Exception("Image's size different from 360x32");

            //Initialize the preview image
            Bitmap previewImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(previewImage);

            //Outer bound rectangle
            Rectangle outerRectangle = new System.Drawing.Rectangle(0, 0, width - 1, height - 1);

            //Inner rectangle
            int innerSquareSize = 65; //Adjust this value to optimize the result 
            Rectangle innerRectangle = new Rectangle((width - 1) / 2 - innerSquareSize / 2, (height - 1) / 2 - innerSquareSize / 2, innerSquareSize, innerSquareSize);

            //Calculate the increment value for the circle radius
            int extraCirleSize = (outerRectangle.Width - innerRectangle.Width) / 32;

            int largestW = innerRectangle.Width + 32 * extraCirleSize;

            //// Create a vector (point down 90 degrees)
            System.Windows.Vector origin = new System.Windows.Vector(0, 50);

            //Revert mapping each pixel in the preview image back to the input image
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    float length = (float)Math.Sqrt(Math.Pow(width / 2 - x, 2) + Math.Pow(width / 2 - y, 2));
                    //Row control
                    int rowPosition = (int)Math.Ceiling((length - (float)innerSquareSize / 2) / ((float)extraCirleSize / 2));

                    if (!(length > largestW / 2 | length < innerSquareSize / 2))
                    {

                        //Column control
                        System.Windows.Vector vecTemp = new System.Windows.Vector(x - width / 2, y - width / 2);
                        int angle = (int)Math.Ceiling(System.Windows.Vector.AngleBetween(origin, vecTemp));

                        //Normalzie negative angle
                        if (angle < 0) angle = 360 + angle;
                        else if (angle == 0) angle = 1;

                        if (rowPosition == 0) rowPosition = 1;
                        if (rowPosition != 16) rowPosition = 33 - rowPosition;

                        System.Drawing.Color pixelColour = image.GetPixel(angle - 1, rowPosition - 1);

                        previewImage.SetPixel(x, y, pixelColour);
                    }

                }
            }

            //Drawing inner circle
            g.DrawEllipse(System.Drawing.Pens.Gray, innerRectangle);

            //Return previewImage
            return previewImage;

        }

        public static Bitmap ResizeBitmap(Bitmap bitmap, int nWidth, int nHeight)
        {
            //Create result bitmap
            Bitmap result = new Bitmap(nWidth, nHeight);

            //Resize the image to requirement size
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.DrawImage(bitmap, 0, 0, nWidth, nHeight);
            }

            //Return the result
            return result;
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Initialize the main window & run the application 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GUI.MainWindow Window = new GUI.MainWindow();
            Window.ClearFileInfor();
            Application.Run(Window);
        }
    }
}
