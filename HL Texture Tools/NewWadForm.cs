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
    public partial class NewWadForm : Form
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

        public NewWadForm()
        {
            InitializeComponent();
            listPictures.ListViewItemSorter = new ListViewIndexComparer();
        }

        public NewWadForm(OpenFileDelegate openFile)
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
                listPictures.Visible = false;
                listPictures.BeginUpdate();
                ListViewItem[] items = new ListViewItem[openFileDialog1.FileNames.Length];
                int w, h = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = new ListViewItem();
                    using (Image origImage = Image.FromFile(openFileDialog1.FileNames[i]))
                    {
                        w = origImage.Width;
                        h = origImage.Height;
                        imageList1.Images.Add(origImage.GetThumbnailImage(72, 72, null, IntPtr.Zero));
                    }
                    string itemName = Path.GetFileNameWithoutExtension(openFileDialog1.FileNames[i]);
                    if (itemName.Length > 16)
                        itemName = itemName.Substring(0, 16);

                    items[i].Text = itemName;
                    items[i].SubItems.Add(string.Format("{0}x{1}", w, h));
                    if ((w % 8) != 0 || (h % 8) != 0)
                    {
                        items[i].SubItems.Add(string.Format("ERROR: Not divisible by 8!", w, h));
                    }
                    else
                    {
                        items[i].SubItems.Add("OK");
                    }
                    
                    items[i].ImageIndex = imageList1.Images.Count - 1;
                    items[i].Tag = openFileDialog1.FileNames[i];
                    listPictures.Items.Add(items[i]);
                    //Application.DoEvents();
                }
                listPictures.EndUpdate();
                listPictures.Visible = true;
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
                progBar.Show();
                progLbl.Show();
                button5.Enabled = false;
                tabControl1.Enabled = false;
                try
                {
                    string[] names;
                    string[] fNames;
                    GetInputFilenames(out names, out fNames);
                    Thread thCreator = new Thread((o) =>
                    {
                        WAD3Loader.CreateWad(saveFileDialog1.FileName, fNames, names, checkBox1.Checked);
                    });
                    thCreator.Start();

                    while (thCreator.IsAlive)
                    {
                        System.Threading.Thread.Sleep(10);
                        Application.DoEvents();
                    }

                    if (MessageBox.Show("Wad file created! Open it now in viewer?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (openFileNow != null)
                            openFileNow(saveFileDialog1.FileName);
                    }
                }
                finally
                {
                    progBar.Hide();
                    progLbl.Hide();
                    this.Close();
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
            if (tabControl1.SelectedIndex == 1)
            {
                button5.Enabled = listPictures.Items.Count > 0;
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            nMaxWidth.Enabled = nMaxHeight.Enabled = checkBox2.Checked;
        }



    }
}
