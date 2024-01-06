using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace HLTextureTools
{
    public partial class PaletteForm : Form
    {
        //Update palette delegate
        public delegate void UpdatePaletteDelegate(ColorPalette pal);
        private UpdatePaletteDelegate updatePalette = null;
        //
        public static ColorPalette palette = null;
        public static bool backup = false;
        private Color[] originalColorsPalette = null;
        private int ColorItemSize = 22;
        private int MaxItemsRow = 16;
        private bool isTransparentTexture = false;

        //current color under mouse
        private int tileX, tileY;

        /// <summary>
        /// Create palette form from ColorPalette.
        /// </summary>
        /// <param name="colPal">Color palette.</param>
        public PaletteForm(bool isTransparent, string name, ColorPalette colPal, UpdatePaletteDelegate palDelegate)
        {
            InitializeComponent();

            float dpiScale = DeviceDpi / 96.0f;
            ColorItemSize = (int)(ColorItemSize * dpiScale);

            isTransparentTexture = isTransparent;
            Text += name;

            //Backup actual color palette
            originalColorsPalette = new Color[colPal.Entries.Length];
            colPal.Entries.CopyTo(originalColorsPalette, 0);

            //Set color palette
            palette = colPal;
            updatePalette = palDelegate;
        }

        /// <summary>
        /// Convert Color to RGB string.
        /// </summary>
        /// <param name="clr">Input color.</param>
        /// <returns>Returns R,G,B.</returns>
        private string GetRGBString(Color clr)
        {
            return clr.R + "," + clr.G + "," + clr.B;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Check if is palette set
            if (palette == null)
            {
                return;
            }

            //Render color palette
            for (int i = 0, x = 0, y = 0; i < palette.Entries.Length; i++)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, palette.Entries[i])))
                {
                    if (i > 0 && (i % MaxItemsRow) == 0)
                    {
                        y += ColorItemSize;
                        x = 0;
                    }

                    int xPos = x * ColorItemSize;
                    e.Graphics.FillRectangle(brush, xPos, y, ColorItemSize, ColorItemSize);

                    x++;
                }
            }

            DrawSelectedItem(e);
            //Refresh picturebox
            //pictureBox1.Update();
        }

        private void DrawSelectedItem(PaintEventArgs e)
        {
            //Calculate relative tile positions
            Point mousePos = pictureBox1.PointToClient(MousePosition);
            tileX = mousePos.X / ColorItemSize;
            tileY = mousePos.Y / ColorItemSize;

            if (tileX > -1 && tileY > -1 && tileX < MaxItemsRow && tileY < MaxItemsRow)
            {
                //absolute x pos
                int helpTilePosX = (tileX * ColorItemSize);
                //absolute y pos
                int helpTilePosY = (tileY * ColorItemSize);
                //absolute width
                int helpTileWidth = (ColorItemSize);
                //absolute height
                int helpTileHeight = (ColorItemSize);

                //Hatch temporary rectangle on mouse move
                Rectangle tempRect = new Rectangle(
                    helpTilePosX,
                    helpTilePosY,
                    helpTileWidth - 2,
                    helpTileHeight - 2
                );

                //Draw temp. hatch rectangle
                e.Graphics.DrawRectangle(Pens.Red, tempRect);

                //Update RGB status
                Color entry = palette.Entries[tileX + (tileY * MaxItemsRow)];
                string colorStatus = GetRGBString(entry);
                if (textBox1.Text != colorStatus) //if changed color
                {
                    textBox1.Text = colorStatus;
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                palColorSelect.Color = palette.Entries[tileX + (tileY * MaxItemsRow)];
                if (palColorSelect.ShowDialog() == DialogResult.OK)
                {
                    palette.Entries[tileX + (tileY * MaxItemsRow)] = palColorSelect.Color;
                    pictureBox1.Invalidate();
                    updatePalette(palette);
                }
            }
            //Copy color to clipboard
            else if (e.Button == MouseButtons.Left && textBox1.TextLength > 0)
            {
                Clipboard.SetText(textBox1.Text);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Reset color palette to previous original state
            for (int i = 0; i < palette.Entries.Length; i++)
            {
                palette.Entries[i] = originalColorsPalette[i];
            }

            updatePalette(palette);
            pictureBox1.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Close window on escape
            if (msg.WParam.ToInt32() == (int)Keys.Escape)
            {
                Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool IsChangedPalette()
        {
            for (int i = 0; i < palette.Entries.Length; i++)
            {
                if (palette.Entries[i] != originalColorsPalette[i])
                {
                    return true;
                }
            }

            return false;
        }

        private void PaletteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChangedPalette() && this.DialogResult != DialogResult.Yes)
            {
                linkLabel3_LinkClicked(sender, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsChangedPalette())
            {
                linkLabel3_LinkClicked(sender, null);
            }

            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsChangedPalette())
            {
                updatePalette(palette);
                backup = checkBox1.Checked;
                DialogResult = DialogResult.Yes;
            }
            else
            {
                backup = false;
            }
            Close();
        }

        private int GetPaletteLength()
        {
            return palette.Entries.Length + (isTransparentTexture ? -1 : 0);
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false))
                {
                    sw.WriteLine("JASC-PAL");
                    sw.WriteLine("0100");
                    sw.WriteLine("256");
                    for (int i = 0; i < palette.Entries.Length; i++)
                    {
                        sw.WriteLine(string.Format("{0} {1} {2}", palette.Entries[i].R, palette.Entries[i].G, palette.Entries[i].B));
                    }
                }
            }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Try parse palette file
                try
                {
                    using (StreamReader sw = new StreamReader(openFileDialog.FileName))
                    {
                        if (sw.ReadLine() == "JASC-PAL") //Pal Identifier
                        {
                            string version = sw.ReadLine(); //pal version
                            string count = sw.ReadLine(); //pal colors count

                            for (int i = 0; i < palette.Entries.Length; i++) //read every palette color
                            {
                                string line = sw.ReadLine();
                                string[] clrs = line.Split(' ');
                                if (clrs.Length >= 3)
                                {
                                    palette.Entries[i] = Color.FromArgb(byte.Parse(clrs[0]), byte.Parse(clrs[1]), byte.Parse(clrs[2]));
                                }
                            }

                            pictureBox1.Invalidate();
                        }
                        else
                        {
                            MessageBox.Show("Unknown file type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    updatePalette(palette);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (palColorSelect.ShowDialog() == DialogResult.OK)
            {

                for (int i = 0; i < palette.Entries.Length; i++)
                {
                    palette.Entries[i] = palColorSelect.Color;

                }
                pictureBox1.Invalidate();
                updatePalette(palette);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //Invert
            byte r, g, b;
            for (int i = 0; i < GetPaletteLength(); i++)
            {
                r = palette.Entries[i].R;
                g = palette.Entries[i].G;
                b = palette.Entries[i].B;

                palette.Entries[i] = Color.FromArgb(255 - r, 255 - g, 255 - b);
            }

            pictureBox1.Invalidate();
            updatePalette(palette);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //Sepia
            byte r, g, b;
            int oR, oG, oB;

            for (int i = 0; i < GetPaletteLength(); i++)
            {
                r = palette.Entries[i].R;
                g = palette.Entries[i].G;
                b = palette.Entries[i].B;
                oR = (int)((r * .393) + (g * .769f) + (b * .189));
                oG = (int)((r * .349) + (g * .686) + (b * .168));
                oB = (int)((r * .272) + (g * .534) + (b * .131));

                palette.Entries[i] = Color.FromArgb(Math.Min(oR, 255), Math.Min(oG, 255), Math.Min(oB, 255));
            }

            pictureBox1.Invalidate();
            updatePalette(palette);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Grayscale
            for (int i = 0; i < GetPaletteLength(); i++)
            {
                int gray = (int)((palette.Entries[i].R * .3) + (palette.Entries[i].G * .59) + (palette.Entries[i].B * .11));
                palette.Entries[i] = Color.FromArgb(gray, gray, gray);
            }
            pictureBox1.Invalidate();
            updatePalette(palette);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Brighthness -
            int r, g, b;
            for (int i = 0; i < GetPaletteLength(); i++)
            {
                r = palette.Entries[i].R - 10;
                g = palette.Entries[i].G - 10;
                b = palette.Entries[i].B - 10;

                palette.Entries[i] = Color.FromArgb(Math.Max(r, 0), Math.Max(g, 0), Math.Max(b, 0));
            }

            pictureBox1.Invalidate();
            updatePalette(palette);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Brighthness +
            int r, g, b;
            for (int i = 0; i < GetPaletteLength(); i++)
            {
                r = palette.Entries[i].R + 10;
                g = palette.Entries[i].G + 10;
                b = palette.Entries[i].B + 10;

                palette.Entries[i] = Color.FromArgb(Math.Min(r, 255), Math.Min(g, 255), Math.Min(b, 255));
            }

            pictureBox1.Invalidate();
            updatePalette(palette);
        }
    }
}
