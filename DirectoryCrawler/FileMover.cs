using System;
using System.IO;
using System.Threading;

namespace DuplicateFinder.DirectoryCrawler
{
    public class FileMover
    {
        public event FileMoverEventHandler CopyProgressed, MoveDone, DeleteError;

        private long m_Size = 0;
        
        private int m_Progress = 0;

        private string 
            m_srcName, 
            m_destName, 
            m_path;

        private Thread m_tdMove;

        public string Filename
        {
            get { return m_srcName; }
        }

        public FileMover(string SourceFile, string DestinationFile)
        {
            m_srcName = SourceFile;
            m_destName = DestinationFile;
        }

        public FileMover(string sourceFile, string DestinationFolder, bool Overwrite)
        {
            m_srcName = sourceFile;
            m_path = DestinationFolder;
            if (!Directory.Exists(m_path))
                Directory.CreateDirectory(m_path);

            m_destName = m_path + "\\" + Path.GetFileName(m_srcName);
            int i = 0;
            while (File.Exists(m_destName))
                m_destName = m_path + "\\" + Path.GetFileNameWithoutExtension(sourceFile) +
                    i++.ToString() + Path.GetExtension(sourceFile);
        }

        public FileMover(string sourceFile)
        {
            m_srcName = sourceFile;
        }

        public int Progress
        {
            get { return m_Progress; }
        }

        public void Move()
        {
            m_tdMove = new Thread(new ThreadStart(move));
            m_tdMove.Start();
        }

        public void MoveSynchronous()
        {
            move();
        }

        private void move()
        {
            try
            {
                int bufSize = 7 * 1024 * 1024;
                Stream sSrc = new FileStream(m_srcName, FileMode.Open, FileAccess.Read);
                m_Size = sSrc.Length;
                Stream sDst = new FileStream(m_destName, FileMode.Create, FileAccess.Write);

                byte[] buffer = new byte[bufSize];
                int readCount = 0;
                do
                {
                    readCount = sSrc.Read(buffer, 0, bufSize);
                    sDst.Write(buffer, 0, readCount);
                    m_Progress = (int)(sSrc.Position * 100 / m_Size);
                    OnProgressed(new FileMoverEventArgs(m_srcName, m_Progress));
                } while (readCount > 0);

                //bsDst.Close();
                //bsSrc.Close();
                sSrc.Close();
                sDst.Close();

                File.Delete(m_srcName);
            }
            catch (Exception es)
            {
                OnErrorDeleting(new FileMoverEventArgs(m_srcName, 100));
                return;
            }
            OnFinished(new FileMoverEventArgs(m_srcName, 100));
        }

        private void OnErrorDeleting(FileMoverEventArgs eventArgs)
        {
            if (DeleteError != null)
                DeleteError(this, eventArgs);
            GC.Collect();
        }

        protected virtual void OnFinished(FileMoverEventArgs e)
        {
            if (MoveDone != null)
                MoveDone(this, e);
            GC.Collect();
        }

        protected virtual void OnProgressed(FileMoverEventArgs e)
        {
            if (CopyProgressed != null)
                CopyProgressed(this, e);
        }

        public void Delete()
        {
            try
            {
                File.Delete(m_srcName);
                OnFinished(new FileMoverEventArgs(m_srcName, 100));
            }
            catch (Exception es)
            {
                OnErrorDeleting(new FileMoverEventArgs(m_srcName, 100));
                return;
            }

        }
    }

}