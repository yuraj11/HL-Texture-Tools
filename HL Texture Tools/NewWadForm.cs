using System;
using System.Drawing;
using System.Windows.Forms;
using HLTools;
using System.Threading;
using System.IO;
using FreeImageAPI;

namespace HLTextureTools
{
    public partial class NewWadForm : Form
    {
        private readonly OpenFileDelegate openFileNow;

        public NewWadForm(OpenFileDelegate openFile)
        {
            openFileNow = openFile;
            InitializeComponent();

            listPictures.ListViewItemSorter = new ListViewIndexComparer();
            helpBrowser.DocumentText = Properties.Resources.wad_help_text;
        }

        private void btnAddImages_Click(object sender, EventArgs e)
        {
            //Import images
            if (addImagesFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddFiles(addImagesFileDialog.FileNames);
            }
        }

        private ListViewItem CreateImageListItem(string filename)
        {
            ListViewItem item = new ListViewItem();
            int w, h = 0;
            bool hasTrasparentColor = false;
            using (Image origImage = Image.FromFile(filename))
            using (FreeImageBitmap freeImage = new FreeImageBitmap(origImage))
            {
                hasTrasparentColor = freeImage.IsTransparent;
                w = origImage.Width;
                h = origImage.Height;
                imageList1.Images.Add(origImage.GetThumbnailImage(72, 72, null, IntPtr.Zero));
            }
            string itemName = Path.GetFileNameWithoutExtension(filename);
            if (hasTrasparentColor)
            {
                itemName = "{" + itemName;
            }
            if (itemName.Length > 16)
            {
                itemName = itemName.Substring(0, 16);
            }

            item.Text = itemName;
            item.SubItems.Add(string.Format("{0}x{1}", w, h));
            if ((w % 8) != 0 || (h % 8) != 0)
            {
                item.SubItems.Add(string.Format("ERROR: Not divisible by 8!", w, h));
            }
            else
            {
                item.SubItems.Add("OK");
            }

            item.ImageIndex = imageList1.Images.Count - 1;
            item.Tag = filename;

            return item;
        }

        private void btnRemoveImages_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listPictures.SelectedItems)
            {
                listPictures.Items.Remove(item);
            }
        }

        private void btnMoveImageUp_Click(object sender, EventArgs e)
        {
            //Move up
            ListViewItem a = listPictures.SelectedItems[0];
            int ai = listPictures.SelectedItems[0].Index;
            if (ai > 0)
            {
                int newIndex = ai - 1;
                ListViewItem itemP = listPictures.Items[ai-1];
                listPictures.Items.Remove(itemP);
                listPictures.Items[ai - 1] = a;
                listPictures.Items.Insert(ai, itemP);
                listPictures.EnsureVisible(newIndex);
            }
        }

        private void btnMoveImageDown_Click(object sender, EventArgs e)
        {
            //Move down
            ListViewItem a = listPictures.SelectedItems[0];
            int ai = listPictures.SelectedItems[0].Index;
            if (ai < (listPictures.Items.Count-1))
            {
                int newIndex = ai + 1;
                ListViewItem itemP = listPictures.Items[ai + 1];
                listPictures.Items.Remove(itemP);
                listPictures.Items.Insert(ai, itemP);
                listPictures.Items[ai+1] = a;
                listPictures.EnsureVisible(newIndex);
                
            }
        }

        private void btnSaveWad_Click(object sender, EventArgs e)
        {
            if (saveWadFileDialog.ShowDialog() == DialogResult.OK)
            {
                progBar.Show();
                progLbl.Show();
                btnSaveWad.Enabled = false;
                tabControl1.Enabled = false;
                try
                {
                    string savePath = saveWadFileDialog.FileName;
                    Color transparentColorReplacement = panelPickTransparentColor.BackColor;
                    bool reserveLastColor = checkBox1.Checked;
                    GetInputFilenames(out string[] names, out string[] fNames);

                    Thread thCreator = new Thread((o) =>
                    {
                        WAD3Loader.CreateWad(savePath, fNames, names, transparentColorReplacement, reserveLastColor);
                    });
                    thCreator.Start();

                    while (thCreator.IsAlive)
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }

                    progBar.Hide();
                    progLbl.Hide();

                    if (MessageBox.Show("Wad file created! Open it now in viewer?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        openFileNow(saveWadFileDialog.FileName);
                    }
                }
                finally
                {
                    Close();
                }
            }
        }

        private void GetInputFilenames(out string[] names, out string[] filenames)
        {
            names = new string[listPictures.Items.Count];
            filenames = new string[listPictures.Items.Count];
            for (int i = 0; i < listPictures.Items.Count; i++)
            {
                names[i] = listPictures.Items[i].Text;
                filenames[i] = (string)listPictures.Items[i].Tag;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                btnSaveWad.Enabled = listPictures.Items.Count > 0;
            }
        }

        private void listPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPictures.SelectedItems.Count > 0)
            {
                btnRemoveImages.Enabled = true;
                btnMoveImageUp.Enabled = listPictures.SelectedItems[0].Index > 0;
                btnMoveImageDown.Enabled = listPictures.SelectedItems[0].Index < (listPictures.Items.Count - 1);
            }
            else
            {
                btnRemoveImages.Enabled = false;
                btnMoveImageUp.Enabled = btnMoveImageDown.Enabled = false;
            }
        }

        private void listPictures_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listPictures.SelectedItems.Count > 0)
            {
                try
                {
                    System.Diagnostics.Process.Start(listPictures.SelectedItems[0].Tag.ToString());
                } catch { }
            }
        }

        private void listPictures_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnRemoveImages.PerformClick();
            }
            else if (e.Control == true && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in listPictures.Items)
                {
                    item.Selected = true;
                }
            }
        }

        private void listPictures_ItemActivate(object sender, EventArgs e)
        {
            if (listPictures.SelectedItems.Count > 0)
            {
                listPictures.SelectedItems[0].BeginEdit();
            }
        }

        private void listPictures_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;

            if (e.Label.Trim() == string.Empty)
            {
                e.CancelEdit = true;
            }
            else
            {
                if (e.Label.Length > 15)
                {
                    e.CancelEdit = true;
                    listPictures.Items[e.Item].Text = e.Label.Substring(0, 15);
                }
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            //Enabled file drop
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            AddFiles(files);
        }

        private void AddFiles(string[] filenames)
        {
            try
            {
                listPictures.Visible = false;
                listPictures.BeginUpdate();
                foreach (string path in filenames)
                {
                    listPictures.Items.Add(CreateImageListItem(path));
                }
                listPictures.EndUpdate();
            }
            catch
            {
                MessageBox.Show("Could not open file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listPictures.Visible = true;
            }
        }

        private void panelPickTransparentColor_Click(object sender, EventArgs e)
        {
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                panelPickTransparentColor.BackColor = colorPicker.Color;
            }
        }
    }
}
