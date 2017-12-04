using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DuplicateFinder
{
    class StandaloneHashFunctions
    {
        public static string AverageHash64(Image image)
        {
            //Buggy
            // Squeeze the image into an 16x16 canvas
            Bitmap squeezed = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 16, 16);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[256];
            uint averageValue = 0;
            for (int y = 0; y < 16; y++)
                for (int x = 0; x < 16; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 16)] = (byte)gray;
                    averageValue += gray;
                }
            return(matrixToHashAverage(grayscale, averageValue));
        }

        public static string matrixToHashAverage(byte[] grayscale, uint average)
        {

            string hash = "";
            for (int i = 0; i < grayscale.Length; i++)
                if (grayscale[i] >= average)
                { hash += "1"; }
                else { hash += "0"; }


            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }


            return Convert.ToString(result);
        }

        public static string HashReduce(string stringy)
        {
            int numChunk = 16; //8x8 matrix => 64 bits => 16 byte hash => 16^16 Possible =/= Pseudo-unique

            float Avgs = 0;
            float totalAvgs = 0;

            List<float> avgList = new List<float>();
            List<int> preMatrix = new List<int>();

            while (stringy.Length % numChunk != 0)
            {
                stringy += "A";
            }
            //System.Console.WriteLine("Padded Input is " + stringy);

            int chunkLen = stringy.Length / numChunk;

            for (int i = 0; i < stringy.Length; i += chunkLen)
            {
                if (i + chunkLen > stringy.Length) chunkLen = stringy.Length - i;
                foreach (char c in stringy.Substring(i, chunkLen))
                {
                    Avgs += Convert.ToInt32(c);


                }
                Avgs = Avgs / chunkLen;
                //System.Console.WriteLine("Chunk " + ((i/(stringy.Length/numChunk)) +1) + "'s average is " + Avgs);

                avgList.Add(Avgs);
                totalAvgs = totalAvgs + Avgs;

                Avgs = 0;
            }
            totalAvgs = totalAvgs / numChunk;

            //System.Console.WriteLine( "The overall average is " + totalAvgs);

            foreach (float num in avgList)
            {
                if (num > totalAvgs)
                { preMatrix.Add(1); }
                else
                { preMatrix.Add(0); }
            }

            //preMatrix.ForEach(Console.WriteLine);
            var myArray = preMatrix.ToArray();
            string strBinary = "";

            foreach (var item in myArray)
            {
                strBinary += item.ToString();

            }

            System.Console.WriteLine("The value of the binary is " + strBinary);

            StringBuilder result = new StringBuilder(strBinary.Length / 8 + 1);
            for (int i = 0; i < strBinary.Length; i += 8)
            {
                string eightBits = strBinary.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            System.Console.WriteLine("The converted hex value is " + result.ToString());
            return result.ToString().ToUpper();
        }

    }
}
