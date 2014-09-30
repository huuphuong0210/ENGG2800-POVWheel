using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POVWheel.DataAccess
{
    class USBCommunication
    {
        public static byte[] GetBytesFromCurrentImage()
        {
            int Width = Program.CurrentImage.Width;
            int Height = Program.CurrentImage.Height;
            byte[] BytesArray;
            if (Program.ImageType == 1) //Black-White Image
            {
                BytesArray = new byte[3+(int)Math.Ceiling(Width*Height/8.0)];
                BytesArray[0] = 1; // Black and White Image
                BytesArray[1] = (byte)Width;
                BytesArray[2] = (byte)Height;
                int byteOffSet = 3;
                int bitOffSet = 0;

                //Go thought the bitmap
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        byte mask = (byte)(1 << bitOffSet);
                        if (Program.CurrentImage.GetPixel(x, y) == System.Drawing.Color.Black)
                            BytesArray[byteOffSet] |= mask; // Set the bit
                        bitOffSet++;

                        if (bitOffSet == 8)
                        {
                            bitOffSet = 0;
                            byteOffSet++;
                        }
                    }
                }

            }
            else if (Program.ImageType == 2) //Gray-scale Image
            {
                BytesArray = new byte[Width * Height + 3];
                BytesArray[0] = 2; // Black and White Image
                BytesArray[1] = (byte)Width;
                BytesArray[2] = (byte)Height;
                int byteOffSet = 3;
                for (int y=0; y < Width ; y++){
                    for (int x=0; x < Height; x++){
                        System.Drawing.Color gray = Program.CurrentImage.GetPixel(x,y);
                        BytesArray[byteOffSet] = gray.A;
                        byteOffSet++;
                    }
                }
            }//Color Image
            else
            {
                BytesArray = new byte[Width * Height * 3 + 3];
                BytesArray[0] = 3; // I
                BytesArray[1] = (byte)Width;
                BytesArray[2] = (byte)Height;
                int byteOffSet = 3;
                //Convert RGB Pixel to GrayScale
                for (int y = 0; y < Width; y++)
                {
                    for (int x = 0; x < Height; x++)
                    {
                        byte R = Program.CurrentImage.GetPixel(x,y).R;
                        byte G = Program.CurrentImage.GetPixel(x,y).G;
                        byte B = Program.CurrentImage.GetPixel(x,y).B;
                        byte grayScale = (int)((R * .3) + (G * .59) + (B * .11));

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
