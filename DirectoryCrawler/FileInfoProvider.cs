using System;
using System.IO;
using System.Threading;
using System.Text;
using System.Collections.Generic;




namespace DuplicateFinder.DirectoryCrawler
{
    public class FileInfoProvider
    {
        public static string universalFlag;

        public FileInfoProvider(string hash)
        {
            string HashFunction = hash;
        }
        public event FileInfoEventHandler FileDone, AllFilesRead, FileProgressed;

        private Thread m_tdInfo;
        
        private int
            m_HoleProgress = 0, 
            m_FileProgress = 0;
        
        private string m_CurrentFile;
        
        private string[] m_Item;

        public int HoleProgress
        {
            get
            {
                return m_HoleProgress;
            }
        }

        public string[] Item
        {
            get
            {
                return m_Item;
            }
        }

        public string CurrentFile
        {
            get
            {
                return m_CurrentFile;
            }
        }

        public int FileProgress
        {
            get
            {
                return m_FileProgress;
            }
        }

        public static string HashFunction { get; private set; }

        public void PopulateInfos(FileInfo[] files)
        {
#if DEBUG
            populateInfos(files);
#else
            
            m_tdInfo = new Thread(new ParameterizedThreadStart(populateInfos));
            m_tdInfo.IsBackground = true;
            m_tdInfo.Priority = ThreadPriority.BelowNormal;
            m_tdInfo.Start(files);
#endif
        }


        private void populateInfos(object tmpfiles)
        {
            string file = string.Empty;
            

            System.IO.FileInfo fileInfo;
            m_Item = new string[5];
            string hashedValue = "";

            int len = ((FileInfo[])tmpfiles).Length;

            Hashing.PerceptualHashing.HashFinished += new DuplicateFinder.Hashing.HashEventHandler(MD5_HashFinished);
            Hashing.PerceptualHashing.HashProgressed += new DuplicateFinder.Hashing.HashEventHandler(MD5_HashProgressed);
            for (int i = 0; i < len; i++)
            {
                try
                {

                    m_HoleProgress = (i + 1) * 100 / len;
                    fileInfo = ((FileInfo[])tmpfiles)[i];
                    m_CurrentFile = fileInfo.FullName;
                    file = fileInfo.FullName;

                    if (universalFlag == "1")
                    {
                        int chunk = 7 * 1024 * 1024;
                        if (fileInfo.Length / 50 > chunk)
                            chunk = (int)(fileInfo.Length / 50);
                        hashedValue = Hashing.MD5.MD5HashFile(file, chunk);
                    }
                    else if (universalFlag == "2")
                    {
                        hashedValue = Hashing.PerceptualHashing.PhbstHashFile64(file);
                    }
                    else if (universalFlag == "3")
                    {
                        hashedValue = Hashing.PerceptualHashing.PhbstHashFile16(file);
                    }


                    else if (universalFlag == "9")
                    {
                        hashedValue = Hashing.PerceptualHashing.GradientHash64(file);
                    }
                    else if (universalFlag == "8")
                    {
                        hashedValue = Hashing.PerceptualHashing.GradientHash16(file);
                    }

                    else if (universalFlag == "7")
                    {
                        hashedValue = Hashing.PerceptualHashing.GradientHash4(file);
                    }
                    else if (universalFlag == "6")
                    {
                        hashedValue = Hashing.PerceptualHashing.AverageHash64(file);
                    }
                    else if (universalFlag == "5")
                    {
                        hashedValue = Hashing.PerceptualHashing.AverageHash16(file);
                    }
                    else if (universalFlag == "4")
                    {
                        hashedValue = Hashing.PerceptualHashing.AverageHash4(file);
                    }

                    m_Item[0] = fileInfo.Name;
                    m_Item[1] = SpaceThousands(fileInfo.Length);
                    m_Item[2] = fileInfo.Extension.Replace(".", string.Empty).ToUpper() + " File";
                    m_Item[3] = hashedValue;
                    m_Item[4] = fileInfo.DirectoryName;
                    OnFileDone(new FileInfoEventArgs(m_Item, m_HoleProgress));

                }

                catch 
                {

                    
                }
            }
            OnAllFilesDone(new FileInfoEventArgs());
        }

        private string SpaceThousands(long size)
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalDigits = 0;
            return size.ToString("N", nfi);
        }

        private void MD5_HashProgressed(object sender, Hashing.HashEventArgs e)
        {
            m_FileProgress = e.Progress;
            OnFileProgressed(new FileInfoEventArgs(e.Filename, e.Progress, e.Position, e.FileSize));
        }

        private void MD5_HashFinished(object sender, Hashing.HashEventArgs e)
        {
            OnFileDone(new FileInfoEventArgs(e.Filename, e.Progress));
        }

        protected virtual void OnFileProgressed(FileInfoEventArgs e)
        {
            if (FileProgressed != null)
                FileProgressed(this, e);
        }

        protected virtual void OnFileDone(FileInfoEventArgs e)
        {
            if (FileDone != null)
                FileDone(this, e);
        }

        protected virtual void OnAllFilesDone(FileInfoEventArgs e)
        {
            if (AllFilesRead != null)
                AllFilesRead(this, e);
        }

    }
}