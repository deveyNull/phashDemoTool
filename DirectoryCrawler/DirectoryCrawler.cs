using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DuplicateFinder.DirectoryCrawler
{
    public class DirectoryCrawler
    {
        private FileInfo[] fiZ;
        private List<FileInfo> alFiles = new List<FileInfo>(10);

        private List<string>
            m_Path,
            m_Pattern,
            m_SkipPath,
            m_SkipPattern;

        private long 
            skipLessThan, 
            skipMoreThan;

        private System.Threading.Thread tdCrawl;

        private int m_FilesFound = 0;

        private string
            m_CurrentFolder = string.Empty,
            m_DeleteToFolder = string.Empty;

        private string[] m_separators = new string[] { "|", ";" };

        public event EventHandler
            FolderChaged,
            FoldersDone;

        public string PathOne
        {
            get
            {
                return m_Path[0];
            }
        }

        public string DeleteToFolder
        {
            get { return m_DeleteToFolder; }
            set { m_DeleteToFolder = value; }
        }

        public FileInfo[] FileInfos
        {
            get
            {
                return fiZ;
            }
        }

        public int FilesFound
        {
            get
            {
                return m_FilesFound;
            }
        }

        public string CurrentFolder
        {
            get
            {
                return m_CurrentFolder;
            }
        }

        public DirectoryCrawler(string path, string pattern)
        {
            ParsePath(path);
            ParsePattern(pattern);
        }

        public DirectoryCrawler()
        { }

        private void ParsePattern(string pattern)
        {
            m_SkipPattern = new List<string>();
            m_Pattern = new List<string>();
            string[] tmpPattern = pattern.Split(m_separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string minipattern in tmpPattern)
                if (minipattern.Trim().StartsWith("-"))
                    m_SkipPattern.Add(minipattern.Trim().Substring(1));
                else
                    m_Pattern.Add(minipattern.Trim());

            foreach (string forbiddenPattern in m_SkipPattern)
                if (m_Pattern.Contains(forbiddenPattern))
                    m_Pattern.Remove(forbiddenPattern);
        }

        private void ParsePath(string path)
        {
            m_Path = new List<string>();
            m_SkipPath = new List<string>();

            string[] tmpPATH = path.Split(m_separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string minipath in tmpPATH)
                if (minipath.Trim().StartsWith("-"))
                    m_SkipPath.Add(minipath.Trim().Substring(1));
                else
                    m_Path.Add(minipath.Trim());

            foreach (string forbiddenPath in m_SkipPath)
                if (m_Path.Contains(forbiddenPath))
                    m_Path.Remove(forbiddenPath);
        }

        public void Crawl(long SkipLessThan, long SkipMoreThan)
        {
            skipLessThan = SkipLessThan;
            skipMoreThan = SkipMoreThan;
            if (string.IsNullOrEmpty(m_DeleteToFolder))
                throw new ArgumentNullException("DeleteToFolder");

#if DEBUG
            doCrawl();
#else
            tdCrawl = new System.Threading.Thread(new System.Threading.ThreadStart(doCrawl));
            tdCrawl.IsBackground = true;
            tdCrawl.Start();
#endif
        }

        private void doCrawl()
        {
            DirectoryInfo df;
            foreach (string path in m_Path)
            {
                df = new DirectoryInfo(path);
                BrowseFolders(df);
            }

            //alFiles.Sort(Comparers.FileListComparer.FileSizeComparer);
            fiZ = new FileInfo[alFiles.Count];
            alFiles.CopyTo(fiZ);
            OnFoldersDone(EventArgs.Empty);
        }

        private void ScanFolder(DirectoryInfo Folder)
        {
            try
            {
                List<FileInfo> fileZ = new List<FileInfo>();
                List<FileInfo> nofileZ = new List<FileInfo>();

                foreach (string pattern in m_Pattern)
                    fileZ.AddRange(Folder.GetFiles(pattern, SearchOption.TopDirectoryOnly));

                foreach (string nopattern in m_SkipPattern)
                    nofileZ.AddRange(Folder.GetFiles(nopattern, SearchOption.TopDirectoryOnly));

                if (nofileZ.Count != 0)
                {
                    List<int> toRemove = new List<int>();
                    for (int i = 0; i < fileZ.Count; i++)
                        for (int j = 0; j < nofileZ.Count; j++)
                        {
                            if (nofileZ[j].Name == fileZ[i].Name)
                            {
                                toRemove.Add(i);
                                break;
                            }
                        }
                    foreach (int index in toRemove)
                        fileZ.RemoveAt(index);
                }
                foreach (FileInfo fi in fileZ)
                    if (fi.Length > skipLessThan && fi.Length < skipMoreThan)
                    {
                        alFiles.Add(fi);
                        m_FilesFound = alFiles.Count;
                    }

            }
            catch (Exception IOExec) { }

        }

        private void BrowseFolders(DirectoryInfo Folder)
        {
            if (Folder.FullName != m_DeleteToFolder)
            {
                ScanFolder(Folder);
                try
                {
                    DirectoryInfo[] diZ = Folder.GetDirectories();
                    if (diZ != null)
                    {
                        foreach (DirectoryInfo di in diZ)
                            if (!m_SkipPath.Contains(di.FullName))
                            {
                                BrowseFolders(di);
                                m_CurrentFolder = di.FullName;
                                OnFolderChanged(EventArgs.Empty);
                            }
                    }
                }
                catch (Exception IOExec) { }
            }
        }

        protected virtual void OnFolderChanged(EventArgs e)
        {
            if (FolderChaged != null)
                FolderChaged(this, e);
        }

        protected virtual void OnFoldersDone(EventArgs e)
        {
            if (FoldersDone != null)
                FoldersDone(this, e);
        }
    }
}