using System.Windows.Forms;

namespace HLTextureTools
{
    public delegate void OpenFileDelegate(string filename);

    class ListViewIndexComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
        }
    }
}
