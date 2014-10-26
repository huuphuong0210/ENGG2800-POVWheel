using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
namespace POVWheel.DataAccess
{
    class USBCommunication
    {
        public static byte[] GetBytesFromCurrentImage()
        {
            int width = Program.CurrentImage.Width;
            int height = Program.CurrentImage.Height;
            byte[] bytesArray;

            //Prepare image for transfering
            Bitmap finalImage = new Bitmap(360, 32);

            if (width < 360 || height < 32) // Adding black background for image size smaller than 360x32
            {
                using (Graphics G = Graphics.FromImage((Image)finalImage))
                {
                    G.InterpolationMode = InterpolationMode.NearestNeighbor;
                    G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    G.FillRectangle(Brushes.Black, new Rectangle(0, 0, 360, 32));
                    G.DrawImage(Program.CurrentImage, new Point((360 - width) / 2, 0));
                }
            }
            else finalImage = Program.CurrentImage;

            if (Program.CurrentImageType == 1) //Black-White Image
            {
                Console.WriteLine("Black and White Image");
                bytesArray = new byte[360 * 32];
                int byteOffset = 0;

                //Go thought the bitmap
                for (int x = 0; x < 360; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        byte R = finalImage.GetPixel(x, y).R;
                        byte G = finalImage.GetPixel(x, y).G;
                        byte B = finalImage.GetPixel(x, y).B;
                        if (R == 0 && G == 0 && B == 0)
                        {
                            //Console.WriteLine("WHITE");
                            bytesArray[byteOffset] = 0; //Set the white byte
                        }
                        else bytesArray[byteOffset] = 255; //Set the black byte
                        byteOffset++;
                    }
                }


            }
            else if (Program.CurrentImageType == 2) //Gray-scale Image
            {
                bytesArray = new byte[360 * 32];
                int byteOffSet = 0;
                for (int x = 0; x < 360; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        byte R = finalImage.GetPixel(x, y).R;
                        byte G = finalImage.GetPixel(x, y).G;
                        byte B = finalImage.GetPixel(x, y).B;
                        bytesArray[byteOffSet] = R;
                        byteOffSet++;
                    }
                }
            }
            else //Color Image
            {
                bytesArray = new byte[360 * 32];
                int byteOffSet = 0;
                //Convert RGB Pixel to GrayScale
                for (int x = 0; x < 360; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {
                        byte R = finalImage.GetPixel(x, y).R;
                        byte G = finalImage.GetPixel(x, y).G;
                        byte B = finalImage.GetPixel(x, y).B;
                        byte grayScale = (byte)((R * .3) + (G * .59) + (B * .11));

                        bytesArray[byteOffSet] = grayScale;
                        byteOffSet++;
                    }
                }
            }

            //Return the result
            return bytesArray;
        }
        public static void UploadData(string comPort, int baudRate)
        {
            System.IO.Ports.SerialPort Com = new System.IO.Ports.SerialPort(comPort, baudRate, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.Two);
            byte[] Data = GetBytesFromCurrentImage(); // Get image data for sending

            Com.Open(); //Open port for transfering data
            
            //Setting timeout for writing and reading for 5 seconds
            Com.ReadTimeout = 5000;
            Com.WriteTimeout = 5000;

            //Handshaking
            Com.Write("A");
            //Acknowledge from the microcontroller
            Com.ReadByte();

            int offset = 0;
            //Sending data to the com port
            while (offset < 32 * 360)
            {
                //Sending 4 column at a time
                Com.Write(Data, offset, 32 * 4);
                //Wating for ackknowledge
                Com.ReadByte();
                offset += 32 * 4;
            }
            
            Com.Close(); //Close port


        }
    }
}
