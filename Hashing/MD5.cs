//  MD5 events enabled Calculation on Stream objects for .Net 
//  Released to the public domain with the NONE of the following 
//  NULL restrictions:(BE AWARE THAT NONE OF THESE APPLY!)
//  
//  1.  You may NOT NOT modify the namespace.
//  2.  Your software must or NOT include acknowledgement 
//      that portions of the code are provided
//      by m.feriati@gmail.com.
//
//  CopyLEFT ® 2008 m.feriati@gmail.com.  NO Rights Reserved.
//  m.feriati@gmail.com for WHATEVER YOU NEED :).
//  YOU make HISTORY!


using System;
using System.Text;
using System.Security.Cryptography;

namespace DuplicateFinder.Hashing
{
    /// <summary>
    /// MD5 hashing for files and strings with events
    /// </summary>
    public class MD5
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
        public static string MD5HashFile(string FileName)
        {
            byte[] result;
            string HashString;

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            System.IO.Stream stream = System.IO.File.OpenRead(FileName);

            result = md5.ComputeHash(stream);
            System.Text.StringBuilder output = new System.Text.StringBuilder(2 + (result.Length * 2));

            foreach (byte b in result)
            {
                output.Append(b.ToString("x2"));
            }
            HashString = output.ToString().ToUpper();
            return HashString;
        }

        /// <summary>
        /// returns HASH string of string : stringToHash  
        /// </summary>
        /// <param name="stringToHash">string to Hash</param>
        public static string MD5HashString(string stringToHash)
        {
            byte[] result;
            string HashString;

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            char[] cctx = stringToHash.ToCharArray();
            byte[] bctx = System.Text.UTF8Encoding.UTF8.GetBytes(cctx);

            result = md5.ComputeHash(bctx);
            System.Text.StringBuilder output = new System.Text.StringBuilder(2 + (result.Length * 2));

            foreach (byte b in result)
            {
                output.Append(b.ToString("x2"));
            }
            HashString = output.ToString().ToUpper();
            return HashString;
        }

        /// <summary>
        /// Hash a chunk of the given file
        /// </summary>
        /// <param name="FileName">the file</param>
        /// <param name="startPosition">from where to start</param>
        /// <param name="endPosition">the end position</param>
        /// <returns></returns>
        public static string MD5HashChunk(string FileName, long startPosition, long endPosition)
        {
            byte[] result, chunk;
            string HashString;
            int len = (int)(endPosition - startPosition + 1);

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            System.IO.FileStream fs = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            fs.Seek(startPosition, System.IO.SeekOrigin.Begin);
            chunk = new byte[len];
            fs.Read(chunk, 0, len);
            fs.Close();
            result = md5.ComputeHash(chunk);

            System.Text.StringBuilder output = new System.Text.StringBuilder(2 + (result.Length * 2));

            foreach (byte b in result)
            {
                output.Append(b.ToString("x2"));
            }
            HashString = output.ToString().ToUpper();
            return HashString;
        }

        /// <summary>
        /// MD5 hashing of the file using blocks, use 7Mb for chunks
        /// </summary>
        /// <param name="FileName">the file</param>
        /// <param name="blocksize">the chunk size</param>
        /// <returns></returns>
        public static string MD5HashFile(string FileName, int blocksize)
        {
            byte[] result;
            byte[] chunk = new byte[blocksize];
            string HashString;

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            try
            {
                System.IO.Stream stream = System.IO.File.OpenRead(FileName);

                long filesize = stream.Length;
                while (filesize - stream.Position > chunk.Length)
                {
                    stream.Read(chunk, 0, blocksize);
                    md5.TransformBlock(chunk, 0, blocksize, chunk, 0);

                    OnHashProgressed(new HashEventArgs(FileName, (int)(100 * stream.Position / filesize), stream.Position, filesize));
                }
                int readCount = stream.Read(chunk, 0, (int)(filesize - stream.Position));
                md5.TransformFinalBlock(chunk, 0, readCount);

                result = md5.Hash;

                System.Text.StringBuilder output = new System.Text.StringBuilder(2 + (result.Length * 2));

                foreach (byte b in result)
                {
                    output.Append(b.ToString("x2"));
                }
                HashString = output.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                chunk = null;
                result = null;
                GC.Collect();
                return "0x0000";
            }
            chunk = null;
            result = null;
            GC.Collect();
            return HashString;
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