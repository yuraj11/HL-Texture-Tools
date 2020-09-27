using System;
using System.Drawing;
using System.Windows.Forms;
using HLTools;
using System.IO;
using System.Threading;

namespace HLTextureTools
{
    public partial class NewSpriteForm : Form
    {
        private readonly OpenFileDelegate openFileNow;

        public NewSpriteForm(OpenFileDelegate openFile)
        {
            openFileNow = openFile;
            InitializeComponent();

            listPictures.ListViewItemSorter = new ListViewIndexComparer();
            helpBrowser.DocumentText = Properties.Resources.sprite_help_text;
        }

        private void btnAddImages_Click(object sender, EventArgs e)
        {
            //Import images
            if (addImagesFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddFiles(addImagesFileDialog.FileNames);
            }
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
                ListViewItem itemP = listPictures.Items[ai - 1];
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
            if (ai < (listPictures.Items.Count - 1))
            {
                int newIndex = ai + 1;
                ListViewItem itemP = listPictures.Items[ai + 1];
                listPictures.Items.Remove(itemP);
                listPictures.Items.Insert(ai, itemP);
                listPictures.Items[ai + 1] = a;
                listPictures.EnsureVisible(newIndex);

            }
        }

        private ListViewItem CreateImageListItem(string filename)
        {
            ListViewItem item = new ListViewItem();
            imageList1.Images.Add(Image.FromFile(filename));
            item.Text = Path.GetFileName(filename);
            item.ImageIndex = imageList1.Images.Count - 1;
            item.Tag = filename;
            return item;
        }

        private SprType GetSelectedSpriteType()
        {
            SprType newSprType = SprType.VP_PARALLEL_UPRIGHT;
            if (radioParallelUpright.Checked)
            {
                newSprType = SprType.VP_PARALLEL_UPRIGHT;
            }
            else if (radioFacingUpright.Checked)
            {
                newSprType = SprType.FACING_UPRIGHT;
            }
            else if (radioParallel.Checked)
            {
                newSprType = SprType.VP_PARALLEL;
            }
            else if (radioOriented.Checked)
            {
                newSprType = SprType.ORIENTED;
            }
            else if (radioParallelOriented.Checked)
            {
                newSprType = SprType.VP_PARALLEL_ORIENTED;
            }
            return newSprType;
        }

        private SprTextFormat GetSelectedSpriteFormat()
        {
            SprTextFormat newTextFormat = SprTextFormat.SPR_NORMAL;
            if (radioNormalTexture.Checked)
            {
                newTextFormat = SprTextFormat.SPR_NORMAL;
            }
            else if (radioAdditiveTexture.Checked)
            {
                newTextFormat = SprTextFormat.SPR_ADDITIVE;
            }
            else if (radioIndexAlphaTexture.Checked)
            {
                newTextFormat = SprTextFormat.SPR_INDEXALPHA;
            }
            else if (radioAlphaTestTexture.Checked)
            {
                newTextFormat = SprTextFormat.SPR_ALPHTEST;
            }
            return newTextFormat;
        }

        private void buttonSaveSprite_Click(object sender, EventArgs e)
        {
            if (saveSpriteFileDialog.ShowDialog() == DialogResult.OK)
            {
                progBar.Show();
                progLbl.Show();
                buttonSaveSprite.Enabled = false;
                tabControl1.Enabled = false;

                try
                {
                    string[] filenames = GetInputFilenames();
                    string savePath = saveSpriteFileDialog.FileName;
                    SprType spriteType = GetSelectedSpriteType();
                    SprTextFormat spriteFormat = GetSelectedSpriteFormat();
                    int paletteIndex = (int)inputPaletteIndex.Value - 1;

                    Thread thCreator = new Thread((o) =>
                    {
                        SpriteLoader.CreateSpriteFile(savePath, filenames, spriteType, spriteFormat, paletteIndex);
                    });
                    thCreator.Start();

                    while (thCreator.IsAlive)
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }

                    progBar.Hide();
                    progLbl.Hide();

                    if (MessageBox.Show("Sprite file created! Open it now in viewer?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        openFileNow(saveSpriteFileDialog.FileName);
                    }
                }
                finally
                {
                    Close();
                }
            }
        }

        private string[] GetInputFilenames()
        {
            string[] arr = new string[listPictures.Items.Count];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (string)listPictures.Items[i].Tag;
            }
            return arr;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                buttonSaveSprite.Enabled = listPictures.Items.Count > 0;
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
                }
                catch { }
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
