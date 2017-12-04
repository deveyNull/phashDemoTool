using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFinder
{
    public class FileMoverEventArgs : EventArgs
    {
        private string m_FileName = string.Empty;
        
        private int m_Progress = 0;

        public int Progress
        {
            get { return m_Progress; }
        }

        public string FileName
        {
            get { return m_FileName; }
        }

        public FileMoverEventArgs(string filename,int ProgressPercentage)
        {
            m_FileName = filename;
            m_Progress = ProgressPercentage;
        }
    }

    public delegate void FileMoverEventHandler(object sender, FileMoverEventArgs e);
}
