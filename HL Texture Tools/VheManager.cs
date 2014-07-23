using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class VheManager : Form
    {
        private bool changed = false;
        private VheWadManager wadManager;
        private static string currentWad;

        public delegate void OpenFileDelegate(string filename);
        private OpenFileDelegate openFileNow;

        public VheManager(OpenFileDelegate openFileDelegate, string currentWad = null)
        {
            InitializeComponent();
            VheManager.currentWad = currentWad;
            this.openFileNow = openFileDelegate;
        }

        public static void NewWadInViewer(string filename)
        {
            currentWad = filename;
        }

        private void VheManager_Load(object sender, EventArgs e)
        {
            this.wadManager = new VheWadManager();
            listBox1.Items.AddRange(wadManager.GetWadList());
            button3.Enabled = (listBox1.Items.Count > 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentWad) && !listBox1.Items.Contains(currentWad.ToLower()))
            {
                addWadMenu.Show(button1, new Point(0, button1.Height));
            }
            else
            {
                fromFileToolStripMenuItem_Click(sender, e);
            }
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            while (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            changed = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string[] filenames = openFileDialog1.FileNames;
            listBox1.BeginUpdate();
            for (int i = 0; i < filenames.Length; i++)
            {
                filenames[i] = filenames[i].ToLower();
                if (!listBox1.Items.Contains(filenames[i]))
                {
                    listBox1.Items.Add(filenames[i]);
                }
            }
            listBox1.EndUpdate();
            button3.Enabled = (listBox1.Items.Count > 0);
            changed = true;
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = listBox1.SelectedItems.Count > 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button2.Enabled = false;
            changed = true;
            button3.Enabled = false;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            //Listbox ownerdraw item
            ListBox listBox = (sender as ListBox);
            string itemTextOriginal = listBox.Items[e.Index].ToString();
            string fName = Path.GetFileName(itemTextOriginal);
            string currentItem = itemTextOriginal.Remove(itemTextOriginal.IndexOf(fName));

            //Current item text
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) //Selected state
            {
                e.DrawBackground();
                e.Graphics.DrawString(itemTextOriginal, e.Font, Brushes.White, e.Bounds);
            }
            else if (!listBox.Enabled) //Disabled state
            {
                //e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, 0, listBox.Width, listBox.Height);
                e.Graphics.DrawString(currentItem, e.Font, SystemBrushes.GrayText, e.Bounds);
            }
            else
            {
                e.DrawBackground();

                e.Graphics.DrawString(currentItem, e.Font, Brushes.Black, e.Bounds);
                SizeF textSize = e.Graphics.MeasureString(currentItem, e.Font);
                e.Graphics.DrawString(fName, e.Font, Brushes.Red, new PointF(textSize.Width - 6, e.Bounds.Y));
            }
        }

        private void VheManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!changed)
                return;
            DialogResult res = MessageBox.Show("Do you wish to save changes?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            switch (res)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    //Save
                    string[] items = new string[listBox1.Items.Count];
                    listBox1.Items.CopyTo(items, 0);
                    wadManager.SaveWadList(items);
                    break;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button2.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                //Deselect items
                if (listBox1.SelectedItems.Count == listBox1.Items.Count)
                {
                    listBox1.ClearSelected();
                }
                else
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        listBox1.SetSelected(i, true);
                    }
                }
            }
        }

        private void currentWADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(currentWad.ToLower());
            changed = true;
            VheManager.currentWad = null;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.openFileNow(listBox1.SelectedItem.ToString());
            }
            listBox1.ClearSelected();
        }
    }
}
