using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using DuplicateFinder.DirectoryCrawler;

namespace DuplicateFinder
{

    public partial class frmMain : Form
    {
        #region Global Variables

        private DirectoryCrawler.DirectoryCrawler DCSearch;

        private DirectoryCrawler.FileInfoProvider DCFileInfo;

        private FileInfo[] m_fileInfos;

        private Hashtable[] groupTables;

        public static string GlobalString = "3";

        private int
            prg = 0,
            len;

        private List<ListViewItem> filesToDelete;

        private List<string[]> alFiles = new List<string[]>(12);

        private List<string> undeletable = new List<string>();

        private string
            firstFolder,
            m_deleteFolder = @"D:\DuplicateFiles";

        private Thread m_tdDELETE;

        #endregion

        public frmMain()
        {
            InitializeComponent();
        }
        
        #region MyMethods
        private void MoveToRecycleBin(string file)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);

        }

        private void DeleteAllToRecycleBin()
        {
            foreach (ListViewItem it in filesToDelete)
            {
                MoveToRecycleBin(it.SubItems[4].Text + "\\" + it.SubItems[0].Text);

                deleteItem(it);
            }
        }

        private string SpaceThousands(long valu)
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat;

            nfi.NumberGroupSeparator = " ";

            nfi.NumberDecimalDigits = 0;

            return valu.ToString("N", nfi);
        }

        private void SearchFiles()
        {
            long[] limits = ParseMinMaxSizes();
            string hashMethod = ParseHashMethod();

            FileInfoProvider.universalFlag = hashMethod;

            long
                skipLessThan = limits[0],
                skipMoreThan = limits[1];


            string
                sExt = txtExtension.Text,
                sFolder = txtFolder.Text;

            m_fileInfos = null;

            alFiles = new List<string[]>();


            DCSearch = new DirectoryCrawler.DirectoryCrawler(sFolder, sExt);

            firstFolder = DCSearch.PathOne.ToLower();

            DCSearch.DeleteToFolder = m_deleteFolder;

            DCSearch.FolderChaged += new EventHandler(DCSearch_FolderChanged);

            DCSearch.FoldersDone += new EventHandler(DCSearch_FoldersDone);

            setText(this, "Duplicate Finder - Searching in FOLDERS");

            DCSearch.Crawl(skipLessThan, skipMoreThan);
        }

        private long[] ParseMinMaxSizes()
        {
            long
                min = 0,
                max = long.MaxValue;

            int
                multiM = 1;


            if (nmMax.Value != 0)
            {
                if (rdbMUnit.Checked)
                    multiM = 1;

                else if (rdbMKilo.Checked)
                    multiM = 1024;

                else
                    multiM = 1024 * 1024;

                max = (long)nmMax.Value * multiM;
            }

            return new long[] { min, max };
        }

        private string ParseHashMethod()
        {
            if (rdbLKilo3.Checked)
                return "9";

            else if (rdbLKilo2.Checked)
                return "8";

            else if (rdbLKilo.Checked)
                return "7";

            else if (rdbLUnit3.Checked)
                return "6";

            else if (rdbLUnit2.Checked)
                return "5";

            else if (rdbLUnit.Checked)
                return "4";

            else if (rdbLMega2.Checked)
                return "3";

            else if (rdbLMega3.Checked)
                return "2";

            else
                return "1";
        }

        private FileInfo[] SameSize(FileInfo[] filesToCompare)
        {
            int len = filesToCompare.Length;

            List<long> alIdx = new List<long>();

            Hashtable HLengths = new Hashtable();


            foreach (FileInfo fileInfo in filesToCompare)
            {
                if (!HLengths.Contains(1))
                    HLengths.Add(1, 1);

                ///////////////////////////////////////////////
                // Should do this in a less sketchy way buttt
                // that's for later
                // Date of Change: 6.9.2016
                //////////////////////////////////////////////

                else
                    HLengths[1] = (int)HLengths[1] + 1;
            }

            foreach (DictionaryEntry hash in HLengths)
                if ((int)hash.Value == 1)
                {
                    try { alIdx.Add((long)hash.Key); }
                    catch { MessageBox.Show((hash.Key).ToString()); }
                    
                    //alIdx.Add((long)hash.Key);
                    setText(stsMain, string.Format("Will remove File with size {0}", hash.Key));
                }

            FileInfo[] fiZ = new FileInfo[len - alIdx.Count];

            int j = 0;

            for (int i = 0; i < len; i++)
            {
                if (!alIdx.Contains(filesToCompare[i].Length))
                    fiZ[j++] = filesToCompare[i];
            }

            return fiZ;
        }

        private void FindDuplicate()
        {
            DCFileInfo = new DirectoryCrawler.FileInfoProvider(GloboGym.GlobalString);


            DCFileInfo.FileProgressed += new FileInfoEventHandler(DCFileInfo_FileProgressed);


            DCFileInfo.FileDone += new FileInfoEventHandler(DCFileInfo_FileDone);


            DCFileInfo.AllFilesRead += new FileInfoEventHandler(DCFileInfo_AllFilesRead);


            setText(this, "Duplicate Finder - Getting hash for files ");


            DCFileInfo.PopulateInfos(m_fileInfos);
        }

        private void clearNonDuplicates()
        {
            List<string> alIdx = new List<string>();

            int itCnt = 5, len = alFiles.Count;

            System.Collections.Hashtable HHashes = new System.Collections.Hashtable();

            foreach (string[] file in alFiles)
            {
                if (!HHashes.Contains(file[3]))
                    HHashes.Add(file[3], 1);

                else
                    HHashes[file[3]] = (int)HHashes[file[3]] + 1;
            }

            foreach (DictionaryEntry hash in HHashes)
                if ((int)hash.Value == 1)
                {
                    alIdx.Add((string)hash.Key);

                    showStatus(string.Format("Will remove File with MD5 {0}", hash.Key));
                }

            string[,] sITEMS = new string[alFiles.Count, itCnt];

            string[] tmp = new string[itCnt];


            for (int i = 0; i < len; i++)
            {
                tmp = alFiles[i];

                if (!alIdx.Contains(tmp[3]))
                    for (int j = 0; j < itCnt; j++)
                        sITEMS[i, j] = tmp[j];
            }

            alFiles.Clear();

            for (int i = 0; i < sITEMS.GetLength(0); i++)
            {
                if (sITEMS[i, 0] != null)
                {
                    tmp = new string[itCnt];

                    for (int j = 0; j < itCnt; j++)
                        tmp[j] = sITEMS[i, j];

                    alFiles.Add(tmp);
                }
            }
        }

        private void showInformations()
        {
            long[] dups = GetduplicateFilesInfos();

            long dupFiles = dups[0];

            long dupSizes = dups[1];

            showDuplicateInfo(dupFiles, dupSizes);
        }

        private long[] GetduplicateFilesInfos()
        {
            long nSizes = 0;

            int len = filesToDelete.Count;

            long nFiles = len;

            for (int i = 0; i < len; i++)
            {
                nSizes += long.Parse(filesToDelete[i].SubItems[1].Text.Replace(" ", string.Empty));
            }

            return new long[] { nFiles, nSizes };
        }

        private void deleteDuplicateFiles()
        {
            if (filesToDelete.Count == 0)
                return;


            len = filesToDelete.Count;

            prg = 0;

            foreach (ListViewItem it in filesToDelete)
                deleteFile(it);

            WatchFinished();
        }

        private Color Colorize(long size)
        {
            long kilo = 1024;

            long mega = kilo * kilo;

            long hunmeg = 100 * mega;

            long giga = mega * mega;


            Color[] clrs = new System.Drawing.Color[] 
            { 
                Color.LightGreen, 
                Color.White, 
                Color.LightBlue, 
                Color.Red 
            };

            if (size < kilo)
                return clrs[0];

            if (size < mega)
                return clrs[1];

            if (size < hunmeg)
                return clrs[2];

            return clrs[3];
        }

        private string TrunC(string text, int nMX)
        {
            if (text.Length <= nMX)
                return text;

            int nRetain = text.Length - nMX;

            int mid = (int)((float)text.Length / 2f);

            int limin = mid - (nRetain / 2) - 2;

            int limax = mid + (nRetain / 2) + 2;

            if ((limin * limax > 0) && (limax < text.Length))
            {
                string part1 = text.Substring(0, limin) + "...";

                string part2 = text.Substring(limax);

                return string.Concat(part1, part2);
            }
            else
                return text;
        }

        private void deleteFile(ListViewItem itemToDelete)
        {
            string file = itemToDelete.SubItems[4].Text + "\\" + itemToDelete.SubItems[0].Text;

            string s = "Moving : " + itemToDelete.SubItems[4].Text + "\\" + itemToDelete.SubItems[0].Text;

            showStatus(TrunC(s, 100));

            if (!bErase)
            {
                DirectoryCrawler.FileMover fs = new DirectoryCrawler.FileMover(file, m_deleteFolder, false);

                fs.CopyProgressed += new FileMoverEventHandler(fs_MoveProgressed);

                fs.MoveDone += new FileMoverEventHandler(fs_MoveDone);

                fs.DeleteError += new FileMoverEventHandler(fs_DeleteError);

#if DEBUG
                fs.MoveSynchronous();
#else
                fs.Move();
#endif
            }
            else
            {
                DirectoryCrawler.FileMover fs = new DuplicateFinder.DirectoryCrawler.FileMover(file);

                fs.CopyProgressed += new FileMoverEventHandler(fs_MoveProgressed);

                fs.MoveDone += new FileMoverEventHandler(fs_MoveDone);

                fs.DeleteError += new FileMoverEventHandler(fs_DeleteError);

                fs.Delete();
            }
        }

        private void WatchFinished()
        {
            Thread td = new Thread(new ThreadStart(watchFinished));

            td.Start();
        }

        private void watchFinished()
        {
            try
            {
                do
                {
                    Thread.Sleep(50);
                }
                while (lstFiles.CheckedItems.Count > 0); // Cross threading bug on delete
            }
            catch { }
            showProgress(prgHole, 100);

            string ans = "Done";

            if (undeletable.Count != 0)
            {
                string[] items = new string[undeletable.Count];

                undeletable.CopyTo(items, 0);

                frmUndeletable frm = new frmUndeletable(items);

                frm.ShowDialog();
            }
            else
                ans += ", No Items TO Delete";


            MessageBox.Show(ans + " : " + prg.ToString() + " files deleted");
        }

        private void FormatAddFilesToLV()
        {
            
            bool ischkChecked = chkSkipFirst.Checked;

            string
                lastHASH = string.Empty,
                HASH = string.Empty;

            Comparers.FileListComparer flCmp = new Comparers.FileListComparer(firstFolder);

            alFiles.Sort(flCmp.FileNameComparer);

            ListViewItem[] lvCOL = new ListViewItem[alFiles.Count];

            bool
                bIsInFirstFolder,
                bIsOldHash,
                bBold,
                bIsCheck;


            int iLV = 0;

            Font fntBold = new Font("calibri", 10, FontStyle.Bold);

            Font fntNormal = new Font("calibri", 10, FontStyle.Regular);


            foreach (string[] lvFile in alFiles)
            {
                HASH = lvFile[3];

                bIsInFirstFolder = lvFile[4].ToLower().StartsWith(firstFolder) && ischkChecked;

                bIsOldHash = (HASH.CompareTo(lastHASH) == 0);

                bBold = bIsInFirstFolder;

                bIsCheck = !bBold && bIsOldHash;

                lvCOL[iLV] = new ListViewItem(lvFile);

                lvCOL[iLV].Checked = bIsCheck;

                lvCOL[iLV].BackColor = Colorize(long.Parse(lvFile[1].Replace(" ", string.Empty)));

                if (!bBold)
                    lvCOL[iLV].Font = fntNormal;

                else
                {
                    lvCOL[iLV].Font = fntBold;

                    lvCOL[iLV].ForeColor = Color.Red;
                }

                lastHASH = HASH;

                iLV++;
            }

            addListItems(lstFiles, lvCOL);

            lvCOL = null;
        }

        #endregion

        #region DirectoryCrawler.FileInformation Events
        private void DCFileInfo_AllFilesRead(object sender, FileInfoEventArgs e)
        {
            setText(this, "Duplicate Finder - Clearing non Duplicates");

            clearNonDuplicates();

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.BelowNormal;

            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;

            setText(this, "Duplicate Finder - Adding Files to Listview");

            if (alFiles.Count == 0)
            {
                lockControls(false);

                m_fileInfos = new FileInfo[0];

                return;
            }

            FormatAddFilesToLV();

            showProgress(prgFile, 100);

            showProgress(prgHole, 100);

            setText(this, "Duplicate Finder - Organizing Groups");

            groupOrganize();

            

            setWindowState();

            fillDeletableItems();

            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;

            setCursor(Cursors.Default);

            lockControls(false);
        }

        private void DCFileInfo_FileDone(object sender, FileInfoEventArgs e)
        {
            string[] Items = new string[5];

            e.Items.CopyTo(Items, 0);

            alFiles.Add(Items);

            showProgress(prgHole, e.TotalProgress);

            showProgress(prgFile, 100);

            showStatus("Hashed " + Items[0] + " : " + Items[3]);
        }

        private void DCFileInfo_FileProgressed(object sender, FileInfoEventArgs e)
        {
            showProgress(prgFile, e.Progress);

            showStatus(TrunC(e.CurrentFile, 100));
        }

        #endregion

        #region FileMover Events
        int errors = 0;

        private void fs_DeleteError(object sender, FileMoverEventArgs e)
        {
            undeletable.Add(e.FileName);

            errors++;

            prg++;

            showProgress(prgHole, 100 * prg / len);

        }

        private void fs_MoveDone(object sender, FileMoverEventArgs e)
        {
            prg++;

            showProgress(prgHole, 100 * prg / len);

            deleteItembyString(e.FileName);

        }

        private void fs_MoveProgressed(object sender, FileMoverEventArgs e)
        {
            showProgress(prgFile, e.Progress);
        }
        #endregion

        #region DirectoryCrawler Events
        private void DCSearch_FoldersDone(object sender, EventArgs e)
        {
            int cnt = ((DirectoryCrawler.DirectoryCrawler)sender).FilesFound;

            string s = "Found " + cnt.ToString() + " files";

            showStatus(TrunC(s, 100));

            setText(this, "Duplicate Finder - Searching Files");

            m_fileInfos = SameSize(((DirectoryCrawler.DirectoryCrawler)sender).FileInfos);

            setText(lblFolder, "Number of files : " + m_fileInfos.Length);

            setText(this, "Duplicate Finder - Searching duplicates");

            FindDuplicate();
        }

        private void DCSearch_FolderChanged(object sender, EventArgs e)
        {
            string fld = ((DirectoryCrawler.DirectoryCrawler)sender).CurrentFolder;

            int cnt = ((DirectoryCrawler.DirectoryCrawler)sender).FilesFound;

            string s = cnt.ToString() + " @ " + fld;

            showStatus(TrunC(s, 100));
        }
        #endregion

        #region Form delegates

        public delegate void LockControls(bool lockit);

        private void lockControls(bool lockit)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new LockControls(lockControls), new object[] { lockit });

            else
                btnGo.Enabled = btnAdd.Enabled = btnBrowse.Enabled = btnDrop.Enabled =
                    txtExtension.Enabled = chkSkipFirst.Enabled = !lockit;
        }

        public delegate void AddListItems(ListView lv, ListViewItem[] lvItems);

        private void addListItems(ListView lv, ListViewItem[] lvItems)
        {
            if (lv.InvokeRequired)
                lv.BeginInvoke(new AddListItems(addListItems), new object[] { lv, lvItems });

            else
                lv.Items.AddRange(lvItems);
        }

        public delegate void FillDeletableItems();

        private void fillDeletableItems()
        {
            //System.Windows.Forms.MessageBox.Show("Fill");
                if (lstFiles.InvokeRequired)
                lstFiles.BeginInvoke(new FillDeletableItems(fillDeletableItems));

            else
            {
                filesToDelete = new List<ListViewItem>();

                foreach (ListViewItem it in lstFiles.CheckedItems)
                {
                    if (!it.Font.Strikeout)
                        filesToDelete.Add(it);
                }

                showInformations();
            }
        }

        public delegate void GroupOrganize();

        private void groupOrganize()
        {
            if (!lstFiles.InvokeRequired)
            {
                int cols = 5;

                groupTables = new Hashtable[cols];

                for (int column = 0; column < cols; column++)
                {
                    groupTables[column] = CreateGroupsTable(column);
                }
                SetGroups(3);
                for (int i = 0; i < lstFiles.Columns.Count; i++)
                    if (i != 3)
                        lstFiles.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                clnHash.Width = 0;
            }
            else
                lstFiles.BeginInvoke(new GroupOrganize(groupOrganize));
        }

        public delegate void DeleteItem(ListViewItem it);

        private void deleteItem(ListViewItem it)
        {
            if (lstFiles.InvokeRequired)
                lstFiles.BeginInvoke(new DeleteItem(deleteItem), new object[] { it });

            else
            {
                Font fntStr = new Font(it.Font, FontStyle.Strikeout);

                it.Font = fntStr;

                it.Checked = false;
            }
        }

        public delegate void DeleteItemByString(string filename);

        private void deleteItembyString(string filename)
        {
            if (lstFiles.InvokeRequired)
                lstFiles.BeginInvoke(new DeleteItemByString(deleteItembyString), new object[] { filename });

            else
            {
                foreach (ListViewItem item in lstFiles.Items)
                    if (item.SubItems[4].Text + "\\" + item.SubItems[0].Text == filename)
                    {
                        Font fntStr = new Font(item.Font, FontStyle.Strikeout);

                        item.Font = fntStr;

                        item.Checked = false;

                        return;
                    }
            }
        }

        public delegate void SetCursor(Cursor cursor);

        private void setCursor(Cursor cursor)
        {
            if (!this.InvokeRequired)
                this.Cursor = cursor;

            else
                this.BeginInvoke(new SetCursor(setCursor), new object[] { cursor });
        }

        public delegate void SetWindowState();

        private void setWindowState()
        {
            if (!this.InvokeRequired)
                this.WindowState = FormWindowState.Normal;

            else
                this.BeginInvoke(new SetWindowState(setWindowState));
        }

        public delegate void ShowStatus(string status);

        private void showStatus(string status)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            if (stsMain.InvokeRequired)
                stsMain.BeginInvoke(new ShowStatus(showStatus), new object[] { status });

            else
            {
                stsLabel.Text = TrunC(status, 100);
            }
        }

        public delegate void AddRangeToGroup(ListViewGroup[] groupsArray);

        private void addRangeToGroup(ListViewGroup[] groupsArray)
        {
            
            if (!lstFiles.InvokeRequired)
                lstFiles.Groups.AddRange(groupsArray);

            else
                lstFiles.BeginInvoke(new AddRangeToGroup(addRangeToGroup), new object[] { groupsArray });
        }

        public delegate void ClearGroups();

        private void clearGroups()
        {
            if (!lstFiles.InvokeRequired)
                lstFiles.Groups.Clear();

            else
                lstFiles.BeginInvoke(new ClearGroups(clearGroups));
        }

        public delegate void AddListItem(object lvi, bool check);

        private void addListItem(object lvitem, bool check)
        {
            if (lstFiles.InvokeRequired)
                lstFiles.BeginInvoke(new AddListItem(addListItem), new object[] { lvitem, check });

            else
            {
                ListViewItem lv = new ListViewItem((string[])lvitem);

                lv.Checked = check;

                lv.BackColor = Colorize(long.Parse(lv.SubItems[1].Text.Replace(" ", string.Empty)));

                lstFiles.Items.Add(lv);
            }
        }

        public delegate void ShowProgress(ProgressBar progressbar, int prg);

        private void showProgress(ProgressBar progressbar, int prg)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            if (progressbar.InvokeRequired)
                progressbar.BeginInvoke(new ShowProgress(showProgress), new object[] { progressbar, prg });

            else
            {
                if (prg < progressbar.Maximum)
                    progressbar.Value = prg;

                else
                    progressbar.Value = progressbar.Maximum;
            }
        }

        public delegate void ShowDuplicateInfo(long dupFiles, long dupSizes);

        private void showDuplicateInfo(long dupFiles, long dupSizes)
        {
            if (stsMain.InvokeRequired)
                stsMain.BeginInvoke(new ShowDuplicateInfo(showDuplicateInfo), new object[] { dupFiles, dupSizes });

            else
                stsLabel.Text = dupFiles.ToString() + " duplicate files of total size : " +
                    SpaceThousands(dupSizes / 1024) + " Kb";
        }

        public delegate void SetText(Control ctrl, string str);

        private void setText(Control ctrl, string str)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            if (ctrl.InvokeRequired)
                ctrl.BeginInvoke(new SetText(setText), new object[] { ctrl, str });

            else
                ctrl.Text = str;
        }

        #endregion

        #region Form events

        private void lstFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //fillDeletableItems();
        }

        //Sets myListView to groups created for column
        private void SetGroups(int column)
        {
            // remove all current groups... this seems dumb
            clearGroups();

            //Retrieve hash for column
            Hashtable groups = (Hashtable)groupTables[column];

            //Copy groups for column to an array
            ListViewGroup[] groupsArray = new ListViewGroup[groups.Count];

            groups.Values.CopyTo(groupsArray, 0);

            //Sort groups
            Array.Sort(groupsArray, new DuplicateFinder.Comparers.ListViewGroupSorter(SortOrder.Ascending, column));
            
            //Add them to range
            addRangeToGroup(groupsArray);
            
            //Iterate through items in listview
            //Putting each in correct group
            foreach (ListViewItem item in lstFiles.Items)
            {
                // Get Item by column
                string subItemText = item.SubItems[column].Text;

                //For title column, use only first letter
                if (column == 0)
                {
                    subItemText = subItemText.Substring(0, 1); 
                }
                // Assign item to matching group
                //try { item.Group = (ListViewGroup)groups[subItemText];}
                // catch { }
                item.Group = (ListViewGroup)groups[subItemText];
            }
        }

        //Creates hashtable with one entry for each unique hash
        private Hashtable CreateGroupsTable(int column)
        {
            // Create a hashtable object
            Hashtable groups = new Hashtable();
            //System.IO.StreamReader rw = new System.IO.StreamReader("listViewContent2.txt"));
            //string fileContent

            using (System.IO.TextWriter tw = new System.IO.StreamWriter("listViewContent.txt"))
                //Iterate through items in myListView
                foreach (ListViewItem item in lstFiles.Items)
                {
                    //System.Windows.Forms.MessageBox.Show(item.ToString());
                    //Retrieve value for column
                    string subItemText = item.SubItems[column].Text;
                    //tw.WriteLine(item.Text);
                    string temp = "";
                    for (int a = 0; a <= 4; a++) { temp += (item.SubItems[a].Text) + ", "; }
                    tw.WriteLine(temp);
                    //For title, first letter only
                    if (column == 0)
                    {
                        subItemText = subItemText.Substring(0, 1);
                    }

                    if (!groups.Contains(subItemText))
                    {

                        groups.Add(subItemText, new ListViewGroup(subItemText, HorizontalAlignment.Left));

                    }
                }
            return groups;

        }

        private bool bErase = false;

        private void deleteSelectedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setText(this, "Duplicate Finder - moving selected FILES to " + m_deleteFolder);

#if DEBUG
            deleteDuplicateFiles();
#else
            if (rdbRecycleBin.Checked && !chkDelete.Checked)
            {
                DeleteAllToRecycleBin();
                return;
            }

            m_tdDELETE = new Thread(new ThreadStart(deleteDuplicateFiles));

            m_tdDELETE.Start();
#endif
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (fldBrowse.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fldBrowse.SelectedPath;

                btnGo.Enabled = true;

                btnAdd.Visible = true;

                btnDrop.Visible = true;

                btnAdd.Focus();
            }
            else if (txtFolder.Text.Length == 0)
            {
                btnGo.Enabled = false;

                btnAdd.Visible = false;

                btnDrop.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (fldBrowse.ShowDialog() == DialogResult.OK)
                txtFolder.Text += " | " + fldBrowse.SelectedPath;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            lockControls(true);

            lstFiles.Items.Clear();

            alFiles.Clear();

            this.Cursor = Cursors.WaitCursor;

            SearchFiles();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_tdDELETE != null)
                    m_tdDELETE.Abort();
            }
            catch (Exception esd) { }
        }

        private void lstFiles_DoubleClick(object sender, EventArgs e)
        {
            string path = lstFiles.SelectedItems[0].SubItems[4].Text + "\\" + lstFiles.SelectedItems[0].SubItems[0].Text;

            System.Diagnostics.Process.Start(path);
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            if (fldBrowse.ShowDialog() == DialogResult.OK)
                txtFolder.Text += " | -" + fldBrowse.SelectedPath;
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = lstFiles.SelectedItems[0].SubItems[4].Text;

            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void viewWithNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = lstFiles.SelectedItems[0].SubItems[4].Text + "\\" + lstFiles.SelectedItems[0].SubItems[0].Text;

            System.Diagnostics.Process.Start("notepad++.exe", path);
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            bErase = chkDelete.Checked;
            rdbDUP.Enabled = !chkDelete.Checked;
            rdbRecycleBin.Enabled = !chkDelete.Checked;
        }
        #endregion


    }
    public static class GloboGym
    {
        public static String GlobalString = "1";
    }
}
