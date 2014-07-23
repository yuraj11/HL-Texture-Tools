using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLTextureTools
{
    class ListBoxEx : ListBox
    {
        private SolidBrush foreBrush;
        private SolidBrush backBrush;
        public ListBoxEx()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            this.foreBrush = new SolidBrush(this.ForeColor);
            this.backBrush = new SolidBrush(this.BackColor);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Region region = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(this.backBrush, region);
            if (base.Items.Count > 0)
            {
                for (int i = 0; i < base.Items.Count; i++)
                {
                    Rectangle itemRectangle = base.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(itemRectangle))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i) || (this.SelectionMode == SelectionMode.MultiSimple && base.SelectedIndices.Contains(i)) || (this.SelectionMode == SelectionMode.MultiExtended && base.SelectedIndices.Contains(i)))
                        {
                            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, itemRectangle, i, DrawItemState.Selected, this.ForeColor, this.BackColor));
                        }
                        else
                        {
                            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, itemRectangle, i, DrawItemState.Default, this.ForeColor, this.BackColor));
                        }
                        region.Complement(itemRectangle);
                    }
                }
            }
            base.OnPaint(e);
        }
    }
}
