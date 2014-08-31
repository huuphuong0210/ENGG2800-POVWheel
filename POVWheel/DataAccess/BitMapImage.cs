using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POVWheel.DataAccess
{
    class BitMapImage
    {
        public int magicNumber;
        public int width;
        public int height;
        public char[] data;
        public BitMapImage(int magicNumber, int width, int height, char[] data)
        {
            this.magicNumber = magicNumber;
            this.width = width;
            this.height = height;
            this.data = data;
        }

        public BitMapImage()
        {
            
        }
    }
}
