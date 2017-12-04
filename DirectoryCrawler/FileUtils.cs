using System;
using System.IO;

namespace DuplicateFinder.DirectoryCrawler
{
    public class FileUtils
    {
        private string m_Path = string.Empty;

        public FileUtils(string Path)
        {
            m_Path = Path;
            if (!Directory.Exists(m_Path))
                Directory.CreateDirectory(m_Path);
        }

        public void MoveFile(string FileName)
        {
            FileInfo file = new FileInfo(FileName);
            string Dest = m_Path + "\\" + file.Name;
            int i = 0;
            while (File.Exists(Dest))
                Dest = m_Path + "\\" + Path.GetFileNameWithoutExtension(FileName) +
                    i++.ToString() + Path.GetExtension(FileName);
            file.MoveTo(Dest);
        }
    }
}
