using System;
using System.Drawing;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class DimensionsHelp : Form
    {
        private const int CellSize = 25;
        private readonly SolidBrush selBrush = new SolidBrush(Color.FromArgb(50, Color.White));

        private int x, y = 0;

        public DimensionsHelp()
        {
            InitializeComponent();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(selBrush, x, 0, CellSize, y + CellSize);
            e.Graphics.FillRectangle(selBrush, 0, y, x, CellSize);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            x = -100;
            y = -100;
            pictureBox.Invalidate();
        }

        private void DimensionsHelp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
