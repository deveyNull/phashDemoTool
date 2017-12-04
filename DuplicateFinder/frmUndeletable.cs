using System;
using System.Drawing;
using System.Windows.Forms;

namespace DuplicateFinder
{
    public partial class frmUndeletable : Form
    {
        public frmUndeletable(string[] items)
        {
            InitializeComponent();
            string s = string.Empty;
            foreach (string item in items)
            {
                s += "del \"" + item + "\" /F\n";
            }
            append(s);
        }

        public delegate void Append(string s);
        private void append(string p)
        {
            if (richTextBox1.InvokeRequired)
                richTextBox1.BeginInvoke(new Append(append), new object[] { p });
            else
                richTextBox1.AppendText(p);
        }

        private void frmUndeletable_Shown(object sender, EventArgs e)
        {
            selectAll();
        }

        public delegate void SelectALL();
        private void selectAll()
        {
            if (richTextBox1.InvokeRequired)
                richTextBox1.BeginInvoke(new SelectALL(selectAll));
            else
            {
                richTextBox1.SelectAll();
            }
        }
    }
}