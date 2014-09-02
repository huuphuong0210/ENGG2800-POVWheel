using System;
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
        /// <summary>
        /// Reading the magic number of .pgm or .pbm files
        /// </summary>
        /// <param name="filePath"> The file path in the system.</param>
        /// <returns>
        /// Return 1, 2, 4, 5 for P1, P2, P4, P5 magic number respectively otherwise return -1
        /// </returns>
        public static int readMagicNumber(string filePath)
        {
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
                else return -1; // Error header or not supported file
            }

        }

        /// <summary>
        /// Read data from image File
        /// </summary>
        /// <param name="filePath">THe file path in the system</param>
        /// <param name="bitmapImage">Return Image</param>
        /// <returns>Return 1 - Reading Success | -1 Errors</returns>
        public static System.Drawing.Bitmap readData(string filePath)
        {
            int[] fileInfo = new int[4] {0,0,0,0}; // Widht, Heigh, Maximum Brighness, Data Line
            bool foundAllInfo = false;
            int magicNumber;
            int numberCount = 0; //count the numbers found in the header of the file

            //Get magic number
            magicNumber = DataAccess.FileHandling.readMagicNumber(filePath);
            
            //Initilise StreamReader
            StreamReader myFile = File.OpenText(filePath);
           
            // Header error or file is not supported
            if (magicNumber == -1) return (new System.Drawing.Bitmap(360,32)); 

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
                    if (numberCount == 3 |  ((magicNumber == 1 | magicNumber == 4) && numberCount == 2) )
                    {
                        foundAllInfo = true;
                        break; // Found width, heigh, and maximum brightness
                    }             
                }
                
            }

            if (line == null) return (new System.Drawing.Bitmap(360, 32)); // Header error does not have enough information;

            //ASCII pbm files
            if (magicNumber == 1)
            {
                char[] data = new char[fileInfo[0] * fileInfo[1]];
                int offset = 0;
                line = myFile.ReadLine();
                while (line != null)
                {
                    //Console.WriteLine(line);
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
                return (bitMapFromData(data, fileInfo[0], fileInfo[1]));
            }
            
            //ASCII pgm file 
            else if (magicNumber == 2)
            {

                Console.WriteLine("Maximum Brightness" + fileInfo[2]);
                
                byte[] data = new byte[fileInfo[0] * fileInfo[1]];
                int offset = 0;

                line = myFile.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    string[] line_partition = line.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string p in line_partition)
                    {
                        if (p[0] == '#') break; // Skip comments
                        data[offset] = (byte)Convert.ToByte(255*int.Parse(p)/fileInfo[2]);
                        offset++;
                    }
                    line = myFile.ReadLine();
                }

                //Console.WriteLine("Image Data");
                //for (int i = 0; i < (fileInfo[0] * fileInfo[1]); i++)
                //{
                //    Console.WriteLine(data[i]);
                //}

                //Convert chars array to bitmap
                myFile.Close();
                return (bitMapFromData(data, fileInfo[0], fileInfo[1]));

            }

            myFile.Close(); // Close Text Reader
            //Binary pbm file
            if (magicNumber == 4)
            {
                Console.WriteLine("W: " + fileInfo[0] + " H: " + fileInfo[1]);
                Console.WriteLine("LIne: " + fileInfo[3]);
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
                Console.WriteLine("LIneCount: " + lineCount);

                byte[] dataBytes = new byte[reader.BaseStream.Length - reader.BaseStream.Position];
                int offset = 0;
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    dataBytes[offset] = reader.ReadByte();
                    offset++;
                    //Console.WriteLine(offset);                    
                }
                
                //BitArray dataBits = new BitArray(dataBytes);
                BitArray dataBitsRevered = new BitArray(fileInfo[0]*fileInfo[1]);
                Console.WriteLine("Bits length" + dataBitsRevered.Length);
                int numberOfByteForRow = (int)Math.Ceiling(fileInfo[0] / 8.0);
                int remainderBit = fileInfo[0] % 8;
                Console.WriteLine("Row " + numberOfByteForRow + " Remainder " + remainderBit);
                offset = 0;

                for (int k = 0; k < dataBytes.Length; k++ )
                {

                    BitArray bits = new BitArray(new Byte[1] { dataBytes[k] });
                  
                    if ( (remainderBit != 0) && (((k+1) % numberOfByteForRow) == 0) )
                    {
                        Console.WriteLine("Byte Number: " + k + "Bit Length:" + bits.Length);
                        for (int i = 7; i >= (8 - remainderBit); i--)
                        {
                            //Console.WriteLine("OFFSET:" + offset);
                            dataBitsRevered.Set(offset, bits.Get(i));
                            offset++;
                        }
                        continue;
                    }
                    else
                    for (int i = 7; i >= 0; i--)
                    {
                        //Console.WriteLine("OFFSET:" + offset);
                        dataBitsRevered.Set(offset, bits.Get(i));
                        offset++;
                    }
                }

                //Convert chars array to bitmap
                reader.Close();
                return (bitMapFromData(dataBitsRevered, fileInfo[0], fileInfo[1]));
                
            }

            //Binary pgm file
            if (magicNumber == 5)
            {
                Console.WriteLine("W: " + fileInfo[0] + " H: " + fileInfo[1]);
                Console.WriteLine("Max: " + fileInfo[2]);
                Console.WriteLine("Line: " + fileInfo[3]);

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
                Console.WriteLine("LIneCount: " + lineCount);

                byte[] dataBytes = new byte[reader.BaseStream.Length - reader.BaseStream.Position];
                Console.WriteLine("Byte Lenght: " + dataBytes.Length);

                int offset = 0;
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    //Console.WriteLine(offset);  
                    dataBytes[offset] = (byte)Convert.ToByte(255 * reader.ReadByte() / fileInfo[2]);
                    Console.WriteLine("["+offset+"] "+dataBytes[offset]);
                    offset++;
                                      
                }
               
             
                //Convert chars array to bitmap
                reader.Close();
                return (bitMapFromData(dataBytes, fileInfo[0], fileInfo[1]));

            }


            return (new System.Drawing.Bitmap(360, 32)); // Error
          
        }

        public static System.Drawing.Bitmap bitMapFromData(char[] pixels, int width, int height)
        {
            System.Drawing.Bitmap resultBitmap = new System.Drawing.Bitmap(width, height);
          
            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                   // Console.WriteLine("X= " + x + "Y= " + y);
                    if (pixels[y * resultBitmap.Width + x] == '1')
                    {
                        resultBitmap.SetPixel(x, y, System.Drawing.Color.Black);                       
                    }
                    else
                        resultBitmap.SetPixel(x, y, System.Drawing.Color.White);

                }
            }
            return resultBitmap;
        }

        public static System.Drawing.Bitmap bitMapFromData(BitArray pixels, int width, int height)
        {
            System.Drawing.Bitmap resultBitmap = new System.Drawing.Bitmap(width, height);

            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    // Console.WriteLine("X= " + x + "Y= " + y);
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
        public static System.Drawing.Bitmap bitMapFromData(byte[] pixels, int width, int height) 
        {
            System.Drawing.Bitmap resultBitmap = new System.Drawing.Bitmap(width, height);
   
            int index = 0;
            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    index = y * resultBitmap.Width + x;
                    //Console.WriteLine("X= " + x + "Y= " + y);
                    resultBitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(pixels[index], pixels[index], pixels[index]));

                }
            }
            return resultBitmap;
 
        }
    }
}
