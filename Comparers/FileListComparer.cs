using System;
using System.Collections;

namespace DuplicateFinder.Comparers
{
    public class FileListComparer : IComparer
    {
        private string m_FirstFolder = string.Empty;
        
        public int Compare(object x, object y)
        {
            if ((x == null) && (y == null))
            {
                return 0;
            }
            else
            {
                long xLength = ((System.IO.FileInfo)x).Length;
                long yLength = ((System.IO.FileInfo)y).Length;

                int retval = ((System.IO.FileInfo)x).Length.CompareTo(((System.IO.FileInfo)y).Length);
                return retval;
 
            }
        }

        public FileListComparer(string firstFolder)
        {
            m_FirstFolder = firstFolder;
        }

        public int FileNameComparer(string[] fileInfoA, string[] fileInfoB)
        {
            if (fileInfoA[4] + "\\" + fileInfoA[0] == fileInfoB[4] + "\\" + fileInfoB[0])
                return 0;

            string fiA = fileInfoA[3] + fileInfoA[4].ToLower();
            string fiB = fileInfoB[3] + fileInfoB[4].ToLower();

            if (fileInfoA[3] != fileInfoB[3])
                return fileInfoA[3].CompareTo(fileInfoB[3]);

            if (fileInfoA[4].ToLower().Contains(m_FirstFolder))
                return -1;
            if (fileInfoB[4].ToLower().Contains(m_FirstFolder))
                return 1;

            return fiA[3].CompareTo(fiB[3]);
        }

        public static int FileSizeComparer(System.IO.FileInfo fileA, System.IO.FileInfo fileB)
        {
            return fileA.Length.CompareTo(fileB.Length);
            
        }

    }
}
