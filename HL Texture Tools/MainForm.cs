using BrendanGrant.Helpers.FileAssociation;
using HLTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class MainForm : Form
    {
        //Initialize WAD and Sprite loaders
        private readonly WAD3Loader wadLoader = new WAD3Loader();
        private readonly SpriteLoader sprLoader = new SpriteLoader();
        private SpriteLoader.Frame[] frames;

        private string lastDirectory = string.Empty;
        private string lastFilename = string.Empty;
        private HLFileType currentFileType = HLFileType.Wad;
        private HLFileType lastFileType = HLFileType.Wad;
        private int lastFrame = 0;
        private object prevSelected = null;
        private byte bOrW = 0;
        private bool selectingTransparent = false;
        private bool dontFireSelectEventFilesBox = false;
        private Image lastImageViewed = null;

        //Startup path
        public readonly string StartupPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private const string RecentFileName = "recent.dat";

        //Error msg
        private bool errorDraw = false;
        private string errorMessage = string.Empty;
        private readonly Font errorFont = new Font("Verdana", 10.0f);
        private readonly SolidBrush thumbnailBg = new SolidBrush(Color.FromArgb(150, Color.Black));
        private readonly Font thumbnailFont = new Font("Verdana", 6.0f);

        //MRU list
        private const int MRUnumber = 10;
        private readonly Queue<string> mruList = new Queue<string>();

        //Zoom
        private float lastZoomX = 0.0f, lastZoomY = 0.0f;
        private Point autoScrollCenterPos = Point.Empty;
        
        //
        private const int TransparentColorAlphaMagic = 80;

        enum HLFileType
        {
            Wad, Sprite
        }

        public MainForm()
        {
            InitializeComponent();
            //Add current version to caption
            Text += ProductVersion.Replace(".0.0", "");

            //Recent files load
            LoadRecentList();
            foreach (string item in mruList)
            {
                ToolStripMenuItem fileRecent = new ToolStripMenuItem(ShortenPathname(item, 48), null, RecentItemClick)
                {
                    Tag = item
                };  
                //create new menu for each item in list
                recFiles.DropDownItems.Insert(0, fileRecent); //add the menu to "recent" menu
            }

            //Set color dialog default color
            colorDialog.Color = panel1.BackColor;
            //Set thumbnails spacing
            ListViewTools.SetSpacing(listViewEx1, 74, 74);

            //Refersh setting
            RefreshSettings();

        }

        private void RefreshSettings()
        {
            animSpeedTextBox_TextChanged(null, null);
            if (Properties.Settings.Default.tl_size_1 == CheckState.Checked)
            {
                x32ToolStripMenuItem_Click(x32ToolStripMenuItem, null);
            }
            else if (Properties.Settings.Default.tl_size_2 == CheckState.Checked)
            {
                x64ToolStripMenuItem_Click(x64ToolStripMenuItem, null);
            }
            else if (Properties.Settings.Default.tl_size_3 == CheckState.Checked)
            {
                x128ToolStripMenuItem_Click(x128ToolStripMenuItem, null);
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.transparent_textures = transparentBgItem.CheckState;
            Properties.Settings.Default.tile_image = tileImageToolStripMenuItem.CheckState;
            Properties.Settings.Default.anim_speed = animSpeedTextBox.TextBox.Text;
            Properties.Settings.Default.tl_size_1 = x32ToolStripMenuItem.CheckState;
            Properties.Settings.Default.tl_size_2 = x64ToolStripMenuItem.CheckState;
            Properties.Settings.Default.tl_size_3 = x128ToolStripMenuItem.CheckState;
            Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close parsers.
            wadLoader.Close();
            sprLoader.Close();

            SaveSettings();
        }

        public void UpdateImagePalette(ColorPalette pal)
        {
            //Update BG image
            if (pictureBox1.Image != null || pictureBox1.BackgroundImage != null)
            {
                (pictureBox1.Image ?? pictureBox1.BackgroundImage).Palette = pal;
                pictureBox1.Refresh();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Selected item in listbox
            if (listBox1.SelectedItem != null && listBox1.SelectedItem != prevSelected)
            {
                ReloadCurrentImage();
            }
        }

        private void ReloadCurrentImage()
        {
            //Do not reload image If current is empty
            if (string.IsNullOrEmpty(lastFilename))
            {
                return;
            }

            panel1.AutoScrollPosition = Point.Empty;
            toolStripButton2.Enabled = false;
            ChangeTransparentColorMode(false);

            if (currentFileType == HLFileType.Wad)
            {
                //Refresh image
                try
                {
                    //Hide errors
                    SetErrorImageMessage(null);

                    //Show image info in status bar
                    WAD3Loader.WADLump lumpInfo = new WAD3Loader.WADLump();
                    if (listBox1.SelectedItem is WAD3Loader.WADLump lump)
                    {
                        lumpInfo = lump;
                        sLbl3.Text = "Name:";
                        nameLbl.Text = lumpInfo.Name;
                    }

                    //Try get lump
                    Bitmap bmp = wadLoader.GetLumpImage(listBox1.SelectedIndex, transparentBgItem.Checked).Image;
                    SetImage(bmp);

                    //Show size in statusbar
                    sizeLbl.Text = string.Format("{0:00}x{1:00}", bmp.Width, bmp.Height);

                    //Set panel scrollbars minsize
                    panel1.AutoScrollMinSize = bmp.Size;
                    fixTextureNameToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    prevSelected = listBox1.SelectedItem;

                    //Show error in picturebox
                    sizeLbl.Text = "-";
                    pictureBox1.Image = null;
                    pictureBox1.BackgroundImage = null;

                    SetErrorImageMessage("Error: " + ex.Message);
                }
            }
            else if (currentFileType == HLFileType.Sprite)
            {
                SetImage(frames[listBox1.SelectedIndex].Image);

                //Status
                sLbl3.Text = "Origin:";
                nameLbl.Text = string.Format("{0:00}x{1:00}",
                    frames[listBox1.SelectedIndex].OriginX,
                    frames[listBox1.SelectedIndex].OriginY);
                sizeLbl.Text = string.Format("{0:00}x{1:00}",
                    frames[listBox1.SelectedIndex].Image.Width,
                    frames[listBox1.SelectedIndex].Image.Height);
                panel1.AutoScrollMinSize = frames[listBox1.SelectedIndex].Image.Size;

                fixTextureNameToolStripMenuItem.Enabled = false;
            }

            prevSelected = listBox1.SelectedItem;
        }

        private void SetErrorImageMessage(string message)
        {
            if (message != null)
            {
                errorDraw = true;
                errorMessage = message;
                
            } 
            else
            {
                errorDraw = false;
            }
            pictureBox1.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Search box
            textBox1.Text = textBox1.Text.Replace("\r", "").Replace("\n", "");
            SearchItem(listBox1, textBox1.Text);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            //On search box enter, remove Search info text
            if (textBox1.Text == "Search")
            {
                textBox1.Clear();
            }

            textBox1.ForeColor = SystemColors.WindowText;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //On search box leave, if textlength = 0 add search info text
            if (textBox1.TextLength == 0)
            {
                textBox1.ForeColor = Color.Silver;
                textBox1.Text = "Search";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //If pressed enter in search box, find next item
            if (e.KeyCode == Keys.Enter)
            {
                if (!SearchItem(listBox1, textBox1.Text, listBox1.SelectedIndex + 1)) //if not found, search from start
                {
                    SearchItem(listBox1, textBox1.Text);
                }
                e.SuppressKeyPress = true;
            }

            //If pressed escape, clear box
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Clear();
            }
        }

        /// <summary>
        /// ListBox search item and selected item if found.
        /// </summary>
        private bool SearchItem(ListBox listBox, string text, int startIndex = 0)
        {
            if (startIndex < 0 || startIndex > listBox.Items.Count || text.Length == 0)
                return false;

            for (int i = startIndex; i < listBox.Items.Count; i++)
            {
                if (listBox.Items[i].ToString().IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    listBox1.SelectedIndex = i;
                    if (toolStripButton1.Checked)
                    {
                        listViewEx1.Items[i].EnsureVisible();
                        listViewEx1.Items[i].Selected = true;
                    }
                    return true;
                }
            }

            return false;
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show about dialog.
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        /// <summary>
        /// File Loading routine.
        /// </summary>
        /// <param name="filename">Input filename.</param>
        private void OpenFile(string filename)
        {
            try
            {
                //Mainly for delegates
                if (progressBar.Visible)
                {
                    //UGLY HACK!!!
                    return;
                }
                Activate();

                //Disable menu/toolbar items
                saveToolStripButton.Enabled = false;
                copyToolStripButton.Enabled = false;
                extractImageItem.Enabled = false;
                extractAllItem.Enabled = false;

                runSprToolButton.Enabled = false;
                colorPaletteToolItem.Enabled = false;
                copyMenuItem.Enabled = false;
                animateMenuItem.Enabled = false;
                sLbl2.Visible = false;
                typeLbl.Visible = false;
                sLbl3.Visible = sLbl4.Visible = false;
                nameLbl.Visible = sizeLbl.Visible = false;
                totalLbl.Visible = sLbl1.Visible = true;
                rotateLItem.Enabled = rotateRItem.Enabled = false;
                rlStripButton1.Enabled = rpStripButton1.Enabled = false;
                pStripButton.Enabled = toolStripButton2.Enabled = mStripButton.Enabled = false;
                paletteStripButton.Enabled = false;
                editToolStripMenuItem.Enabled = false;

                SetErrorImageMessage(null);
                pictureBox1.Image = null;
                listBox1.Enabled = true;

                switch (Path.GetExtension(filename).ToLower())
                {
                    case ".wad":
                        LoadWadFile(filename);
                        break;
                    case ".spr":
                        LoadSpriteFile(filename);
                        break;
                    default:
                        SetErrorImageMessage(string.Format("ERROR: Unknown file type! [{0}]", Path.GetFileName(filename)));
                        return;
                }

                //Update filesbox current name
                filesBox.Text = Path.GetFileName(filename);

                //Load list of supported files in directory
                LoadAllCompatibleFilesInDirectory(filename);

                //Insert lumps to listbox
                InsertTextureEntries();

                //Enable extract/copy
                saveToolStripButton.Enabled = true;
                copyToolStripButton.Enabled = true;
                extractImageItem.Enabled = true;
                extractAllItem.Enabled = true;

                colorPaletteToolItem.Enabled = true;
                textBox1.Enabled = true;
                copyMenuItem.Enabled = true;
                rotateLItem.Enabled = rotateRItem.Enabled = true;
                rlStripButton1.Enabled = rpStripButton1.Enabled = true;
                pStripButton.Enabled = mStripButton.Enabled = true;
                paletteStripButton.Enabled = true;

                //Show status bar labels
                sLbl3.Visible = sLbl4.Visible = true;
                nameLbl.Visible = sizeLbl.Visible = true;

                //Set last opened directory
                lastDirectory = Path.GetDirectoryName(filename);
                lastFileType = currentFileType;
                lastFilename = filename;

                //Set listbox selected item index
                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                listViewEx1.VirtualListSize = 0;
                listBox1.Items.Clear();
                totalLbl.Text = "-1";
                SetErrorImageMessage(string.Format("ERROR: While opening file! [{0}]\nDetail: {1}", Path.GetFileName(filename), ex.Message));
                listBox1.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void LoadWadFile(string filename)
        {
            //Stop animation if running
            timerAnimate.Enabled = false;
            runSprToolButton.Image = Properties.Resources.control_play_blue;

            wadLoader.LoadFile(filename);
            currentFileType = HLFileType.Wad;

            //Recent
            SaveRecentFile(filename);

            VheManager.NewWadInViewer(filename);

            totalLbl.Text = string.Format("{0:0000}", wadLoader.LumpsInfo.Count);
        }

        private void LoadSpriteFile(string filename)
        {
            frames = sprLoader.LoadFile(filename, transparentBgItem.Checked);

            if (frames.Length > 0 && sprLoader.SpriteHeader.TextFormat == SprTextFormat.SPR_ADDITIVE)
            {
                pictureBox1.BackColor = Color.FromArgb(255, frames[0].Image.Palette.Entries[frames[0].Image.Palette.Entries.Length - 1]);
            }

            currentFileType = HLFileType.Sprite;
            if (frames.Length > 1) //If sprite contains more frames
            {
                runSprToolButton.Enabled = true;
                animateMenuItem.Enabled = true;

            }
            else
            {
                //Stop animation if running
                timerAnimate.Enabled = false;
                runSprToolButton.Image = Properties.Resources.control_play_blue;
            }

            //
            listViewEx1.Hide();
            listBox1.Show();

            sLbl2.Visible = true;
            typeLbl.Visible = true;
            editToolStripMenuItem.Enabled = true;
            toolStripButton1.Enabled = false;
            //Recent
            SaveRecentFile(filename);

            totalLbl.Text = string.Format("{0:0000}", frames.Length);
            typeLbl.Text = string.Format("{0} | {1}", sprLoader.SpriteHeader.Type.ToString(), sprLoader.SpriteHeader.TextFormat.ToString());
        }

        private void LoadAllCompatibleFilesInDirectory(string filename)
        {
            string actualDir = Path.GetDirectoryName(filename);
            if (actualDir != lastDirectory || currentFileType != lastFileType)
            {
                filesBox.Enabled = true;
                filesBox.BeginUpdate();
                filesBox.Items.Clear();

                string[] files = Directory.GetFiles(actualDir, (currentFileType == HLFileType.Wad) ? "*.wad" : "*.spr", SearchOption.TopDirectoryOnly);
                string[] filesNew = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    filesNew[i] = Path.GetFileName(files[i]);
                }
                filesBox.Items.AddRange(filesNew);
                filesBox.EndUpdate();

                //Prev/Next buttons
                if (files.Length > 0)
                {
                    prevFileBtn.Enabled = true;
                    nextFileBtn.Enabled = true;
                    dontFireSelectEventFilesBox = true;
                    filesBox.SelectedIndex = filesBox.FindStringExact(filesBox.Text);
                    dontFireSelectEventFilesBox = false;
                    CheckControlsWays();
                }
            }
        }

        private void InsertTextureEntries()
        {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();

            if (currentFileType == HLFileType.Wad)
            {
                foreach (WAD3Loader.WADLump item in wadLoader.LumpsInfo)
                {
                    listBox1.Items.Add(item);
                }
                listBox1.EndUpdate();

                //Thumbnail mode
                //Virtual size of listview
                if (listViewEx1.Items.Count > 0)
                {
                    listViewEx1.Items[0].Selected = true;
                    listViewEx1.Items[0].EnsureVisible();
                }

                listViewEx1.VirtualListSize = wadLoader.LumpsInfo.Count;
                toolStripButton1.Enabled = true;

                tlCache.Images.Clear();
                if (toolStripButton1.Checked)
                {
                    UpdateImageCache();
                }
            }
            else if (currentFileType == HLFileType.Sprite)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    listBox1.Items.Add("Frame #" + i);
                }
                listBox1.EndUpdate();
            }
        }

        private void openFileItem_Click(object sender, EventArgs e)
        {
            //Show open file dialog, try open file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Close();
        }

        private void bgColorItem_Click(object sender, EventArgs e)
        {
            //Set bg color
            colorDialog.Color = pictureBox1.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog.Color;
            }
        }

        private void transparentBgItem_Click(object sender, EventArgs e)
        {
            //Refresh selected image if changed bg type (trans,opaq)
            ReloadCurrentImage();
        }

        private void extractImageItem_Click(object sender, EventArgs e)
        {
            //Extract current image
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format;

                switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                {
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".jpeg":
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;
                    case ".tiff":
                        format = ImageFormat.Tiff;
                        break;
                    default:
                        format = ImageFormat.Bmp;
                        break;
                }

                //Extract image
                if (pictureBox1.Image != null)
                {
                    lastImageViewed.Save(saveFileDialog.FileName, format);
                }
            }
        }

        private void colorPaletteToolItem_Click(object sender, EventArgs e)
        {
            //Get color palette from current image
            ColorPalette pal = null;
            if (lastImageViewed != null)
            {
                pal = lastImageViewed.Palette;
                if (toolStripButton2.Enabled)
                {
                    toolStripButton2.PerformClick();
                }
            }
            
            //Create palette form if can
            if (pal != null)
            {
                string title = "";
                string selItemText = listBox1.SelectedItem.ToString();
                if (currentFileType == HLFileType.Wad)
                {
                    title = (" - " + selItemText) ?? "";
                }

                PaletteForm palForm = new PaletteForm(selItemText.StartsWith("{") || (currentFileType == HLFileType.Sprite && sprLoader.SpriteHeader.TextFormat == SprTextFormat.SPR_ALPHTEST), 
                    title, pal, UpdateImagePalette);

                if (palForm.ShowDialog() == DialogResult.Yes)
                {
                    //pictureBox1.Image.Palette = PaletteForm.palette;

                    if (currentFileType == HLFileType.Sprite && frames != null)
                    {
                        for (int i = 0; i < frames.Length; i++)
                        {
                            frames[i].Image.Palette = PaletteForm.palette;
                        }
                    }
                    pictureBox1.Refresh();

                    if (PaletteForm.backup)
                        DoBackup();

                    if (currentFileType == HLFileType.Sprite)
                    {
                        sprLoader.ChangeColorPalette(PaletteForm.palette);
                    }
                    else if (currentFileType == HLFileType.Wad)
                    {
                        wadLoader.ChangeColorPalette(PaletteForm.palette);
                    }
                }
            }
        }

        private void tileImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Tile image
            SetImage(lastImageViewed);
            toolStripButton2.Enabled = false;
        }

        private void SetImage(Image img)
        {
            //Reset zoom
            lastImageViewed = img;
            lastZoomX = 0.0f;
            lastZoomY = 0.0f;

            //Switch tile mode
            if (!tileImageToolStripMenuItem.Checked)
            {
                pictureBox1.Image = img;
                pictureBox1.BackgroundImage = null;

            }
            else
            {
                pictureBox1.BackgroundImage = img;
                pictureBox1.Image = null;
            }
        }

        private void timerAnimate_Tick(object sender, EventArgs e)
        {
            //Animation timer
            if (lastFrame >= (listBox1.Items.Count - 1))
            {
                lastFrame = 0;
            }
            else
            {
                lastFrame++;
            }

            SetImage(frames[lastFrame].Image);
            //Refresh status label (current index)
            //statusLbl.Text = "Image index: #" + lastFrame.ToString();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
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

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            //Try open dropped file
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s.Length > 0)
            {
                string filename = s[0];

                OpenFile(filename);
            }
        }

        private void filesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dontFireSelectEventFilesBox) return;
            //If selected index changed in filesbox, load file
            if (filesBox.SelectedItem != null || Path.GetDirectoryName(lastFilename) != lastDirectory)
            {
                ForceRefresh();
                CheckControlsWays();
            }
        }

        private void ForceRefresh(bool f = false)
        {
            OpenFile(Path.Combine(lastDirectory, f ? filesBox.Text : filesBox.Items[filesBox.SelectedIndex].ToString()));
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            //Copy image to clipboard
            if (pictureBox1.Image != null || pictureBox1.BackgroundImage != null)
            {
                Clipboard.SetImage(pictureBox1.Image ?? lastImageViewed);
            }
        }

        private void animateMenuItem_Click(object sender, EventArgs e)
        {
            //Sprite Animation run/pause
            if (timerAnimate.Enabled)
            {
                textBox1.Enabled = true;
                listBox1.Enabled = true;
                copyToolStripButton.Enabled = true;
                copyMenuItem.Enabled = true;
                saveToolStripButton.Enabled = true;
                extractImageItem.Enabled = true;
                colorPaletteToolItem.Enabled = true;
                paletteStripButton.Enabled = true;
                extractAllItem.Enabled = true;

                rotateRItem.Enabled = rotateLItem.Enabled = true;
                rlStripButton1.Enabled = rpStripButton1.Enabled = true;
                pStripButton.Enabled = mStripButton.Enabled = true;
                editToolStripMenuItem.Enabled = true;
                //
                timerAnimate.Enabled = false;
                listBox1.SelectedIndex = 0;
                runSprToolButton.Image = Properties.Resources.control_play_blue;
            }
            else
            {
                listBox1.ClearSelected();
                lastFrame = 0;
                textBox1.Enabled = false;
                listBox1.Enabled = false;
                copyToolStripButton.Enabled = false;
                copyMenuItem.Enabled = false;
                saveToolStripButton.Enabled = false;
                extractImageItem.Enabled = false;
                extractAllItem.Enabled = false;

                colorPaletteToolItem.Enabled = false;
                paletteStripButton.Enabled = false;
                rotateRItem.Enabled = rotateLItem.Enabled = false;
                rlStripButton1.Enabled = rpStripButton1.Enabled = false;
                pStripButton.Enabled = mStripButton.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                //
                timerAnimate.Enabled = true;
                runSprToolButton.Image = Properties.Resources.control_stop_blue;
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            //Listbox ownerdraw item
            ListBox listBox = (sender as ListBox);
            string currentItem = listBox.Items[e.Index].ToString();

            //Current item text
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) //Selected state
            {
                e.DrawBackground();
                e.Graphics.DrawString(currentItem, e.Font, Brushes.White, e.Bounds);
            }
            else if (!listBox.Enabled) //Disabled state
            {
                //e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, 0, listBox.Width, listBox.Height);
                e.Graphics.DrawString(currentItem, e.Font, SystemBrushes.GrayText, e.Bounds);
            }
            else
            {
                e.DrawBackground();

                if (currentItem.Length > 0)
                {
                    Brush brush = Brushes.Black;
                    switch (currentItem[0])
                    {
                        case '{': //Transparent
                            brush = Brushes.Blue;
                            break;
                        case '!': //Water
                            brush = Brushes.Red;
                            break;
                        case '+': //Toggling
                            brush = Brushes.Green;
                            break;
                        case '-': //Random tiling
                            brush = Brushes.Purple;
                            break;
                        case '~': //Computers,lights?
                            brush = Brushes.Brown;
                            break;
                        default:
                            break;
                    }

                    e.Graphics.DrawString(currentItem, e.Font, brush, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(currentItem, e.Font, Brushes.Black, e.Bounds);
                }
            }
            //Focus rect.
            //e.DrawFocusRectangle();
        }

        private void animSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            //if anim. speed changed
            if (animSpeedTextBox.Text != "")
            {
                if (!int.TryParse(animSpeedTextBox.Text, out int res) || res < 1)
                {
                    animSpeedTextBox.Text = "100";
                    timerAnimate.Interval = 100;
                }
                else
                {
                    timerAnimate.Interval = res;
                }

            }
        }

        #region Extract All
        private void ExtractAll(ImageFormat format)
        {
            //Extract all textures
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                //Convert format to string
                string ext = string.Empty;

                if (format == ImageFormat.Png)
                {
                    ext = ".png";
                }
                else if (format == ImageFormat.Jpeg)
                {
                    ext = ".jpg";
                }
                else if (format == ImageFormat.Gif)
                {
                    ext = ".gif";
                }
                else if (format == ImageFormat.Tiff)
                {
                    ext = ".tiff";
                }
                else if (format == ImageFormat.Bmp)
                {
                    ext = ".bmp";
                }

                toolStrip1.Enabled = false;
                menuStrip1.Enabled = false;
                progressBar.Visible = true;
                progressBar.Value = 0;

                //Extract by input filetype
                if (currentFileType == HLFileType.Wad)
                {
                    progressBar.Maximum = listBox1.Items.Count;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i] is WAD3Loader.WADLump lumpInfo)
                        {
                            using (Bitmap lump = wadLoader.GetLumpImage(i, transparentBgItem.Checked).Image)
                            {
                                lump.Save(Path.Combine(folderDialog.SelectedPath, CleanPath(lumpInfo.Name) + ext), format);
                            }
                        }
                        progressBar.Value++;
                        Application.DoEvents();
                    }

                }
                else if (currentFileType == HLFileType.Sprite)
                {
                    progressBar.Maximum = frames.Length;

                    for (int i = 0; i < frames.Length; i++)
                    {
                        if (frames[i].Image != null) //Safe check if not null image
                        {
                            frames[i].Image.Save(Path.Combine(folderDialog.SelectedPath, "Frame " + i.ToString() + ext), format);
                        }
                        progressBar.Value++;
                        Application.DoEvents();
                    }
                }
                progressBar.Visible = false;
                toolStrip1.Enabled = true;
                menuStrip1.Enabled = true;
            }
        }

        private string CleanPath(string illegal)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                illegal = illegal.Replace(c.ToString(), "_");
            }

            return illegal;
        }

        private void asPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractAll(ImageFormat.Png);
        }

        private void asJPEGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractAll(ImageFormat.Jpeg);
        }

        private void asBMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractAll(ImageFormat.Bmp);
        }

        private void asGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractAll(ImageFormat.Gif);
        }

        private void asTIFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractAll(ImageFormat.Tiff);
        }


        private void namesToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Extract all textures names
            if (saveFileDialogTxt.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialogTxt.FileName, false))
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i] != null)
                        {
                            sw.WriteLine(listBox1.Items[i]);
                        }
                    }
                }
            }
        }
        #endregion

        private void RotateImage(bool right)
        {
            //If is set image
            if (pictureBox1.Image != null || pictureBox1.BackgroundImage != null)
            {
                RotateFlipType flipType;
                //Rotate to L or R
                if (!right)
                {
                    flipType = RotateFlipType.Rotate270FlipNone;
                }
                else
                {
                    flipType = RotateFlipType.Rotate90FlipNone;
                }

                (pictureBox1.Image ?? pictureBox1.BackgroundImage).RotateFlip(flipType);
                pictureBox1.Refresh();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Reset zoom
            ResetZoom();
        }

        private void ResetZoom()
        {
            toolStripButton2.Enabled = false;

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = lastImageViewed;
            }
            else if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage = lastImageViewed;
            }

            lastZoomX = 0.0f;
            lastZoomY = 0.0f;
            panel1.AutoScrollMinSize =  new System.Drawing.Size(lastImageViewed.Width, lastImageViewed.Height);
            panel1.AutoScrollPosition = Point.Empty;
        }

        private void ZoomImage(bool zoom)
        {
            if (pictureBox1.Image != null || pictureBox1.BackgroundImage != null)
            {
                if (lastImageViewed != null)
                {
                    if (lastZoomX == 0.0f)
                        lastZoomX = lastImageViewed.Width;

                    if (lastZoomY == 0.0f)
                        lastZoomY = lastImageViewed.Height;

                    using (FreeImageAPI.FreeImageBitmap fBmp = new FreeImageAPI.FreeImageBitmap(lastImageViewed))
                    {

                        if (zoom)
                        {
                            if (lastZoomX < 5000.0f)
                            {
                                lastZoomX *= 1.20f;
                                lastZoomY *= 1.20f;
                            }
                        }
                        else
                        {
                            if (lastZoomX > 32.0f)
                            {
                                lastZoomX *= 0.80f;
                                lastZoomY *= 0.80f;
                            }
                        }

                        fBmp.Rescale((int)(lastZoomX), (int)(lastZoomY), FreeImageAPI.FREE_IMAGE_FILTER.FILTER_BOX);
                        fBmp.ConvertColorDepth(FreeImageAPI.FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
                        fBmp.Quantize(FreeImageAPI.FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, 256, new FreeImageAPI.Palette(lastImageViewed.Palette.Entries));
                        
                        Bitmap bmpScaled = fBmp.ToBitmap();

                        /*ColorPalette palette = bmpScaled.Palette;
                        Color[] clrs = palette.Entries;

                        clrs[bmpScaled.Palette.Entries.Length - 1] = Color.FromArgb(0, Color.Blue);
                        bmpScaled.Palette = palette;
                        */
                        bmpScaled.MakeTransparent(Color.Blue);
                        if (!tileImageToolStripMenuItem.Checked)
                        {
                            pictureBox1.Image = bmpScaled;
                        }
                        else
                        {
                            pictureBox1.BackgroundImage = bmpScaled;
                        }

                        panel1.AutoScrollMinSize = new Size(fBmp.Width, fBmp.Height);
                        GC.Collect();
                        toolStripButton2.Enabled = true;
                    }
                }
            }
        }

        private void rotateLItem_Click(object sender, EventArgs e)  
        {
            RotateImage(false);
        }

        private void rotateRItem_Click(object sender, EventArgs e)
        {
            RotateImage(true);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditForm editFrm = new EditForm(sprLoader);
            editFrm.ShowDialog();

            //Update
            OpenFile(lastFilename);
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            listBox1_SelectedIndexChanged(sender, e);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (errorDraw)
            {
                float xPos = ((e.ClipRectangle.Width - e.Graphics.MeasureString(errorMessage, errorFont).Width) / 2);
                float yPos = (e.ClipRectangle.Height / 2) - errorFont.Height;
                e.Graphics.DrawString(errorMessage, errorFont, Brushes.Black, xPos, yPos);
                e.Graphics.DrawString(errorMessage, errorFont, Brushes.Red, xPos - 1, yPos - 1);
            }
        }



        private void LoadRecentList()
        {
            //try to load file. If file isn't found, do nothing
            mruList.Clear();
            try
            {
                //read file stream
                using (StreamReader listToRead = new StreamReader(Path.Combine(StartupPath, RecentFileName)))
                {
                    string line;
                    while ((line = listToRead.ReadLine()) != null) //read each line until end of file
                    {
                        if (File.Exists(line))
                        {
                            mruList.Enqueue(line); //insert to list
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch { }
        }

        private void SaveRecentFile(string path)
        {
            //clear all recent list from menu
            recFiles.DropDownItems.Clear();

            LoadRecentList(); //load list from file
            if (!(mruList.Contains(path))) //prevent duplication on recent list
                mruList.Enqueue(path); //insert given path into list
            //keep list number not exceeded the given value
            while (mruList.Count > MRUnumber)
            {
                mruList.Dequeue();
            }
            foreach (string item in mruList)
            {
                //create new menu for each item in list
                ToolStripMenuItem fileRecent = new ToolStripMenuItem
                             (ShortenPathname(item, 48), null, RecentItemClick)
                {
                    Tag = item
                };
                //add the menu to "recent" menu
                recFiles.DropDownItems.Insert(0, fileRecent);
            }
            //writing menu list to file
            //create file called "Recent.txt" located on app folder
            using (StreamWriter stringToWrite = new StreamWriter(Path.Combine(StartupPath, RecentFileName)))
            {
                foreach (string item in mruList)
                {
                    stringToWrite.WriteLine(item); //write list to stream
                }
                stringToWrite.Flush(); //write stream to file
            }
        }

        private void RemoveRecentsFile()
        {
            File.Delete(Path.Combine(StartupPath, RecentFileName));
        }

        private void RecentItemClick(object sender, EventArgs e)
        {
            OpenFile((sender as ToolStripMenuItem).Tag.ToString());
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //Enable recent wads
            recFiles.Enabled = (recFiles.DropDownItems.Count > 0);
        }

        /// <summary>
        /// Shortens a pathname for display purposes.
        /// </summary>
        /// <param labelName="pathname">The pathname to shorten.</param>
        /// <param labelName="maxLength">The maximum number of characters to be displayed.</param>
        /// <remarks>Shortens a pathname by either removing consecutive components of a path
        /// and/or by removing characters from the end of the filename and replacing
        /// then with three elipses (...)
        /// <para>In all cases, the root of the passed path will be preserved in it's entirety.</para>
        /// <para>If a UNC path is used or the pathname and maxLength are particularly short,
        /// the resulting path may be longer than maxLength.</para>
        /// <para>This method expects fully resolved pathnames to be passed to it.
        /// (Use Path.GetFullPath() to obtain this.)</para>
        /// </remarks>
        /// <returns></returns>
        static public string ShortenPathname(string pathname, int maxLength)
        {
            if (pathname.Length <= maxLength)
                return pathname;

            string root = Path.GetPathRoot(pathname);
            if (root.Length > 3)
                root += Path.DirectorySeparatorChar;

            string[] elements = pathname.Substring(root.Length).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            int filenameIndex = elements.GetLength(0) - 1;

            if (elements.GetLength(0) == 1) // pathname is just a root and filename
            {
                if (elements[0].Length > 5) // long enough to shorten
                {
                    // if path is a UNC path, root may be rather long
                    if (root.Length + 6 >= maxLength)
                    {
                        return root + elements[0].Substring(0, 3) + "...";
                    }
                    else
                    {
                        return pathname.Substring(0, maxLength - 3) + "...";
                    }
                }
            }
            else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength) // pathname is just a root and filename
            {
                root += "...\\";

                int len = elements[filenameIndex].Length;
                if (len < 6)
                    return root + elements[filenameIndex];

                if ((root.Length + 6) >= maxLength)
                {
                    len = 3;
                }
                else
                {
                    len = maxLength - root.Length - 3;
                }
                return root + elements[filenameIndex].Substring(0, len) + "...";
            }
            else if (elements.GetLength(0) == 2)
            {
                return root + "...\\" + elements[1];
            }
            else
            {
                int len = 0;
                int begin = 0;

                for (int i = 0; i < filenameIndex; i++)
                {
                    if (elements[i].Length > len)
                    {
                        begin = i;
                        len = elements[i].Length;
                    }
                }

                int totalLength = pathname.Length - len + 3;
                int end = begin + 1;

                while (totalLength > maxLength)
                {
                    if (begin > 0)
                        totalLength -= elements[--begin].Length - 1;

                    if (totalLength <= maxLength)
                        break;

                    if (end < filenameIndex)
                        totalLength -= elements[++end].Length - 1;

                    if (begin == 0 && end == filenameIndex)
                        break;
                }

                // assemble final string

                for (int i = 0; i < begin; i++)
                {
                    root += elements[i] + '\\';
                }

                root += "...\\";

                for (int i = end; i < filenameIndex; i++)
                {
                    root += elements[i] + '\\';
                }

                return root + elements[filenameIndex];
            }
            return pathname;
        }

        private void createNewSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSpriteForm sprForm = new NewSpriteForm(OpenFile);
            sprForm.ShowDialog();
        }



        private void ChangeTransparentColorMode(bool on)
        {
            if (on)
            {
                toolStripButton2.PerformClick();
                this.selectingTransparent = true;
                transLbl.Show();
                lblRgb.Show();
                pictureBox1.Cursor = Cursors.Cross;
                pStripButton.Enabled = mStripButton.Enabled = false;
            }
            else
            {

                //Reset colors
                if (lastImageViewed != null && selectingTransparent)
                {
                    ColorPalette pal = lastImageViewed.Palette;
                    for (int i = 0; i < pal.Entries.Length; i++)
                    {
                        if (pal.Entries[i].A == TransparentColorAlphaMagic)
                            pal.Entries[i] = Color.FromArgb(255, pal.Entries[i]);
                    }
                    UpdateImagePalette(pal);
                }

                this.selectingTransparent = false;
                transLbl.Hide();
                lblRgb.Hide();
                pictureBox1.Cursor = Cursors.Default;
                pStripButton.Enabled = mStripButton.Enabled = true;
            }
        }


        private void toolsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (currentFileType == HLFileType.Sprite)
            {
                if ((sprLoader.SpriteHeader.TextFormat == SprTextFormat.SPR_ALPHTEST || sprLoader.SpriteHeader.TextFormat == SprTextFormat.SPR_INDEXALPHA))
                {
                    makeTransparentColorToolStripMenuItem.Enabled =
                        (pictureBox1.Image != null || pictureBox1.BackgroundImage != null);
                }
                else
                {
                    makeTransparentColorToolStripMenuItem.Enabled = false;
                }
            }

            if (currentFileType == HLFileType.Wad)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    fixTextureNameToolStripMenuItem.Enabled = true;
                    if (listBox1.SelectedItem.ToString().StartsWith("{"))
                    {
                        makeTransparentColorToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        makeTransparentColorToolStripMenuItem.Enabled = false;
                    }
                }
            }
            else
            {
                fixTextureNameToolStripMenuItem.Enabled = false;
            }

            manageVHETexturesToolStripMenuItem.Enabled = VheWadManager.IsVheInstalled();
        }

        private void selectColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTransparentColorMode(true);
        }

        private void DoBackup()
        {
            if (currentFileType == HLFileType.Sprite)
            {
                File.Copy(sprLoader.Filename, Path.ChangeExtension(sprLoader.Filename, ".bak.spr"), true);
            }
            else if (currentFileType == HLFileType.Wad)
            {
                File.Copy(wadLoader.Filename, Path.ChangeExtension(wadLoader.Filename, ".bak.wad"), true);
            }
        }

        private void fixTextureNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string val = listBox1.SelectedItem.ToString();
                if (DialogHelper.InputBox("Name", "Enter new name of texture:", ref val, 15) == DialogResult.OK)
                {
                    if (val.Length > 0)
                    {
                        wadLoader.ChangeTextureName(listBox1.SelectedIndex, val);
                        WAD3Loader.WADLump lumpInfo = (WAD3Loader.WADLump)listBox1.SelectedItem;
                        lumpInfo.Name = val;
                        listBox1.Items[listBox1.SelectedIndex] = lumpInfo;

                        //Update thumbnail name if needed
                        if (toolStripButton1.Checked)
                        {
                            //TODO: UpdateImageCache();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Texture name can't be empty!", "Texture name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void createNewWadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewWadForm wadForm = new NewWadForm(OpenFile);
            wadForm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripButton).Checked)
            {

                UpdateImageCache();
                listViewEx1.Visible = true;
                listBox1.Visible = false;
                if (listBox1.SelectedItem != null)
                {
                    listViewEx1.Items[listBox1.SelectedIndex].Selected = true;
                    listViewEx1.Items[listBox1.SelectedIndex].EnsureVisible();
                }
                listViewEx1.Focus();
                //textBox1.Enabled = false;
                //listViewEx1.EnsureVisible(listBox1.SelectedIndex);

            }
            else
            {
                tlCache.Images.Clear();
                listViewEx1.Visible = false;
                listBox1.Visible = true;
                //textBox1.Enabled = true;
            }
        }

        private void UpdateImageCache()
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Maximum = wadLoader.LumpsInfo.Count;
            toolStrip1.Enabled = false;
            menuStrip1.Enabled = false;
            listBox1.Enabled = false;
            //listViewEx1.Enabled = false;
            //listViewEx1.BeginUpdate();
            try
            {
                Thread th = new Thread(() =>
                {
                    for (int i = 0; i < wadLoader.LumpsInfo.Count; i++)
                    {
                        Bitmap bmp;
                        try
                        {
                            bmp = wadLoader.GetLumpImage(i).Image;
                        }
                        catch
                        {
                            bmp = new Bitmap(tlCache.ImageSize.Width, tlCache.ImageSize.Height, PixelFormat.Format8bppIndexed);
                        }
                        Image resized = bmp.GetThumbnailImage(tlCache.ImageSize.Width, tlCache.ImageSize.Height, null, IntPtr.Zero);

                        //Don't add label to small thumbnails!
                        if (tlCache.ImageSize.Width >= 64)
                        {
                            using (Graphics g = Graphics.FromImage(resized))
                            {
                                g.FillRectangle(thumbnailBg, 0, 0, resized.Width, 15);
                                g.DrawString(wadLoader.LumpsInfo[i].Name, thumbnailFont, Brushes.Black, new PointF(1, 1));
                                g.DrawString(wadLoader.LumpsInfo[i].Name, thumbnailFont, Brushes.Black, new PointF(3, 3));
                                g.DrawString(wadLoader.LumpsInfo[i].Name, thumbnailFont, Brushes.White, new PointF(2, 2));
                            }
                        }

                        tlCache.Images.Add(resized);
                        bmp.Dispose();
                        resized.Dispose();


                        UpdateProgress();
                        if ((i % 50) == 0)
                            RefreshListView();
                    }
                });

                //th.IsBackground = true;
                th.Start();
                while (th.IsAlive)
                {
                    if (this.IsDisposed) Application.Exit();
                    Thread.Sleep(5);
                    Application.DoEvents();
                }
                listViewEx1.Invalidate(); listBox1.EndUpdate();
                GC.Collect();
            }
            finally
            {
                //listViewEx1.EndUpdate();
                toolStrip1.Enabled = true;
                menuStrip1.Enabled = true;
                listBox1.Enabled = true;
                //listViewEx1.Enabled = true;
                progressBar.Visible = false;
            }
        }


        private void UpdateProgress()
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.BeginInvoke(new MethodInvoker(() => UpdateProgress()));
            }
            else
            {
                progressBar.Increment(1);
            }
        }

        private void RefreshListView()
        {
            if (listViewEx1.InvokeRequired)
            {
                listViewEx1.BeginInvoke(new MethodInvoker(() => RefreshListView()));
            }
            else
            {
                listViewEx1.Invalidate();
            }
        }


        private void listViewEx1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(string.Empty, e.ItemIndex);
        }

        private void listViewEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tlCache.Images.Count > 0)
            {
                if (listViewEx1.SelectedIndices.Count > 0)
                {
                    listBox1.SelectedIndex = listViewEx1.SelectedIndices[0];
                }
            }
        }

        private void recommendedTextureSizesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsOpenForm(typeof(DimensionsHelp), true))
            {
                DimensionsHelp dHelp = new DimensionsHelp();
                dHelp.Show();
            }
        }

        private void manageVHETexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsOpenForm(typeof(VheManager), true))
            {
                VheManager wadDlg = new VheManager(OpenFile, currentFileType == HLFileType.Wad ? lastFilename : null);
                wadDlg.Show();
            }
        }

        private bool IsOpenForm(Type form, bool activateIfOpen = false)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == form)
                {
                    if (activateIfOpen)
                    {
                        frm.Activate();
                    }
                    return true;
                }
            }

            return false;
        }

        private void prevFileBtn_Click(object sender, EventArgs e)
        {
            if (filesBox.SelectedIndex > 0)
                filesBox.SelectedIndex--;
            if (listBox1.Visible)
            {
                Focus();
                listBox1.Focus();
            }
            else
            {
                Focus();
                listViewEx1.Focus();
            }
            CheckControlsWays();
        }

        private void nextFileBtn_Click(object sender, EventArgs e)
        {
            if (filesBox.SelectedIndex < (filesBox.Items.Count - 1))
            {
                filesBox.SelectedIndex++;
            }
            //this.Focus();
            if (listBox1.Visible)
            {
                Focus();
                listBox1.Focus();
            }
            else
            {
                Focus();
                listViewEx1.Focus();
            }

            CheckControlsWays();
        }

        private void CheckControlsWays()
        {
            prevFileBtn.Enabled = filesBox.SelectedIndex > 0 || filesBox.SelectedIndex == -1;
            nextFileBtn.Enabled = filesBox.SelectedIndex < (filesBox.Items.Count - 1);
        }

        private void extractWADFromBSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractWadBsp frmExtractor = new ExtractWadBsp(OpenFile);
            frmExtractor.ShowDialog();
        }

        private void associateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UAC.IsAdmin())
            {
                if (!UAC.RestartElevated("assoc"))
                {
                    MessageBox.Show("Can't associate extension without admin rights!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {

                DialogResult dRes = MessageBox.Show("Associate *.wad and *.spr files with this program?", "File(s) association",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dRes == DialogResult.Yes)
                {
                    FileAssociationInfo faiWad = new FileAssociationInfo(".wad");
                    FileAssociationInfo faiSpr = new FileAssociationInfo(".spr");
                    faiWad.Create(Application.ProductName);
                    faiSpr.Create(Application.ProductName);
                    faiWad.OpenWithList = new string[] { Application.ExecutablePath };
                    faiSpr.OpenWithList = new string[] { Application.ExecutablePath };

                    ProgramAssociationInfo paiWad = new ProgramAssociationInfo(faiWad.ProgID);
                    ProgramAssociationInfo paiSpr = new ProgramAssociationInfo(faiSpr.ProgID);
                    paiWad.Create(new ProgramVerb("Open", Application.ExecutablePath + " \"%1\""));
                    paiSpr.Create(new ProgramVerb("Open", Application.ExecutablePath + " \"%1\""));
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //File association
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                if (args[1] == "assoc")
                {
                    if (UAC.IsAdmin())
                    {
                        associateToolStripMenuItem_Click(sender, e);
                    }
                }
                else if (File.Exists(args[1]))
                {
                    OpenFile(args[1]);
                }
            }
        }

        private void x32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckMenuItemSizes(sender as ToolStripMenuItem);
            ChangeThumbnailSize(32, 32);
        }

        private void x64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckMenuItemSizes(sender as ToolStripMenuItem);
            ChangeThumbnailSize(64, 64);
        }

        private void x128ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckMenuItemSizes(sender as ToolStripMenuItem);
            ChangeThumbnailSize(128, 128);
        }

        private void CheckMenuItemSizes(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem menuItem in thumbnailSizeToolStripMenuItem.DropDownItems)
            {
                if (menuItem == item)
                {
                    menuItem.Checked = true;
                }
                else
                {
                    menuItem.Checked = false;
                }
            }
        }

        private void ChangeThumbnailSize(short width, short height)
        {
            short space = 10;
            ListViewTools.SetSpacing(listViewEx1, (short)(width + space), (short)(height + space));
            tlCache.ImageSize = new Size(width, height);
            UpdateImageCache();
        }

        private void pStripButton_Click(object sender, EventArgs e)
        {
            ZoomImage(true);
        }

        private void mStripButton_Click(object sender, EventArgs e)
        {
            ZoomImage(false);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    //Scrolling image only if not in tile mode
                    if (pictureBox1.Image != null || pictureBox1.BackgroundImage != null)
                    {
                        if (!tileImageToolStripMenuItem.Checked)
                        {
                            Point p = GetPicBoxAbsolutePosition();
                            Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
                            if (mouseRect.IntersectsWith(new Rectangle(p.X, p.Y, pictureBox1.Image.Width, pictureBox1.Image.Height)))
                            {
                                autoScrollCenterPos = e.Location;
                                //Change cursor only if can scroll
                                if (panel1.VerticalScroll.Visible || panel1.HorizontalScroll.Visible)
                                {
                                    pictureBox1.Cursor = Cursors.SizeAll;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (selectingTransparent)
                {
                    Point p = GetPicBoxAbsolutePosition();
                    if ((e.X - p.X >= 0 && e.X < pictureBox1.Width - p.X) &&
                        (e.Y - p.Y >= 0 && e.Y < pictureBox1.Height - p.Y))
                    {
                        try
                        {
                            Color clr = ((Bitmap)pictureBox1.Image).GetPixel(e.X - p.X, e.Y - p.Y);
                            if (clr.A == 0) return; // ignore transparent
                            ColorPalette pal = lastImageViewed.Palette;
                            for (int i = 0; i < pal.Entries.Length; i++)
                            {
                                if (pal.Entries[i] == clr)
                                {
                                    pal.Entries[i] = Color.FromArgb(TransparentColorAlphaMagic, clr.R, clr.G, clr.B);
                                }
                                else
                                {
                                    if (pal.Entries[i].A == TransparentColorAlphaMagic)
                                        pal.Entries[i] = Color.FromArgb(255, pal.Entries[i].R, pal.Entries[i].G, pal.Entries[i].B);
                                }
                            }
                            
                            UpdateImagePalette(pal);
                            transLbl.Text = string.Format("RGB: {0:D3},{1:D3},{2:D3}", clr.R, clr.G, clr.B);
                            lblRgb.BackColor = Color.FromArgb(255,clr);
                        }
                        catch { }

                    }
                }

                if (!autoScrollCenterPos.IsEmpty && e.Button == MouseButtons.Left)
                {
                    Point changePoint = new Point(e.Location.X - autoScrollCenterPos.X,
                                e.Location.Y - autoScrollCenterPos.Y);
                    panel1.AutoScrollPosition = new Point(-panel1.AutoScrollPosition.X - changePoint.X,
                                                          -panel1.AutoScrollPosition.Y - changePoint.Y);
                }
            }
        }

        private Point GetPicBoxAbsolutePosition()
        {

            return new Point((pictureBox1.Width - pictureBox1.Image.Width) / 2, (pictureBox1.Height - pictureBox1.Image.Height) / 2);
        }

        private void SwitchPicBg()
        {
            if (bOrW == 0)
                pictureBox1.BackColor = Color.White;
            else if (bOrW == 1)
                pictureBox1.BackColor = Color.Black;
            else if (bOrW == 2)
            {
                pictureBox1.BackColor = colorDialog.Color;
                bOrW = 0;
                return;
            }

            bOrW++;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;

            if (!selectingTransparent)
            {
                if (e.Button == MouseButtons.Left && autoScrollCenterPos.IsEmpty)
                {
                    SwitchPicBg();
                }
            }
            else //While selecting transparent color
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point p = GetPicBoxAbsolutePosition();
                    int x = e.X - p.X, y = e.Y - p.Y;

                    if (doBackupToolStripMenuItem.Checked)
                        DoBackup();

                    Image img = pictureBox1.Image;
                    if (img == null)
                        img = pictureBox1.BackgroundImage;

                    ColorPalette newPal = img.Palette;
                    newPal.Entries[newPal.Entries.Length - 1] = Color.Blue;
                    UpdateImagePalette(newPal);
                    int selectedPixelIndex;
                    if (currentFileType == HLFileType.Sprite)
                    {
                        try
                        {
                            selectedPixelIndex = sprLoader.GetPixelIndexAtPos(listBox1.SelectedIndex, x, y);
                            sprLoader.ChangeColorPalette(newPal);
                            sprLoader.SwitchColorIndex((byte)selectedPixelIndex, (byte)(newPal.Entries.Length - 1));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            MessageBox.Show("Error, wrong pixel selected!");
                        }
                    }
                    else if (currentFileType == HLFileType.Wad)
                    {
                        try
                        {
                            selectedPixelIndex = wadLoader.GetPixelIndexAtPos(x, y);
                            wadLoader.ChangeColorPalette(newPal);
                            wadLoader.SwitchColorIndex((byte)selectedPixelIndex, (byte)(newPal.Entries.Length - 1));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            MessageBox.Show("Error, wrong pixel selected!");
                        }
                    }

                    pictureBox1.Refresh();

                    ChangeTransparentColorMode(false);
                    ForceRefresh(true);
                    listBox1_SelectedIndexChanged(sender, e);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    ChangeTransparentColorMode(false);
                }
            }

            autoScrollCenterPos = Point.Empty;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!autoScrollCenterPos.IsEmpty)
            {
                autoScrollCenterPos = Point.Empty;
                pictureBox1.Cursor = Cursors.Default;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Zoom keys
            if (keyData == (Keys.Add | Keys.Control) || keyData == (Keys.Oemplus | Keys.Control))
            {
                pStripButton.PerformClick();
            }
            else if (keyData == (Keys.Subtract | Keys.Control) || keyData == (Keys.OemMinus | Keys.Control))
            {
                mStripButton.PerformClick();
            }
            else if (keyData == Keys.Escape && selectingTransparent)
            {
                ChangeTransparentColorMode(false);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void switchBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchPicBg();
        }

        private void projectHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/yuraj11/HL-Texture-Tools");
        }
    }
}