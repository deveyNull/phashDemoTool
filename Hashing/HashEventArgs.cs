using System;

namespace DuplicateFinder.Hashing
{
    /// <summary>
    /// 
    /// </summary>
    public class HashEventArgs : EventArgs
    {
        private int m_Progress = 0;

        private long
            m_Position = 0,
            m_FileSize = 0;

        private string m_Filename = string.Empty;

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
        public string Filename
        {
            get
            {
                return m_Filename;
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
        /// <param name="Filename"></param>
        /// <param name="progress"></param>
        public HashEventArgs(string Filename, int progress, long Position, long fileSize)
        {
            m_Progress = progress;
            m_Filename = Filename;
            m_FileSize = fileSize;
            m_Position = Position;
        }
    }

    /// <summary>
    /// ...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void HashEventHandler(object sender, HashEventArgs e);
}
