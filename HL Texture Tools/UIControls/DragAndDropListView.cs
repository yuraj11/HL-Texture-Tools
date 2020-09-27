using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace HLTextureTools
{
    public class ListViewEx : ListView
    {
        private const string REORDER = "Reorder";

        private bool allowRowReorder = true;
        public bool AllowRowReorder
        {
            get
            {
                return allowRowReorder;
            }
            set
            {
                allowRowReorder = value;
            }
        }

        public new SortOrder Sorting
        {
            get
            {
                return SortOrder.None;
            }
            set
            {
                base.Sorting = SortOrder.None;
            }
        }

        public ListViewEx()
            : base()
        {
            AllowRowReorder = true;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            AllowDrop = false;

            if (!AllowRowReorder)
            {
                return;
            }
            if (SelectedItems.Count == 0)
            {
                return;
            }
            Point cp = PointToClient(new Point(e.X, e.Y));
            ListViewItem dragToItem = GetItemAt(cp.X, cp.Y);
            if (dragToItem == null)
            {
                return;
            }
            int dropIndex = dragToItem.Index;
            if (dropIndex > SelectedItems[0].Index)
            {
                dropIndex++;
            }
            ArrayList insertItems =
                new ArrayList(SelectedItems.Count);
            foreach (ListViewItem item in SelectedItems)
            {
                insertItems.Add(item.Clone());
            }
            for (int i = insertItems.Count - 1; i >= 0; i--)
            {
                ListViewItem insertItem =
                    (ListViewItem)insertItems[i];
                base.Items.Insert(dropIndex, insertItem);
            }
            foreach (ListViewItem removeItem in SelectedItems)
            {
                Items.Remove(removeItem);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!AllowRowReorder || !e.Data.GetDataPresent(DataFormats.Text))
            {
                return;
            }

            Point cp = PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = GetItemAt(cp.X, cp.Y);
            if (hoverItem == null)
            {
                return;
            }
            foreach (ListViewItem moveItem in SelectedItems)
            {
                if (moveItem.Index == hoverItem.Index)
                {
                    hoverItem.EnsureVisible();
                    return;
                }
            }
            base.OnDragOver(e);
            string text = (string)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
                hoverItem.EnsureVisible();
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!AllowRowReorder || !e.Data.GetDataPresent(DataFormats.Text))
            {
                return;
            }

            string text = (string) e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (!AllowRowReorder)
            {
                return;
            }
            AllowDrop = true;
            base.OnItemDrag(e);
            base.DoDragDrop(REORDER, DragDropEffects.Move);
        }
    }
}
