using System;
using System.Drawing;
using System.Windows.Forms;

namespace HLTextureTools
{
    class ListBoxEx : ListBox
    {
        private readonly SolidBrush backBrush;

        public ListBoxEx()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            backBrush = new SolidBrush(BackColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Region region = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(backBrush, region);
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Rectangle itemRectangle = GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(itemRectangle))
                    {
                        if ((SelectionMode == SelectionMode.One && SelectedIndex == i) || (SelectionMode == SelectionMode.MultiSimple && SelectedIndices.Contains(i)) || (SelectionMode == SelectionMode.MultiExtended && SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, Font, itemRectangle, i, DrawItemState.Selected, ForeColor, BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, Font, itemRectangle, i, DrawItemState.Default, ForeColor, BackColor));
                        }
                        region.Complement(itemRectangle);
                    }
                }
            }
            base.OnPaint(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            // Must refresh UI because on higher DPI font changes to smaller size in specific cases
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Refresh();

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                Refresh();
            }
        }
    }
}
