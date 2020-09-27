using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class VheManager : Form
    {
        private bool changed = false;
        private VheWadManager wadManager;
        private static string currentWad;

        private readonly OpenFileDelegate openFileNow;

        public VheManager(OpenFileDelegate openFileDelegate, string currentWad = null)
        {
            InitializeComponent();
            VheManager.currentWad = currentWad;
            openFileNow = openFileDelegate;
        }

        public static void NewWadInViewer(string filename)
        {
            currentWad = filename;
        }

        private void VheManager_Load(object sender, EventArgs e)
        {
            wadManager = new VheWadManager();
            listTextures.Items.AddRange(wadManager.GetWadList());
            btnRemoveAll.Enabled = listTextures.Items.Count > 0;
        }

        private void btnAddTextures_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentWad) && !listTextures.Items.Contains(currentWad.ToLower()))
            {
                addWadMenu.Show(btnAddTextures, new Point(0, btnAddTextures.Height));
            }
            else
            {
                fromFileToolStripMenuItem_Click(sender, e);
            }
        }

        private void btnRemoveTextures_Click(object sender, EventArgs e)
        {
            while (listTextures.SelectedItem != null)
            {
                listTextures.Items.Remove(listTextures.SelectedItem);
            }
            changed = true;
        }

        private void openWadFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] filenames = openWadFileDialog.FileNames;
            listTextures.BeginUpdate();
            for (int i = 0; i < filenames.Length; i++)
            {
                filenames[i] = filenames[i].ToLower();
                if (!listTextures.Items.Contains(filenames[i]))
                {
                    listTextures.Items.Add(filenames[i]);
                }
            }
            listTextures.EndUpdate();
            btnRemoveAll.Enabled = listTextures.Items.Count > 0;
            changed = true;
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openWadFileDialog.ShowDialog();
        }

        private void listTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveTextures.Enabled = listTextures.SelectedItems.Count > 0;
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            listTextures.Items.Clear();
            btnRemoveTextures.Enabled = false;
            changed = true;
            btnRemoveAll.Enabled = false;
        }

        private void listTextures_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            //Listbox ownerdraw item
            ListBox listBox = sender as ListBox;
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
            {
                return;
            }

            DialogResult res = MessageBox.Show("Do you wish to save changes?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            switch (res)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    //Save
                    string[] items = new string[listTextures.Items.Count];
                    listTextures.Items.CopyTo(items, 0);
                    wadManager.SaveWadList(items);
                    break;
            }
        }

        private void listTextures_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnRemoveTextures.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                //Deselect items
                if (listTextures.SelectedItems.Count == listTextures.Items.Count)
                {
                    listTextures.ClearSelected();
                }
                else
                {
                    for (int i = 0; i < listTextures.Items.Count; i++)
                    {
                        listTextures.SetSelected(i, true);
                    }
                }
            }
        }

        private void currentWADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listTextures.Items.Add(currentWad.ToLower());
            changed = true;
            currentWad = null;
        }

        private void listTextures_DoubleClick(object sender, EventArgs e)
        {
            if (listTextures.SelectedItem != null)
            {
                openFileNow(listTextures.SelectedItem.ToString());
            }
            listTextures.ClearSelected();
        }
    }
}
