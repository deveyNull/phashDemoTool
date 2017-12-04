using System;
using System.Windows.Forms;
using System.Collections;

namespace DuplicateFinder.Comparers
{
    public class ListViewItemComparer : IComparer
    {
        private int m_Column;
        private bool m_ColumnSortOrder;

        public ListViewItemComparer()
        {
            m_Column = 0;
            m_ColumnSortOrder = true;
        }

        public ListViewItemComparer(int column, bool AscendingOrder)
        {
            m_Column = column;
            m_ColumnSortOrder = AscendingOrder;
        }

        public int Compare(object x, object y)
        {
            if (m_ColumnSortOrder)
            {
                if ((m_Column == 0) || (m_Column == 4) || (m_Column == 2))
                    return String.Compare(((ListViewItem)x).SubItems[m_Column].Text, ((ListViewItem)y).SubItems[m_Column].Text);
                else
                {
                    long X = long.Parse(((ListViewItem)x).SubItems[m_Column].Text);
                    long Y = long.Parse(((ListViewItem)y).SubItems[m_Column].Text);
                    return X >= Y ? 1 : -1;
                }
            }
            else
            {
                if ((m_Column == 0) || (m_Column == 4) || (m_Column == 2))
                    return (-1) * String.Compare(((ListViewItem)x).SubItems[m_Column].Text, ((ListViewItem)y).SubItems[m_Column].Text);
                else
                {
                    long X = long.Parse(((ListViewItem)x).SubItems[m_Column].Text);
                    long Y = long.Parse(((ListViewItem)y).SubItems[m_Column].Text);
                    return X <= Y ? 1 : -1;
                }
            }
        }
    }
}