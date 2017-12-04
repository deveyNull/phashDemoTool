using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = "C:\\Users\\dennis.devey\\Desktop\\y\\Hydrangeas.jpg";
            //Console.WriteLine("The path is " + path + "\n");

            //string output = AverageHash64Init(path);
            //Console.WriteLine("The binary is " + output + "\n");

            //string path2 = "C:\\Users\\dennis.devey\\Desktop\\y\\Hydrangeas2.jpg";
            //Console.WriteLine("The path is " + path2 + "\n");

            //string output2 = AverageHash64Init(path2);
            //Console.WriteLine("The binary is " + output2 + "\n");

            string one = "0009020b0123061e20370b54650800bd1908014a011a08030214160302642e2309ac0874513d05c2aa0200e33e16021906180311087a113a09ca284e31593e36ce0f3740ff0a053a07300a0f12bc304a2468436e1c4a7c23a22a5f35ff1e332312180f0927966e1c22dcef28534a823bb41a732ab02a4d3f130702113a0f1724053e65072446b50f5d23801849190e61";
            Console.WriteLine(HashReduce2(one, 64));
            Console.WriteLine(HashReduce2(one, 16));
            string one2 = "0009020b0126062023390b5a6c0600bd19090146021b080301161603016d2f270ab40a80593904cdaf0300dd2d180313061d04120789133e09c52d4934503f33d3103442d70a053005360c1210cf38562d5d416d1d3e7c23a7366134ff1e2d230f1b120920a4791a24b8ee2a5741853dbf14742a9b2a443c140502123a101921073c71072a4fb9115f2c751b481c0b69";
            Console.WriteLine(HashReduce2(one2, 64));
            Console.WriteLine(HashReduce2(one2, 16));
            string one3 = "0009020b0124051f1f370b54640700bb1a070147011b08020014140301622a2309ae0975523b05c2ac0200e33f16021a04170310057b113709cc284f31583d37cf0f3642ff0a053b07310a0f11be3047236a446a1d4c7d23a32a5f36ff1e312412180f0925966d1b22dbec25544b843bb419752ab0294c3c130802113a0f1624053e64072446b70f5c24811848190e60";
            Console.WriteLine(HashReduce2(one3, 64));
            Console.WriteLine(HashReduce2(one3, 16));
            string one4 = "0009020b0123061e20370b54650800bd1908014a011a08030214160302642e2309ac0874513d05c2aa0200e33e16021906180311087a113a09ca284e31593e36ce0f3740ff0a053a07300a0f12bc304a2468436e1c4a7c23a22a5f35ff1e332312180f0927966e1c22dcef28534a823bb41a732ab02a4d3f130702113a0f1724053e65072446b50f5d23801849190e61";
            Console.WriteLine(HashReduce2(one4, 64));
            Console.WriteLine(HashReduce2(one4, 16));
            string one5 = "0009020b0123061e1f370b54640800bc1908014a011a08020214160302632d2309ad0874513d05c3aa0200e33e16021906180311087a113a09cb284e31593e36ce0f3740ff0a053a07300a0f12bc2f492469436e1c4b7c23a22a5f35ff1e322311180e0926976e1c23dcef28534a823bb419742aaf2a4c3e130702113a0f1723053e66072446b50f5d22801848190e61";
            Console.WriteLine(HashReduce2(one5, 64));
            Console.WriteLine(HashReduce2(one5, 16));
            string one6 = "0009020b002506201f3a095c670700c71e080151021a080201151603016b2f2408b40876523e05c1b40200de3e170218061b03120883113e09c42a4931544333d00f3a41f80a063605340b0f10c8344d2863416e1c457821a32c5d33ff1f3022101810092298711922ccf22559458839b8137627a328453d130702113a0e171f043865042147b10d58277818431c0b62"; 
            Console.WriteLine(HashReduce2(one6, 64));
            Console.WriteLine(HashReduce2(one6, 16));




            Console.ReadLine();


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
        public static string AverageHash64Init(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return AverageHash64(bmp);
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

        public static string GradientHash64Init(String path)
        {
            Bitmap bmp = new Bitmap(path);
            //Bitmap bmp = (Bitmap)Image.FromFile(path, true);
            return GradientHash64and4(bmp);
        }
        public static string GradientHash64and4(Image image)
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
            // There are better ways to do this, but it definitely is not hashReduce()
            // I'll just have to write out numpy.resize() in C#

            Bitmap squeezed2 = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
            Graphics canvas2 = Graphics.FromImage(squeezed2);
            canvas2.CompositingQuality = CompositingQuality.HighQuality;
            canvas2.InterpolationMode = InterpolationMode.HighQualityBilinear;
            canvas2.SmoothingMode = SmoothingMode.HighQuality;

            canvas2.DrawImage(image, 0, 0, 4, 4);

            byte[] grayscale2 = new byte[16];
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    uint pixel2 = (uint)squeezed2.GetPixel(x, y).ToArgb();
                    uint gray2 = (pixel2 & 0x00ff0000) >> 16;
                    gray2 += (pixel2 & 0x0000ff00) >> 8;
                    gray2 += (pixel2 & 0x000000ff);
                    gray2 /= 12;

                    grayscale2[x + (y * 4)] = (byte)gray2;
                }
            return (matrixToHashGradient2(grayscale, grayscale2));
        }
        public static string matrixToHashGradient2(byte[] grayscale, byte[] grayscale2)
        {

            string hash = "";
            for (int i = 0; i < 255; i++)
                if (grayscale[i] >= grayscale[i + 1])
                { hash += "1"; }
                else { hash += "0"; }
            hash += 0;

            string hash2 = "";
            for (int i = 0; i < 15; i++)
                if (grayscale2[i] >= grayscale2[i + 1])
                { hash2 += "1"; }
                else { hash2 += "0"; }
            hash2 += 0;
            //string hash2 = HashReduce(hash, 16);

            //System.Console.WriteLine("The long value of the binary is " + hash + "\n");
            //System.Console.WriteLine("The reduced value of the binary is " + hash2 + "\n");

            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            StringBuilder result2 = new StringBuilder(hash2.Length / 8 + 1);
            for (int i = 0; i < hash2.Length; i += 8)
            {
                string eightBits = hash2.Substring(i, 8);

                result2.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            string both = Convert.ToString(result2) + ", " + Convert.ToString(result);
            return both;
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
        public static string matrixToHashGradient(byte[] grayscale)
        {

            string hash = "";
            for (int i = 0; i < 255; i++)
                if (grayscale[i] >= grayscale[i + 1])
                { hash += "1"; }
                else { hash += "0"; }
            hash += 0;

            string hash2 = HashReduce(hash, 16);

            System.Console.WriteLine("The long value of the binary is " + hash + "\n");
            System.Console.WriteLine("The reduced value of the binary is " + hash2 + "\n");

            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            StringBuilder result2 = new StringBuilder(hash2.Length / 8 + 1);
            for (int i = 0; i < hash2.Length; i += 8)
            {
                string eightBits = hash2.Substring(i, 8);

                result2.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            string both = Convert.ToString(result2) + ", " + Convert.ToString(result);
            return both;
        }
        public static string HashReduce2(string stringy, int numberChunks)
        {
            int numChunk = numberChunks;

            float Avgs = 0;
            float totalAvgs = 0;

            List<float> avgList = new List<float>();

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
            string strBinary = "";
            foreach (float num in avgList)
            {
                if (num > totalAvgs)
                { strBinary += "1"; }
                else
                { strBinary += "0"; }
            }
            string hash2 = strBinary;

            StringBuilder result2 = new StringBuilder(hash2.Length / 8 + 1);
            for (int i = 0; i < hash2.Length; i += 8)
            {
                string eightBits = hash2.Substring(i, 8);

                result2.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }
            return Convert.ToString(result2);
        }
        public static string HashReduce(string stringy, int numberChunks)
        {
            int numChunk = numberChunks;

            float Avgs = 0;
            float totalAvgs = 0;

            List<float> avgList = new List<float>();

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
            string strBinary = "";
            foreach (float num in avgList)
            {
                if (num > totalAvgs)
                { strBinary += "1"; }
                else
                { strBinary += "0"; }
            }
            return strBinary;
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

            return (matrixToHashAverage2(grayscale, averageValue));
        }
        public static string matrixToHashAverage(byte[] grayscale, uint averageValue)
        {

            string hash = "";
            for (int i = 0; i < 63; i++)
                if (grayscale[i] >= averageValue)
                { hash += "1"; }
                else { hash += "0"; }
            hash += 0;

            
            //System.Console.WriteLine("The long value of the binary is " + hash + "\n");
            //System.Console.WriteLine("The reduced value of the binary is " + hash2 + "\n");

            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }
 
            return Convert.ToString(result);
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

            System.Windows.Forms.MessageBox.Show(Encoding.Default.GetString(grayscale));
            return (matrixToHashAverage(grayscale, averageValue));

        }
        public static string matrixToHashAverage2(byte[] grayscale, uint averageValue)
        {

            string hash = "";
            for (int i = 0; i < 255; i++)
                if (grayscale[i] >= averageValue)
                { hash += "1"; }
                else { hash += "0"; }
            hash += 0;

            string hash2 = HashReduce(hash, 16);

            //System.Console.WriteLine("The long value of the binary is " + hash + "\n");
            //System.Console.WriteLine("The reduced value of the binary is " + hash2 + "\n");

            StringBuilder result = new StringBuilder(hash.Length / 8 + 1);
            for (int i = 0; i < hash.Length; i += 8)
            {
                string eightBits = hash.Substring(i, 8);

                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            StringBuilder result2 = new StringBuilder(hash2.Length / 8 + 1);
            for (int i = 0; i < hash2.Length; i += 8)
            {
                string eightBits = hash2.Substring(i, 8);

                result2.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            string both = Convert.ToString(result2) + ", " + Convert.ToString(result);
            return hash;
        }
    }
}
