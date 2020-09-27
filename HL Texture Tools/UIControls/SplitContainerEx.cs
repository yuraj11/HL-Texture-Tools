using System.Drawing;
using System.Windows.Forms;

namespace HLTextureTools
{
    class SplitContainerEx : SplitContainer
    {
        public SplitContainerEx()
        {
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw better splitter
            e.Graphics.Clear(Color.Silver);
            int centerX = SplitterRectangle.X+1;
            int centerY = (SplitterRectangle.Height / 2);
            int boxWidth = 3;
            e.Graphics.FillRectangle(Brushes.Gray, centerX, centerY, boxWidth, boxWidth);
            e.Graphics.FillRectangle(Brushes.Gray, centerX, centerY + 5, boxWidth, boxWidth);
            e.Graphics.FillRectangle(Brushes.Gray, centerX, centerY - 5, boxWidth, boxWidth);
        }
    }
}
