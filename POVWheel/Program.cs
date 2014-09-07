using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace POVWheel
{
    static class Program
    {
        public static System.Drawing.Bitmap openImage(String filePath)
        {
            
            System.Drawing.Bitmap imageBitmap = DataAccess.FileHandling.readData(filePath);
            if (imageBitmap.Width == 360 && imageBitmap.Height == 32) {
                return imageBitmap;
            }
            
            else if (imageBitmap.Width <= 360 && imageBitmap.Height <= 32)
            {
                Bitmap returnImage = new Bitmap(360, 32);
                using (Graphics g = Graphics.FromImage((Image)returnImage))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.FillRectangle(System.Drawing.Brushes.White,new Rectangle(0,0,360,32));
                    g.DrawImage(imageBitmap,new Point((360-imageBitmap.Width)/2,0));
                }
                return returnImage;

            }
            else
            {
                ////////////////////////////////////////////////////////CHUA LAM
            }
            return null;
        }

        public static System.Drawing.Bitmap renderImageFromText(String textInput)
        {
            Font objFont = new Font("Ariald", 26, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            Bitmap image = new Bitmap(360, 32);
            Graphics graphic = Graphics.FromImage(image);
            int intWidth = (int)Math.Floor(graphic.MeasureString(textInput, objFont).Width);
            int intHeight = (int)Math.Floor(graphic.MeasureString(textInput, objFont).Height);

            Console.WriteLine("OrginalW " + intWidth + " OrginalH " + intHeight);

            image = new System.Drawing.Bitmap(image, intWidth, intHeight);
            graphic = Graphics.FromImage(image);

            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            graphic.Clear(Color.White);

            //graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphic.DrawString(textInput, objFont, new SolidBrush(Color.FromArgb(0, 0, 0)), 0, 0);
            graphic.Flush();
            
            if (image.Width == 360 && image.Height == 32)
            {
                return image;
            }

            else if (image.Width <= 360 && image.Height <= 32)
            {
                Bitmap returnImage = new Bitmap(360, 32);
                using (Graphics g = Graphics.FromImage((Image)returnImage))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.FillRectangle(System.Drawing.Brushes.White, new Rectangle(0, 0, 360, 32));
                    g.DrawImage(image, new Point((360 - image.Width) / 2, 0));
                }
                return returnImage;

            }
            else return resizeBitmap(image, 360, 32);
        }

        public static Bitmap getWheelPreview(Bitmap image, int w, int h)
        {
            //Check image size 360x32
            if (image.Width != 360 | image.Height != 32) return new Bitmap(w, h);
            //Create test image
            
            //Initialize the preview image
            int width = w;
            int heigh = h;
            Bitmap previewImage = new Bitmap(width,heigh);
            Graphics g = Graphics.FromImage(previewImage);
            
            //Bound rectangle
            Rectangle outerRectangle = new System.Drawing.Rectangle(0, 0, width-1, heigh-1);
            //g.DrawRectangle(System.Drawing.Pens.Red, outerRectangle);
            //g.DrawEllipse(System.Drawing.Pens.Black, outerRectangle);

            //Bound centre rectangle
            int innerSquareSize = 77 ;
            Rectangle innerRectangle = new Rectangle((width - 1) / 2 - innerSquareSize / 2, (heigh - 1) / 2 - innerSquareSize / 2, innerSquareSize, innerSquareSize);
            //g.DrawRectangle(System.Drawing.Pens.Red, innerRectangle);
            //g.DrawEllipse(System.Drawing.Pens.Black, innerRectangle);
            //g.DrawRectangle(System.Drawing.Pens.Red, rectangle);

          
            

            int extraCirleSize = (outerRectangle.Width - innerRectangle.Width)/32;
            //Create 31 circle
            Console.WriteLine("Outer Size: " + outerRectangle.Width + "Inner Size: " + innerRectangle.Width);
            Console.WriteLine("ExtraSize " + extraCirleSize);
           
            
            int largestW = innerRectangle.Width + 32 * extraCirleSize;
            System.Windows.Vector origin = new System.Windows.Vector(0, 50); // Point down 90 degrees vector
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigh; y++)
                {
                    
                    float length = (float)Math.Sqrt(Math.Pow(width/2 - x,2) + Math.Pow(width/2 - y,2));
                    int rowPosition = (int)Math.Ceiling((length - (float)innerSquareSize / 2) / ((float)extraCirleSize / 2));
                    
                    if (length > largestW/2| length < innerSquareSize/2){
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
                        System.Drawing.Color pixelColour = image.GetPixel(angle -1 , rowPosition -1);

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

        public  static Bitmap resizeBitmap(Bitmap b, int nWidth, int nHeight)
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
