﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections;

namespace POVWheel.DataAccess
{
    class FileHandling
    {

        public static int ReadMagicNumber(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension.Equals(".BMP") | fileExtension.Equals(".GIF") | fileExtension.Equals(".EXIF")
               | fileExtension.Equals(".JPG") | fileExtension.Equals(".PNG") | fileExtension.Equals(".TIFF"))
            {
                return 6;
            }

            //Initialize the file streem to open and read the file, the files is not shared until the file is closed
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                byte[] bytes = new byte[2];
                fs.Read(bytes, 0, 2); // Read the the magic number from the first two byte of the string
                string magicNumber = System.Text.Encoding.ASCII.GetString(bytes);
                if (magicNumber.Equals("P1")) return 1; // ASCII  Portable Bitmap file
                else if (magicNumber.Equals("P2")) return 2; // ASCII Portable Gray Scale File
                else if (magicNumber.Equals("P4")) return 4; // Binary Portable Bitmap File
                else if (magicNumber.Equals("P5")) return 5; // Binary Portable Gray Scale File
                else if (magicNumber.Equals("P3")) return 3; // ASCII Color File
                else if (magicNumber.Equals("P6")) return 6; // Binary Color File
                else return -1; // Error header or not supported file
            }

        }

        public static System.Drawing.Bitmap ReadImageData(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            fileExtension = fileExtension.ToUpper();

            //Support BMP, GIF, EXIF, JPG, PNG and TIFF
            if (fileExtension.Equals(".BMP") | fileExtension.Equals(".GIF") | fileExtension.Equals(".EXIF")
                | fileExtension.Equals(".JPG") | fileExtension.Equals(".PNG") | fileExtension.Equals(".TIFF"))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);
                return (new Bitmap(image));
            }

            //PGB , PBM, PPM file
            //Widht, Heigh, Maximum Brighness, Data Line
            int[] fileInfo = new int[4] { 0, 0, 0, 0 };
            bool foundAllInfo = false;
            int magicNumber;
            int numberCount = 0; //count the numbers found in the header of the file

            //Get magic number
            magicNumber = DataAccess.FileHandling.ReadMagicNumber(filePath);

            //Initilise StreamReader
            StreamReader myFile = File.OpenText(filePath);

            // Header error or file is not supported - Throw exception
            if (magicNumber == -1) throw new Exception("Header error or file is not supported");

            //Finding width and hight information
            string line = " ";
            while (line != null && !foundAllInfo)
            {
                line = myFile.ReadLine();
                fileInfo[3]++;
                //line = line.Trim(); // Trim the trailing and leading white-space if exsist
                string[] line_partition = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                foreach (string p in line_partition)
                {
                    if (p[0] == '#') break; // Skip comment
                    if (System.Text.RegularExpressions.Regex.IsMatch(p, @"^\d+$"))
                    {
                        fileInfo[numberCount] = Int32.Parse(p);
                        numberCount++;
                    }
                    if (numberCount == 3 | ((magicNumber == 1 | magicNumber == 4) && numberCount == 2))
                    {
                        foundAllInfo = true;
                        break; // Found width, heigh, and maximum brightness
                    }
                }

            }

            // Header error does not have enough information;
            if (line == null) throw new Exception("File header does not have enough information!");

            //ASCII pbm files
            if (magicNumber == 1)
            {
                char[] data = new char[fileInfo[0] * fileInfo[1]];
                int offset = 0;
                line = myFile.ReadLine();
                while (line != null)
                {
                    string[] line_partition = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string p in line_partition)
                    {
                        if (p[0] == '#') break; // Skip comments
                        for (int i = 0; i < p.Length; i++)
                        {
                            data[offset] = p[i];
                            offset++;
                        }
                    }
                    line = myFile.ReadLine();
                }
                myFile.Close();
                //Convert chars array to bitmap
                return (BitMapFromData(data, fileInfo[0], fileInfo[1]));
            }

            //ASCII pgm file 
            if (magicNumber == 2)
            {
                byte[] data = new byte[fileInfo[0] * fileInfo[1]];
                int offset = 0;

                line = myFile.ReadLine();
                while (line != null)
                {
                    string[] line_partition = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string p in line_partition)
                    {
                        if (p[0] == '#') break; // Skip comments
                        data[offset] = (byte)Convert.ToByte(255 * int.Parse(p) / fileInfo[2]);
                        offset++;
                    }
                    line = myFile.ReadLine();
                }

                //Convert chars array to bitmap
                myFile.Close();
                return (BitMapFromData(data, fileInfo[0], fileInfo[1]));

            }

            //ASCII Image File
            if (magicNumber == 3)
            {
                byte[] data = new byte[fileInfo[0] * fileInfo[1] * 3];
                int offset = 0;

                line = myFile.ReadLine();
                while (line != null)
                {
                    string[] line_partition = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string p in line_partition)
                    {
                        if (p[0] == '#') break; // Skip comments
                        data[offset] = (byte)Convert.ToByte(255 * int.Parse(p) / fileInfo[2]);
                        offset++;
                    }
                    line = myFile.ReadLine();
                }

                //Convert chars array to bitmap
                myFile.Close();
                return (BitMapFromDataColor(data, fileInfo[0], fileInfo[1]));
            }

            // Close text reader for opening the binary reader
            myFile.Close();

            //Binary pbm file
            if (magicNumber == 4)
            {
                BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open));
                int lineCount = 0;
                char temp;
                //Move stream postion to data line
                while (lineCount < fileInfo[3])
                {
                    temp = reader.ReadChar();
                    if (temp == '\n')
                        lineCount++;
                }

                byte[] dataBytes = new byte[reader.BaseStream.Length - reader.BaseStream.Position];
                int offset = 0;
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    dataBytes[offset] = reader.ReadByte();
                    offset++;
                }

                //Initialize bit array for storing bit data
                BitArray dataBitsRevered = new BitArray(fileInfo[0] * fileInfo[1]);

                int numberOfByteForRow = (int)Math.Ceiling(fileInfo[0] / 8.0);
                int remainderBit = fileInfo[0] % 8;
                offset = 0;

                for (int k = 0; k < dataBytes.Length; k++)
                {

                    BitArray bits = new BitArray(new Byte[1] { dataBytes[k] });

                    if ((remainderBit != 0) && (((k + 1) % numberOfByteForRow) == 0))
                    {
                        for (int i = 7; i >= (8 - remainderBit); i--)
                        {
                            dataBitsRevered.Set(offset, bits.Get(i));
                            offset++;
                        }
                        continue;
                    }
                    else
                        for (int i = 7; i >= 0; i--)
                        {
                            dataBitsRevered.Set(offset, bits.Get(i));
                            offset++;
                        }
                }

                //Close reader
                reader.Close();

                //Convert chars array to bitmap

                return (BitMapFromData(dataBitsRevered, fileInfo[0], fileInfo[1]));

            }

            //Binary pgm file
            if (magicNumber == 5)
            {
                BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open));
                int lineCount = 0;
                char temp;

                //Move stream postion to data line
                while (lineCount < fileInfo[3])
                {
                    temp = reader.ReadChar();
                    if (temp == '\n')
                        lineCount++;
                }

                byte[] dataBytes = new byte[reader.BaseStream.Length - reader.BaseStream.Position];
                Console.WriteLine("Byte Lenght: " + dataBytes.Length);

                int offset = 0;

                if (fileInfo[2] > 255)
                {   //Two byte per pixel
                    while (offset <= (fileInfo[0] * fileInfo[1]))
                    {
                        //Console.WriteLine(offset);  
                        dataBytes[offset] = (byte)Convert.ToByte(255 * (reader.ReadInt16() / fileInfo[2]));
                        offset++;
                    }
                }
                else
                {   //One byte per pixel
                    while (offset <= (fileInfo[0] * fileInfo[1]))
                    {
                        //Console.WriteLine(offset);  
                        dataBytes[offset] = (byte)Convert.ToByte(255 * reader.ReadByte() / fileInfo[2]);
                        offset++;
                    }
                }


                //Convert chars array to bitmap
                reader.Close();
                return (BitMapFromData(dataBytes, fileInfo[0], fileInfo[1]));

            }

            //Binary ppm file
            if (magicNumber == 6)
            {
                BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open));
                int lineCount = 0;
                char temp;

                //Move stream postion to data line
                while (lineCount < fileInfo[3])
                {
                    temp = reader.ReadChar();
                    if (temp == '\n')
                        lineCount++;
                }

                byte[] dataBytes = new byte[reader.BaseStream.Length - reader.BaseStream.Position];
                Console.WriteLine("Byte Lenght: " + dataBytes.Length);

                int offset = 0;

                if (fileInfo[2] > 255)
                {   //Two byte per pixel
                    while (offset <= (fileInfo[0] * fileInfo[1] * 3))
                    {
                        //Console.WriteLine(offset);  
                        dataBytes[offset] = (byte)Convert.ToByte(255 * (reader.ReadInt16() / fileInfo[2]));
                        offset++;

                    }
                }
                else
                {   //One byte per pixel
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        //Console.WriteLine(offset);  
                        dataBytes[offset] = (byte)Convert.ToByte(255 * reader.ReadByte() / fileInfo[2]);
                        offset++;

                    }
                }
                //Closer stream reader
                reader.Close();
                //Convert chars array to bitmap
                return (BitMapFromDataColor(dataBytes, fileInfo[0], fileInfo[1]));
            }

            //If the function reach to this statement there is unknowns error occured
            throw new Exception("Unknown erorr occurred!");
        }

        public static Bitmap BitMapFromData(char[] pixels, int width, int height)
        {
            //Create new bitmap
            Bitmap resultBitmap = new System.Drawing.Bitmap(width, height);

            //Create image bitmap from character arays
            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    if (pixels[y * resultBitmap.Width + x] == '1')
                    {
                        resultBitmap.SetPixel(x, y, System.Drawing.Color.Black);
                    }
                    else resultBitmap.SetPixel(x, y, System.Drawing.Color.White);
                }
            }
            return resultBitmap;
        }

        public static System.Drawing.Bitmap BitMapFromData(BitArray pixels, int width, int height)
        {
            //Create new bitmap
            Bitmap resultBitmap = new System.Drawing.Bitmap(width, height);

            //Create image bitmap from bit array
            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    if (pixels.Get(y * resultBitmap.Width + x))
                    {
                        resultBitmap.SetPixel(x, y, System.Drawing.Color.Black);
                    }
                    else
                        resultBitmap.SetPixel(x, y, System.Drawing.Color.White);

                }
            }
            return resultBitmap;
        }

        public static Bitmap BitMapFromData(byte[] pixels, int width, int height)
        {
            //Create new bitmap
            Bitmap resultBitmap = new Bitmap(width, height);

            int offset = 0;
            //Create image bitmap from byte array
            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    offset = y * resultBitmap.Width + x;
                    resultBitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(pixels[offset], pixels[offset], pixels[offset]));

                }
            }
            return resultBitmap;

        }

        public static Bitmap BitMapFromDataColor(byte[] pixels, int width, int height)
        {
            //Create new bitmap
            Bitmap resultBitmap = new Bitmap(width, height);

            int offset = 0;
            //Create image bitmap from byte array for color image
            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    offset = y * resultBitmap.Width * 3 + x * 3;
                    resultBitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(pixels[offset], pixels[offset + 1], pixels[offset + 2]));

                }
            }
            return resultBitmap;

        }

        public static void SavingImage(string filePath, int fileType, Bitmap image)
        {
            //Open StreamWrite for writing filse
            using (StreamWriter Writer = new StreamWriter(filePath, false))
            {
                //Write Magic Number
                if (fileType == 1) Writer.WriteLine("P1"); //Black and White Image
                else if (fileType == 2) Writer.WriteLine("P2"); //Gray-scale image
                else if (fileType == 3) Writer.WriteLine("P3"); //Color Image

                //Write Width and Height
                string Size = image.Width + " " + image.Height;
                Writer.WriteLine(Size);

                //Write maximum brighness value if the image is either gray-scale of color
                if (fileType == 2 || fileType == 3) Writer.WriteLine("255");

                //Write Image Data
                if (fileType == 1) //Black-White Image
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            byte R = image.GetPixel(x, y).R;
                            byte G = image.GetPixel(x, y).G;
                            byte B = image.GetPixel(x, y).B;
                            if (R == 0 && G == 0 && B == 0) Writer.Write('1'); //Black Pixel
                            else Writer.Write('0'); //White Pixel
                        }
                        Writer.WriteLine();
                    }
                }

                else if (fileType == 2) //Gray-scale Image
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            byte R = image.GetPixel(x, y).R;
                            Writer.Write(R + " "); //Gray Scale Value
                        }
                        Writer.WriteLine();
                    }
                }

                else if (fileType == 3) //Color Image
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            byte R = image.GetPixel(x, y).R;
                            byte G = image.GetPixel(x, y).G;
                            byte B = image.GetPixel(x, y).B;

                            Writer.WriteLine(R + " " + G + " " + B); //Write RGB value
                        }

                    }
                }

                //Close writer
                Writer.Close();
            }

        }

    }
}
