using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class DimensionsHelp : Form
    {
        const int CellSize = 25;
        int x, y = 0;
        SolidBrush selBrush;

        public DimensionsHelp()
        {
            InitializeComponent();
             selBrush = new SolidBrush(Color.FromArgb(50, Color.White));

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(selBrush, x, 0, CellSize, y + CellSize);
            e.Graphics.FillRectangle(selBrush, 0, y, x, CellSize);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.x = e.X;
            this.y = e.Y;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            this.x = -100;
            this.y = -100;
            pictureBox1.Invalidate();
        }

        private void DimensionsHelp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
