using System;
using System.Collections;
using System.Text;

namespace DuplicateFinder.Comparers
{
    public class ListViewGroupSorter : IComparer
    {
        private System.Windows.Forms.SortOrder sortOrder;
        private int m_ColumnIdx = 0;

        public ListViewGroupSorter(System.Windows.Forms.SortOrder theOrder, int Column)
        {
            sortOrder = theOrder;
            m_ColumnIdx = Column;
        }

        public int Compare(object x, object y)
        {
            int result = 0;
            if (m_ColumnIdx != 1)
            {
                result = String.Compare(
                    ((System.Windows.Forms.ListViewGroup)x).Header,
                ((System.Windows.Forms.ListViewGroup)y).Header);
            }
            else
            {
                long X = long.Parse(((System.Windows.Forms.ListViewGroup)x).Header);
                long Y = long.Parse(((System.Windows.Forms.ListViewGroup)y).Header);
                result = (X >= Y ? 1 : -1);
            }
            if (sortOrder == System.Windows.Forms.SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }

}