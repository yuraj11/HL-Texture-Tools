using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLTools;
using System.Threading;
using System.IO;

namespace HLTextureTools
{
    public partial class NewSpriteForm : Form
    {
        public delegate void OpenFileDelegate(string filename);
        private OpenFileDelegate openFileNow;

        private class ListViewIndexComparer : System.Collections.IComparer
        {

            public int Compare(object x, object y)
            {
                return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
            }
        }

        public NewSpriteForm()
        {
            InitializeComponent();
            listPictures.ListViewItemSorter = new ListViewIndexComparer();
        }

        public NewSpriteForm(OpenFileDelegate openFile)
        {
            openFileNow = openFile;
            InitializeComponent();
            listPictures.ListViewItemSorter = new ListViewIndexComparer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Import images
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listPictures.BeginUpdate();
                ListViewItem[] items = new ListViewItem[openFileDialog1.FileNames.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = new ListViewItem();
                    imageList1.Images.Add(Image.FromFile(openFileDialog1.FileNames[i]));
                    items[i].Text = Path.GetFileName(openFileDialog1.FileNames[i]);
                    items[i].ImageIndex = imageList1.Images.Count - 1;
                    items[i].Tag = openFileDialog1.FileNames[i];
                }
                listPictures.Items.AddRange(items);
                listPictures.EndUpdate();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listPictures.SelectedItems)
            {
                listPictures.Items.Remove(item);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SprType newSprType = SprType.VP_PARALLEL_UPRIGHT;
                if (radioButton1.Checked)
                {
                    newSprType = SprType.VP_PARALLEL_UPRIGHT;
                }
                else if (radioButton2.Checked)
                {
                    newSprType = SprType.FACING_UPRIGHT;
                }
                else if (radioButton3.Checked)
                {
                    newSprType = SprType.VP_PARALLEL;
                }
                else if (radioButton4.Checked)
                {
                    newSprType = SprType.ORIENTED;
                }
                else if (radioButton5.Checked)
                {
                    newSprType = SprType.VP_PARALLEL_ORIENTED;
                }

                SprTextFormat newTextFormat = SprTextFormat.SPR_NORMAL;
                if (radioButton6.Checked)
                {
                    newTextFormat = SprTextFormat.SPR_NORMAL;
                }
                else if (radioButton7.Checked)
                {
                    newTextFormat = SprTextFormat.SPR_ADDITIVE;
                }
                else if (radioButton8.Checked)
                {
                    newTextFormat = SprTextFormat.SPR_INDEXALPHA;
                }
                else if (radioButton9.Checked)
                {
                    newTextFormat = SprTextFormat.SPR_ALPHTEST;
                }


                SpriteLoader.CreateSpriteFile(saveFileDialog1.FileName, GetInputFilenames(), newSprType, newTextFormat, (int)numericUpDown1.Value -1);
                if (MessageBox.Show("Sprite file created! Open it now in viewer?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (openFileNow != null)
                        openFileNow(saveFileDialog1.FileName);
                }
                this.Close();
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
                button5.Enabled = listPictures.Items.Count > 0;
            }
            else
            {
                //
            }
        }


        private void listPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPictures.SelectedItems.Count > 0)
            {
                button2.Enabled = true;
                button3.Enabled = listPictures.SelectedItems[0].Index > 0;
                button4.Enabled = listPictures.SelectedItems[0].Index < (listPictures.Items.Count - 1);
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = button4.Enabled = false;
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
                button2.PerformClick();
            } else if (e.Control == true && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in listPictures.Items)
                {
                    item.Selected = true;
                }
            }
        }


    }
}
