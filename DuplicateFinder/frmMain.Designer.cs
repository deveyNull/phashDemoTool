using System.Windows.Forms;

namespace DuplicateFinder
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnBrowse = new System.Windows.Forms.Button();
            this.fldBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.ctxListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWithNotepadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblFolder = new System.Windows.Forms.Label();
            this.prgHole = new System.Windows.Forms.ProgressBar();
            this.prgFile = new System.Windows.Forms.ProgressBar();
            this.grpProgression = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.stsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDrop = new System.Windows.Forms.Button();
            this.chkSkipFirst = new System.Windows.Forms.CheckBox();
            this.clnFileName = new System.Windows.Forms.ColumnHeader();
            this.clnSize = new System.Windows.Forms.ColumnHeader();
            this.clnType = new System.Windows.Forms.ColumnHeader();
            this.clnHash = new System.Windows.Forms.ColumnHeader();
            this.clnFolder = new System.Windows.Forms.ColumnHeader();
            this.lstFiles = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nmMin = new System.Windows.Forms.NumericUpDown();
            this.rdbLMega = new System.Windows.Forms.RadioButton();
            this.rdbLMega2 = new System.Windows.Forms.RadioButton();
            this.rdbLMega3 = new System.Windows.Forms.RadioButton();
            this.rdbLKilo = new System.Windows.Forms.RadioButton();
            this.rdbLKilo2 = new System.Windows.Forms.RadioButton();
            this.rdbLKilo3 = new System.Windows.Forms.RadioButton();
            this.rdbLUnit = new System.Windows.Forms.RadioButton();
            this.rdbLUnit2 = new System.Windows.Forms.RadioButton();
            this.rdbLUnit3 = new System.Windows.Forms.RadioButton();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nmMax = new System.Windows.Forms.NumericUpDown();
            this.rdbMMega = new System.Windows.Forms.RadioButton();
            this.rdbMKilo = new System.Windows.Forms.RadioButton();
            this.rdbMUnit = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbRecycleBin = new System.Windows.Forms.RadioButton();
            this.rdbDUP = new System.Windows.Forms.RadioButton();
            this.ctxListView.SuspendLayout();
            this.grpProgression.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMin)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMax)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(401, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // fldBrowse
            // 
            this.fldBrowse.ShowNewFolderButton = false;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(12, 12);
            this.txtFolder.MaxLength = 256;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(328, 20);
            this.txtFolder.TabIndex = 1;
            // 
            // ctxListView
            // 
            this.ctxListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedFileToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem,
            this.viewWithNotepadToolStripMenuItem});
            this.ctxListView.Name = "ctxListView";
            this.ctxListView.Size = new System.Drawing.Size(188, 92);
            // 
            // deleteSelectedFileToolStripMenuItem
            // 
            this.deleteSelectedFileToolStripMenuItem.Name = "deleteSelectedFileToolStripMenuItem";
            this.deleteSelectedFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.deleteSelectedFileToolStripMenuItem.Text = "Delete Checked Files";
            this.deleteSelectedFileToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedFileToolStripMenuItem_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openContainingFolderToolStripMenuItem.Text = "Open Containing Folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // viewWithNotepadToolStripMenuItem
            // 
            this.viewWithNotepadToolStripMenuItem.Name = "viewWithNotepadToolStripMenuItem";
            this.viewWithNotepadToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.viewWithNotepadToolStripMenuItem.Text = "View with Notepad++";
            this.viewWithNotepadToolStripMenuItem.Click += new System.EventHandler(this.viewWithNotepadToolStripMenuItem_Click);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(432, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(36, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtExtension
            // 
            this.txtExtension.AutoCompleteCustomSource.AddRange(new string[] {
            "*.*",
            "*.doc",
            "*.pdf",
            "*.exe",
            "*.rar;*.zip"});
            this.txtExtension.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtExtension.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtExtension.Location = new System.Drawing.Point(12, 38);
            this.txtExtension.MaxLength = 256;
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(328, 20);
            this.txtExtension.TabIndex = 6;
            this.txtExtension.Text = "*.jpg";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(379, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(16, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.Location = new System.Drawing.Point(25, 61);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(503, 23);
            this.lblFolder.TabIndex = 10;
            // 
            // prgHole
            // 
            this.prgHole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgHole.Location = new System.Drawing.Point(79, 29);
            this.prgHole.Name = "prgHole";
            this.prgHole.Size = new System.Drawing.Size(335, 23);
            this.prgHole.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgHole.TabIndex = 11;
            // 
            // prgFile
            // 
            this.prgFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgFile.Location = new System.Drawing.Point(79, 59);
            this.prgFile.Name = "prgFile";
            this.prgFile.Size = new System.Drawing.Size(335, 23);
            this.prgFile.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgFile.TabIndex = 12;
            // 
            // grpProgression
            // 
            this.grpProgression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProgression.Controls.Add(this.label2);
            this.grpProgression.Controls.Add(this.label1);
            this.grpProgression.Controls.Add(this.prgHole);
            this.grpProgression.Controls.Add(this.prgFile);
            this.grpProgression.Location = new System.Drawing.Point(28, 87);
            this.grpProgression.Name = "grpProgression";
            this.grpProgression.Size = new System.Drawing.Size(442, 100);
            this.grpProgression.TabIndex = 13;
            this.grpProgression.TabStop = false;
            this.grpProgression.Text = "Progression";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "File Progress";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Files";
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsLabel});
            this.stsMain.Location = new System.Drawing.Point(0, 639);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(815, 22);
            this.stsMain.TabIndex = 14;
            // 
            // stsLabel
            // 
            this.stsLabel.Name = "stsLabel";
            this.stsLabel.Size = new System.Drawing.Size(19, 17);
            this.stsLabel.Text = "...";
            // 
            // btnDrop
            // 
            this.btnDrop.Location = new System.Drawing.Point(354, 12);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(19, 23);
            this.btnDrop.TabIndex = 9;
            this.btnDrop.Text = "-";
            this.btnDrop.UseVisualStyleBackColor = true;
            this.btnDrop.Visible = false;
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
            // 
            // chkSkipFirst
            // 
            this.chkSkipFirst.AutoSize = true;
            this.chkSkipFirst.Location = new System.Drawing.Point(354, 40);
            this.chkSkipFirst.Name = "chkSkipFirst";
            this.chkSkipFirst.Size = new System.Drawing.Size(167, 17);
            this.chkSkipFirst.TabIndex = 15;
            this.chkSkipFirst.Text = "Don\'t Check First Folder\'s files";
            this.chkSkipFirst.UseVisualStyleBackColor = true;
            // 
            // clnFileName
            // 
            this.clnFileName.Text = "FileName";
            this.clnFileName.Width = 192;
            // 
            // clnSize
            // 
            this.clnSize.Text = "FileSize";
            // 
            // clnType
            // 
            this.clnType.Text = "File Type";
            this.clnType.Width = 81;
            // 
            // clnHash
            // 
            this.clnHash.Text = "Hash";
            this.clnHash.Width = 72;
            // 
            // clnFolder
            // 
            this.clnFolder.Text = "Location";
            this.clnFolder.Width = 337;
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.CheckBoxes = true;
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnFileName,
            this.clnSize,
            this.clnType,
            this.clnHash,
            this.clnFolder});
            this.lstFiles.ContextMenuStrip = this.ctxListView;
            this.lstFiles.FullRowSelect = true;
            this.lstFiles.Location = new System.Drawing.Point(12, 203);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.ShowItemToolTips = true;
            this.lstFiles.Size = new System.Drawing.Size(791, 431);
            this.lstFiles.TabIndex = 2;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstFiles_ItemChecked);
            this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.groupBox1.Controls.Add(this.nmMin);
            this.groupBox1.Controls.Add(this.rdbLMega);
            this.groupBox1.Controls.Add(this.rdbLKilo);
            this.groupBox1.Controls.Add(this.rdbLUnit);
            this.groupBox1.Controls.Add(this.rdbLMega2);
            this.groupBox1.Controls.Add(this.rdbLKilo2);
            this.groupBox1.Controls.Add(this.rdbLUnit2);
            this.groupBox1.Controls.Add(this.rdbLMega3);
            this.groupBox1.Controls.Add(this.rdbLKilo3);
            this.groupBox1.Controls.Add(this.rdbLUnit3);
            this.groupBox1.Location = new System.Drawing.Point(502, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 110);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Hashing Method:";
            // 
            // nmMin
            // 
            //this.nmMin.Location = new System.Drawing.Point(6, 18);
            //this.nmMin.Maximum = new decimal(new int[] {
            //1024,
            //0,
            //0,
            //0});
            //this.nmMin.Name = "nmMin";
            //this.nmMin.Size = new System.Drawing.Size(58, 20);
            //this.nmMin.TabIndex = 2;
            // 
            // rdbLMega
            // 
            this.rdbLMega.AutoSize = true;
            this.rdbLMega.Location = new System.Drawing.Point(210, 18);
            this.rdbLMega.Name = "rdbLMega";
            this.rdbLMega.Size = new System.Drawing.Size(41, 17);
            this.rdbLMega.TabIndex = 1;
            this.rdbLMega.Text = "MD5";
            this.rdbLMega.UseVisualStyleBackColor = true;
            // 
            // rdbLMega
            // 
            this.rdbLMega2.AutoSize = true;
            this.rdbLMega2.Location = new System.Drawing.Point(210, 50);
            this.rdbLMega2.Name = "rdbLMega2";
            this.rdbLMega2.Size = new System.Drawing.Size(41, 17);
            this.rdbLMega2.TabIndex = 1;
            this.rdbLMega2.Text = "Binary16";
            this.rdbLMega2.UseVisualStyleBackColor = true;
            // 
            // rdbLMega
            // 
            this.rdbLMega3.AutoSize = true;
            this.rdbLMega3.Location = new System.Drawing.Point(210, 82);
            this.rdbLMega3.Name = "rdbLMega3";
            this.rdbLMega3.Size = new System.Drawing.Size(41, 17);
            this.rdbLMega3.TabIndex = 1;
            this.rdbLMega3.Text = "Binary64";
            this.rdbLMega3.UseVisualStyleBackColor = true;
            // 
            // rdbLKilo
            // 
            this.rdbLKilo.AutoSize = true;
            this.rdbLKilo.Location = new System.Drawing.Point(110, 18);
            this.rdbLKilo.Name = "rdbLKilo";
            this.rdbLKilo.Size = new System.Drawing.Size(39, 17);
            this.rdbLKilo.TabIndex = 1;
            this.rdbLKilo.Text = "Gradient4";
            this.rdbLKilo.UseVisualStyleBackColor = true;
            // 
            // rdbLKilo
            // 
            this.rdbLKilo2.AutoSize = true;
            this.rdbLKilo2.Location = new System.Drawing.Point(110, 50);
            this.rdbLKilo2.Name = "rdbLKilo2";
            this.rdbLKilo2.Size = new System.Drawing.Size(39, 17);
            this.rdbLKilo2.TabIndex = 1;
            this.rdbLKilo2.Text = "Gradient16";
            this.rdbLKilo2.UseVisualStyleBackColor = true;
            //
            this.rdbLKilo3.AutoSize = true;
            this.rdbLKilo3.Location = new System.Drawing.Point(110, 82);
            this.rdbLKilo3.Name = "rdbLKilo3";
            this.rdbLKilo3.Size = new System.Drawing.Size(39, 17);
            this.rdbLKilo3.TabIndex = 1;
            this.rdbLKilo3.Text = "Gradient64";
            this.rdbLKilo3.UseVisualStyleBackColor = true;
            // 
            // rdbLUnit
            // 
            this.rdbLUnit.AutoSize = true;
            this.rdbLUnit.Checked = true;
            this.rdbLUnit.Location = new System.Drawing.Point(15, 18);
            this.rdbLUnit.Name = "rdbLUnit";
            this.rdbLUnit.Size = new System.Drawing.Size(51, 17);
            this.rdbLUnit.TabIndex = 1;
            this.rdbLUnit.TabStop = true;
            this.rdbLUnit.Text = "Average4";
            this.rdbLUnit.UseVisualStyleBackColor = true;
            // 
            // rdbLUnit
            // 
            this.rdbLUnit2.AutoSize = true;
            this.rdbLUnit2.Checked = true;
            this.rdbLUnit2.Location = new System.Drawing.Point(15, 50);
            this.rdbLUnit2.Name = "rdbLUnit2";
            this.rdbLUnit2.Size = new System.Drawing.Size(51, 17);
            this.rdbLUnit2.TabIndex = 1;
            this.rdbLUnit2.TabStop = true;
            this.rdbLUnit2.Text = "Average16";
            this.rdbLUnit2.UseVisualStyleBackColor = true;
            // 
            // rdbLUnit
            // 
            this.rdbLUnit3.AutoSize = true;
            this.rdbLUnit3.Checked = true;
            this.rdbLUnit3.Location = new System.Drawing.Point(15, 82);
            this.rdbLUnit3.Name = "rdbLUnit3";
            this.rdbLUnit3.Size = new System.Drawing.Size(51, 17);
            this.rdbLUnit3.TabIndex = 1;
            this.rdbLUnit3.TabStop = true;
            this.rdbLUnit3.Text = "Average64";
            this.rdbLUnit3.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(6, 26);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(103, 17);
            this.chkDelete.TabIndex = 17;
            this.chkDelete.Text = "Fully delete Files";
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.chkDelete_CheckedChanged);
            // 
            // groupBox2
            // 
            //this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.groupBox2.Controls.Add(this.nmMax);
            //this.groupBox2.Controls.Add(this.rdbMMega);
            //this.groupBox2.Controls.Add(this.rdbMKilo);
            //this.groupBox2.Controls.Add(this.rdbMUnit);
            //this.groupBox2.Location = new System.Drawing.Point(502, 145);
            //this.groupBox2.Name = "groupBox2";
            //this.groupBox2.Size = new System.Drawing.Size(234, 52);
            //this.groupBox2.TabIndex = 16;
            //this.groupBox2.TabStop = false;
            //this.groupBox2.Text = "Exclude Sizes more than";
            // 
            // nmMax
            // 
           // this.nmMax.Location = new System.Drawing.Point(6, 18);
            //this.nmMax.Maximum = new decimal(new int[] {
            //1024,
            //0,
            //0,
           // 0});
            //this.nmMax.Name = "nmMax";
            //this.nmMax.Size = new System.Drawing.Size(58, 20);
            //this.nmMax.TabIndex = 2;
            // 
            // rdbMMega
            // 
           // this.rdbMMega.AutoSize = true;
            //this.rdbMMega.Location = new System.Drawing.Point(172, 18);
            //this.rdbMMega.Name = "rdbMMega";
            //this.rdbMMega.Size = new System.Drawing.Size(41, 17);
            //this.rdbMMega.TabIndex = 1;
            //this.rdbMMega.Text = "MB";
           // this.rdbMMega.UseVisualStyleBackColor = true;
            // 
            // rdbMKilo
            // 
            //this.rdbMKilo.AutoSize = true;
            //this.rdbMKilo.Location = new System.Drawing.Point(127, 18);
            //this.rdbMKilo.Name = "rdbMKilo";
            //this.rdbMKilo.Size = new System.Drawing.Size(39, 17);
            //this.rdbMKilo.TabIndex = 1;
            //this.rdbMKilo.Text = "KB";
            //this.rdbMKilo.UseVisualStyleBackColor = true;
            // 
            // rdbMUnit
            // 
            //this.rdbMUnit.AutoSize = true;
            //this.rdbMUnit.Checked = true;
            //this.rdbMUnit.Location = new System.Drawing.Point(70, 18);
            //this.rdbMUnit.Name = "rdbMUnit";
            //this.rdbMUnit.Size = new System.Drawing.Size(51, 17);
            //this.rdbMUnit.TabIndex = 1;
            //this.rdbMUnit.TabStop = true;
            //this.rdbMUnit.Text = "Bytes";
            //this.rdbMUnit.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbRecycleBin);
            this.groupBox3.Controls.Add(this.rdbDUP);
            this.groupBox3.Controls.Add(this.chkDelete);
            this.groupBox3.Location = new System.Drawing.Point(534, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 61);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Deletion";
            // 
            // rdbRecycleBin
            // 
            this.rdbRecycleBin.AutoSize = true;
            this.rdbRecycleBin.Location = new System.Drawing.Point(116, 38);
            this.rdbRecycleBin.Name = "rdbRecycleBin";
            this.rdbRecycleBin.Size = new System.Drawing.Size(128, 17);
            this.rdbRecycleBin.TabIndex = 18;
            this.rdbRecycleBin.Text = "Move To Recycle Bin";
            this.rdbRecycleBin.UseVisualStyleBackColor = true;
            // 
            // rdbDUP
            // 
            this.rdbDUP.AutoSize = true;
            this.rdbDUP.Checked = true;
            this.rdbDUP.Location = new System.Drawing.Point(116, 20);
            this.rdbDUP.Name = "rdbDUP";
            this.rdbDUP.Size = new System.Drawing.Size(121, 17);
            this.rdbDUP.TabIndex = 18;
            this.rdbDUP.TabStop = true;
            this.rdbDUP.Text = "Move To Duplicates";
            this.rdbDUP.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 661);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkSkipFirst);
            //this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.grpProgression);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnDrop);
            this.Controls.Add(this.txtExtension);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.btnBrowse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duplicate Files Finder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ctxListView.ResumeLayout(false);
            this.grpProgression.ResumeLayout(false);
            this.grpProgression.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMin)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMax)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog fldBrowse;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.ContextMenuStrip ctxListView;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.ProgressBar prgHole;
        private System.Windows.Forms.ProgressBar prgFile;
        private System.Windows.Forms.GroupBox grpProgression;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel stsLabel;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.CheckBox chkSkipFirst;
        private System.Windows.Forms.ColumnHeader clnFileName;
        private System.Windows.Forms.ColumnHeader clnSize;
        private System.Windows.Forms.ColumnHeader clnType;
        private System.Windows.Forms.ColumnHeader clnHash;
        private System.Windows.Forms.ColumnHeader clnFolder;
        private System.Windows.Forms.ListView lstFiles;
        private System.Windows.Forms.ToolStripMenuItem viewWithNotepadToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nmMin;
        private System.Windows.Forms.RadioButton rdbLMega;
        private System.Windows.Forms.RadioButton rdbLKilo;
        private System.Windows.Forms.RadioButton rdbLUnit;
        private System.Windows.Forms.RadioButton rdbLMega2;
        private System.Windows.Forms.RadioButton rdbLKilo2;
        private System.Windows.Forms.RadioButton rdbLUnit2;
        private System.Windows.Forms.RadioButton rdbLMega3;
        private System.Windows.Forms.RadioButton rdbLKilo3;
        private System.Windows.Forms.RadioButton rdbLUnit3;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nmMax;
        private System.Windows.Forms.RadioButton rdbMMega;
        private System.Windows.Forms.RadioButton rdbMKilo;
        private System.Windows.Forms.RadioButton rdbMUnit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbRecycleBin;
        private System.Windows.Forms.RadioButton rdbDUP;
        
    }
}

