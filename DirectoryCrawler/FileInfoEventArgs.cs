using System;

namespace DuplicateFinder
{
    public class FileInfoEventArgs : EventArgs
    {
        int
            m_Progress = 0,
            m_HoleProgress = 0;

        string m_CurrentFile = string.Empty;

        string[] m_Items;

        long
            m_FileSize = 0,
            m_Position = 0;

        public long FileSize
        {
            get { return m_FileSize; }
        }

        public long Position
        {
            get { return m_Position; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Items
        {
            get
            {
                return m_Items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileInfoEventArgs()
        { }

        /// <summary>
        /// 
        /// </summary>
        public int TotalProgress
        {
            get
            {
                return m_HoleProgress;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentFile
        {
            get
            {
                return m_CurrentFile;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Progress
        {
            get
            {
                return m_Progress;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        public FileInfoEventArgs(string file, int progress)
        {
            m_CurrentFile = file;
            m_Progress = progress;
        }

        public FileInfoEventArgs(string file, int progress, long position, long FileSize)
        {
            m_CurrentFile = file;
            m_Progress = progress;
            m_Position = position;
            m_FileSize = FileSize;
        }

        public FileInfoEventArgs(string[] Items, int TotalProgress)
        {
            m_HoleProgress = TotalProgress;
            m_Items = new string[Items.Length];
            Items.CopyTo(m_Items, 0);
        }

    }
    public delegate void FileInfoEventHandler(object sender, FileInfoEventArgs e);
}
