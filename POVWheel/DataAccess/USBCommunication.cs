using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace POVWheel.DataAccess
{
    class USBCommunication
    {
        public static byte[] GetBytesFromCurrentImage()
        {
            int Width = Program.CurrentImage.Width;
            int Height = Program.CurrentImage.Height;
            byte[] BytesArray;

            Bitmap FinalImage = new Bitmap(360, 32); //Prepare image for transfer

            if (Width < 360 || Height < 32) // Adding black background for image size smaller than 360x32
            {
                using (Graphics G = Graphics.FromImage((Image)FinalImage))
                {
                    G.InterpolationMode = InterpolationMode.NearestNeighbor;
                    G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    G.FillRectangle(Brushes.Black, new Rectangle(0, 0, 360, 32));
                    G.DrawImage(Program.CurrentImage, new Point((360 - Width) / 2, 0));
                }
            }
            else FinalImage = Program.CurrentImage;

            if (Program.ImageType == 1) //Black-White Image
            {
                Console.WriteLine("Black and White Image");
                BytesArray = new byte[360 * 32];
                int byteOffset = 0;

                //Go thought the bitmap
                for (int x = 0; x < 360; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        byte R = FinalImage.GetPixel(x, y).R;
                        byte G = FinalImage.GetPixel(x, y).G;
                        byte B = FinalImage.GetPixel(x, y).B;
                        if (R == 0 && G == 0 && B == 0){
                            //Console.WriteLine("WHITE");
                            BytesArray[byteOffset] = 255; //Set the white byte
                        }
                            
                        else BytesArray[byteOffset] = 0; //Set the black byte
                        byteOffset++;
                    }
                }


            }
            else if (Program.ImageType == 2) //Gray-scale Image
            {
                BytesArray = new byte[360 * 32];
                int byteOffSet = 0;
                for (int x = 0; x < 360; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        System.Drawing.Color gray = FinalImage.GetPixel(x, y);
                        BytesArray[byteOffSet] = gray.A;
                        byteOffSet++;
                    }
                }
            }//Color Image
            else
            {
                BytesArray = new byte[360 * 32];
                int byteOffSet = 0;
                //Convert RGB Pixel to GrayScale
                for (int x = 0; x < 360; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        byte R = FinalImage.GetPixel(x, y).R;
                        byte G = FinalImage.GetPixel(x, y).G;
                        byte B = FinalImage.GetPixel(x, y).B;
                        byte grayScale = (byte)((R * .3) + (G * .59) + (B * .11));

                        BytesArray[byteOffSet] = grayScale;
                        byteOffSet++;
                    }
                }
            }

            return BytesArray;
        }

        public static int UploadData(string comPort, int baudRate)
        {
            System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort(comPort, baudRate);
            return 1;
        }
    }
}
