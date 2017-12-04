
using System;
using System.Text;
using System.IO;
using System.Threading;

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DuplicateFinder.Hashing
{
    /// <summary>
    /// MD5 hashing for files and strings with events
    /// </summary>
    public class PerceptualHashing
    {
        /// <summary>
        /// Events triggered while hashing
        /// </summary>
        public static event HashEventHandler
            HashProgressed,
            HashFinished;

        /// <summary>
        /// returns HASH string of FileName no events are triggered
        /// </summary>
        /// <param name="FileName">Filename to Hash</param>
        public static string GradientHash64(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //System.Windows.Forms.MessageBox.Show(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return GradientHash64(bmp);
        }
        public static string GradientHash16(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return GradientHash16(bmp);
        }
        public static string GradientHash4(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return GradientHash4(bmp);
        }

        public static string GradientHashX(Image image, int sideLength)
        {
            int squared = sideLength * sideLength;
            uint uSquared = (uint)(int)squared;
            // Squeeze the image into an 16x16 canvas
            Bitmap squeezed = new Bitmap(sideLength, sideLength, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, sideLength, sideLength);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[squared];
            uint averageValue = 0;
            for (int y = 0; y < sideLength; y++)
                for (int x = 0; x < sideLength; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * sideLength)] = (byte)gray;
                    averageValue += gray;
                }
            averageValue /= uSquared;

            //return (matrixToHashGradient(grayscale));

            //Some shenanigans are necessary to get hashes of arbitrary lengths. 
            // You must pad, or slice, until you reach the nearest multiple of 4. Which makes sense.

            return Convert.ToString(grayscale.Length);
        }
        public static string GradientHash64(Image image)
        {
            // Squeeze the image into an 16x16 canvas
            Bitmap squeezed = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 16, 16);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[256];
            for (int y = 0; y < 16; y++)
                for (int x = 0; x < 16; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 16)] = (byte)gray;
                }

            return (matrixToHashGradient(grayscale));
        }
        public static string GradientHash16(Image image)
        {
            // Squeeze the image into an 8x8 canvas
            Bitmap squeezed = new Bitmap(8, 8, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 8, 8);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[64];
            
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 8)] = (byte)gray;

                }


            // Compute the hash: each bit is a pixel
            // 1 = higher than average, 0 = lower than average

            return(matrixToHashGradient(grayscale));
            
        }
        public static string GradientHash4(Image image)
        {
            // Squeeze the image into an 8x8 canvas
            Bitmap squeezed = new Bitmap(4, 4, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 4, 4);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[16];

            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 4)] = (byte)gray;

                }


            // Compute the hash: each bit is a pixel
            // 1 = higher than average, 0 = lower than average

            return (matrixToHashGradient(grayscale));

        }
        public static string matrixToHashGradient(byte[] grayscale)
        {

            string hash = "";
            for (int i = 0; i < grayscale.Length - 1; i++)
                if (grayscale[i] >= grayscale[i + 1])
                { hash += "1"; }
                else { hash += "0"; }
            hash += 0;

            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }


            return Convert.ToString(result);
        }

        public static string GradientHash36(Image image)
        {
            // Squeeze the image into an 6x6 canvas
            Bitmap squeezed = new Bitmap(12, 12, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 12, 12);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[144];
            uint averageValue = 0;
            for (int y = 0; y < 12; y++)
                for (int x = 0; x < 12; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 12)] = (byte)gray;
                    averageValue += gray;
                }
            averageValue /= 144;

            return (matrixToHashAverage(grayscale, averageValue));
        }

        public static string AverageHash64(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return AverageHash64(bmp);
        }
        public static string AverageHash16(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return AverageHash16(bmp);
        }
        public static string AverageHash4(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return AverageHash4(bmp);
        }

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
            averageValue /= 256;

            return (matrixToHashAverage(grayscale, averageValue));
        }
        
        public static string AverageHash16(Image image)
        {
            // Squeeze the image into an 8x8 canvas
            Bitmap squeezed = new Bitmap(8, 8, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 8, 8);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[64];
            uint averageValue = 0;
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 8)] = (byte)gray;
                    averageValue += gray;
                }
            averageValue /= 64;
            //System.Windows.Forms.MessageBox.Show(Encoding.Default.GetString(grayscale));
            return (matrixToHashAverage(grayscale, averageValue));

        }
        public static string AverageHash4(Image image)
        {
            // Squeeze the image into an 8x8 canvas
            Bitmap squeezed = new Bitmap(4, 4, PixelFormat.Format32bppRgb);
            Graphics canvas = Graphics.FromImage(squeezed);
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.DrawImage(image, 0, 0, 4, 4);

            // Reduce colors to 6-bit grayscale and calculate average color value
            byte[] grayscale = new byte[16];
            uint averageValue = 0;
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                    uint gray = (pixel & 0x00ff0000) >> 16;
                    gray += (pixel & 0x0000ff00) >> 8;
                    gray += (pixel & 0x000000ff);
                    gray /= 12;

                    grayscale[x + (y * 4)] = (byte)gray;
                    averageValue += gray;
                }
            averageValue /= 16;

            return (matrixToHashAverage(grayscale, averageValue));

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
        public static string PhbstHashFile16(string FileName)
        {
            string HashString;

            string stream;

            using (StreamReader streamReader = new StreamReader(FileName, Encoding.UTF8))
            {
                stream = streamReader.ReadToEnd();
            }

            HashString = phbstHash16(stream);
            //System.Windows.Forms.MessageBox.Show(HashString);
            return HashString;
        }
        public static string PhbstHashFile64(string FileName)
        {
            string HashString;

            string stream;

            using (StreamReader streamReader = new StreamReader(FileName, Encoding.UTF8))
            {
                stream = streamReader.ReadToEnd();
            }

            HashString = phbstHash64(stream);

            return HashString;
        }
        public static string phbstHash16(string stringy)
        {
            int numChunk = 64; //8x8 matrix => 64 bits => 16 byte hash => 16^16 Possible =/= Pseudo-unique

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

            string hash = "";

            for (int i = 0; i < avgList.Count; i++)
                if (avgList[i] >= totalAvgs)
                { hash += "1"; }
                else { hash += "0"; }


            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }


            return result.ToString().ToUpper();
        }
        public static string phbstHash64(string stringy)
        {
            int numChunk = 256; //8x8 matrix => 64 bits => 16 byte hash => 16^16 Possible =/= Pseudo-unique

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

            string hash = "";

            for (int i = 0; i < avgList.Count; i++)
                if (avgList[i] >= totalAvgs)
                { hash += "1"; }
                else { hash += "0"; }


            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }
           

            return result.ToString().ToUpper();
        }

        public static string HashReduce(string stringy, int numberChunks)
        {
            int numChunk = numberChunks; 

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

        
        /// <summary>
        /// Fired if progressed
        /// </summary>
        /// <param name="hashEventArgs"></param>
        public static void OnHashProgressed(HashEventArgs hashEventArgs)
        {
            if (HashProgressed != null)
                HashProgressed(null, hashEventArgs);
        }

        /// <summary>
        /// Fired when finished
        /// </summary>
        /// <param name="hashEventArgs"></param>
        public static void OnHashCompleted(HashEventArgs hashEventArgs)
        {
            if (HashFinished != null)
                HashFinished(null, hashEventArgs);
        }
    }
}