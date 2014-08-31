using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        /// <param name="image">Return Image</param>
        /// <returns>Return 1 - Reading Success | -1 Errors</returns>
        public static int readData(string filePath, System.Windows.Media.Imaging.BitmapImage image)
        {
            int[] fileInfo = new int[3] {0,0,0};
            bool foundAllInfo = false;
            int magicNumber;
            int numberCount = 0; //count the numbers found in the header of the file

            magicNumber = DataAccess.FileHandling.readMagicNumber(filePath);
            StreamReader myFile = File.OpenText(filePath);
            
            if (magicNumber == -1) return -1; // Header error or not supported file

            //Finding width and hight information
            string line = myFile.ReadLine();
            while (line != null && !foundAllInfo)
            {
                line = line.Trim(); // Trim the trailing and leading white-space if exsist
                string[] line_partition = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                foreach (string p in line_partition)
                {
                    if (p[0] == '#') break; // Skip comment
                    if (System.Text.RegularExpressions.Regex.IsMatch(p, @"^\d+$"))
                    {
                        fileInfo[numberCount] = Int32.Parse(p);
                        Console.WriteLine(p);
                        numberCount++;
                    }
                    if (numberCount == 3 |  ((magicNumber == 1 | magicNumber == 4) && numberCount == 2) )
                    {
                        foundAllInfo = true;
                        break; // Found width, heigh, and maximum brightness
                    }             
                }
                line = myFile.ReadLine(); 
            }
           
            if (line == null) return -1; // Header error does not have enough information;

            char[] data = new char[fileInfo[0] * fileInfo[1]];
            int offset = 0;
            if (magicNumber == 1 | magicNumber == 2)
            {
                while (line != null)
                {
                    //Console.WriteLine(line);
                    string[] line_partition = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string p in line_partition)
                    {
                        if (p[0] == '#') break; // Skip comments
                        for (int i=0; i < p.Length; i++)
                        {
                            data[offset] = p[i];
                            offset++;
                        }
                        
                    }
                    line = myFile.ReadLine(); 
                }
            }
            //Console.WriteLn("GO THERE");
            for (int i = 0; i < (fileInfo[0] * fileInfo[1]); i++)
            {
                Console.Write(data[i]);
            }

            image = bmpFromPBM(data, fileInfo[0], fileInfo[1]);

            if (magicNumber == 4 | magicNumber == 5)
            {
                //while (line != null)
                //{
                //    string[] line_partition = line.split((char[])null, stringsplitoptions.removeemptyentries);
                //    foreach (string p in line_partition)
                //    {
                //        //for (int i = 0; i < p.length; i++)
                //        //{
                //        //    data[offset] = p[i];
                //        //    offset++;
                //        //}
                //        console.writeline(p[0]);

                //    }
                //    line = myfile.readline();
                //}
            }
           
            //Console.WriteLine("OFFSET " + offset);
            return -1;
        }


        public static System.Windows.Media.Imaging.BitmapImage bmpFromPBM(char[] pixels, int width, int height)
        {
            //Remember that pixels is simply a string of "0"s and "1"s. Width and Height are integers.

            int Width = width;
            int Height = height;

            //Create our bitmap
            using (Bitmap B = new Bitmap(Width, Height))
            {
                //Will hold our byte as a string of bits
                //string Bits = null;

                //Current X,Y co-ordinates of the Bitmap object
                int X = 0;
                int Y = 0;

                //Loop through all of the bits
                for (int i = 0; i < pixels.Length; i++)
                {
                    //Below, we're comparing the value with "0". If it is a zero, then we change the pixel white, else make it black.
                    B.SetPixel(X, Y, pixels[i] == '0' ? System.Drawing.Color.White : System.Drawing.Color.Black);

                    //Increment our X position

                    X += 1;//Move along the right

                    //If we're passed the right boundry, reset the X and move the Y to the next line

                    if (X >= Width)
                    {
                        X = 0;//reset
                        Y += 1;//Add another row
                    }
                }

                //return B;
                MemoryStream ms = new MemoryStream();
                B.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Position = 0;
                System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                B.Save(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "Output3.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
                return bi;
                //Output the bitmap to the desktop
               
            }
        }
 
    }
}
