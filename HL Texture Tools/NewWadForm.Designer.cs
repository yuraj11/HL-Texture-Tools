namespace HLTextureTools
{
    partial class NewWadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listPictures = new HLTextureTools.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnMoveImageDown = new System.Windows.Forms.Button();
            this.btnMoveImageUp = new System.Windows.Forms.Button();
            this.btnRemoveImages = new System.Windows.Forms.Button();
            this.btnAddImages = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPickTransparentColor = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.helpBrowser = new System.Windows.Forms.WebBrowser();
            this.progLbl = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.btnSaveWad = new System.Windows.Forms.Button();
            this.addImagesFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveWadFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(724, 551);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listPictures);
            this.tabPage1.Controls.Add(this.btnMoveImageDown);
            this.tabPage1.Controls.Add(this.btnMoveImageUp);
            this.tabPage1.Controls.Add(this.btnRemoveImages);
            this.tabPage1.Controls.Add(this.btnAddImages);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(716, 522);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1. Images";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listPictures
            // 
            this.listPictures.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listPictures.AllowRowReorder = true;
            this.listPictures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPictures.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listPictures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listPictures.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listPictures.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listPictures.HideSelection = false;
            this.listPictures.LabelEdit = true;
            this.listPictures.LargeImageList = this.imageList1;
            this.listPictures.Location = new System.Drawing.Point(8, 37);
            this.listPictures.Name = "listPictures";
            this.listPictures.ShowGroups = false;
            this.listPictures.Size = new System.Drawing.Size(702, 477);
            this.listPictures.TabIndex = 5;
            this.listPictures.TileSize = new System.Drawing.Size(230, 75);
            this.listPictures.UseCompatibleStateImageBehavior = false;
            this.listPictures.View = System.Windows.Forms.View.Tile;
            this.listPictures.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listPictures_AfterLabelEdit);
            this.listPictures.ItemActivate += new System.EventHandler(this.listPictures_ItemActivate);
            this.listPictures.SelectedIndexChanged += new System.EventHandler(this.listPictures_SelectedIndexChanged);
            this.listPictures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listPictures_KeyDown);
            this.listPictures.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listPictures_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(72, 72);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnMoveImageDown
            // 
            this.btnMoveImageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveImageDown.Enabled = false;
            this.btnMoveImageDown.Image = global::HLTextureTools.Properties.Resources.arrow_down;
            this.btnMoveImageDown.Location = new System.Drawing.Point(672, 8);
            this.btnMoveImageDown.Name = "btnMoveImageDown";
            this.btnMoveImageDown.Size = new System.Drawing.Size(30, 23);
            this.btnMoveImageDown.TabIndex = 4;
            this.btnMoveImageDown.UseVisualStyleBackColor = true;
            this.btnMoveImageDown.Click += new System.EventHandler(this.btnMoveImageDown_Click);
            // 
            // btnMoveImageUp
            // 
            this.btnMoveImageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveImageUp.Enabled = false;
            this.btnMoveImageUp.Image = global::HLTextureTools.Properties.Resources.arrow_up;
            this.btnMoveImageUp.Location = new System.Drawing.Point(642, 8);
            this.btnMoveImageUp.Name = "btnMoveImageUp";
            this.btnMoveImageUp.Size = new System.Drawing.Size(30, 23);
            this.btnMoveImageUp.TabIndex = 3;
            this.btnMoveImageUp.UseVisualStyleBackColor = true;
            this.btnMoveImageUp.Click += new System.EventHandler(this.btnMoveImageUp_Click);
            // 
            // btnRemoveImages
            // 
            this.btnRemoveImages.Enabled = false;
            this.btnRemoveImages.Image = global::HLTextureTools.Properties.Resources.delete;
            this.btnRemoveImages.Location = new System.Drawing.Point(140, 6);
            this.btnRemoveImages.Name = "btnRemoveImages";
            this.btnRemoveImages.Size = new System.Drawing.Size(128, 25);
            this.btnRemoveImages.TabIndex = 2;
            this.btnRemoveImages.Text = "Remove";
            this.btnRemoveImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemoveImages.UseVisualStyleBackColor = true;
            this.btnRemoveImages.Click += new System.EventHandler(this.btnRemoveImages_Click);
            // 
            // btnAddImages
            // 
            this.btnAddImages.Image = global::HLTextureTools.Properties.Resources.add;
            this.btnAddImages.Location = new System.Drawing.Point(6, 6);
            this.btnAddImages.Name = "btnAddImages";
            this.btnAddImages.Size = new System.Drawing.Size(128, 25);
            this.btnAddImages.TabIndex = 1;
            this.btnAddImages.Text = "Add images";
            this.btnAddImages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddImages.UseVisualStyleBackColor = true;
            this.btnAddImages.Click += new System.EventHandler(this.btnAddImages_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(716, 522);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "2. Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.panelPickTransparentColor);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(700, 111);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image color palette";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Transparent color replacement:";
            // 
            // panelPickTransparentColor
            // 
            this.panelPickTransparentColor.BackColor = System.Drawing.Color.Blue;
            this.panelPickTransparentColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPickTransparentColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelPickTransparentColor.ForeColor = System.Drawing.Color.White;
            this.panelPickTransparentColor.Location = new System.Drawing.Point(233, 57);
            this.panelPickTransparentColor.Name = "panelPickTransparentColor";
            this.panelPickTransparentColor.Size = new System.Drawing.Size(33, 34);
            this.panelPickTransparentColor.TabIndex = 7;
            this.panelPickTransparentColor.Click += new System.EventHandler(this.panelPickTransparentColor_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(32, 26);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(330, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Reserve last color in palette for transparent textures";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.helpBrowser);
            this.tabPage3.Controls.Add(this.progLbl);
            this.tabPage3.Controls.Add(this.progBar);
            this.tabPage3.Controls.Add(this.btnSaveWad);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(716, 522);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "2. Save";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // helpBrowser
            // 
            this.helpBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpBrowser.IsWebBrowserContextMenuEnabled = false;
            this.helpBrowser.Location = new System.Drawing.Point(8, 6);
            this.helpBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.helpBrowser.Name = "helpBrowser";
            this.helpBrowser.Size = new System.Drawing.Size(700, 390);
            this.helpBrowser.TabIndex = 8;
            this.helpBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // progLbl
            // 
            this.progLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progLbl.AutoSize = true;
            this.progLbl.Location = new System.Drawing.Point(8, 414);
            this.progLbl.Name = "progLbl";
            this.progLbl.Size = new System.Drawing.Size(125, 16);
            this.progLbl.TabIndex = 3;
            this.progLbl.Text = "Creating WAD file ...";
            this.progLbl.Visible = false;
            // 
            // progBar
            // 
            this.progBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progBar.Location = new System.Drawing.Point(8, 433);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(700, 23);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progBar.TabIndex = 2;
            this.progBar.Visible = false;
            // 
            // btnSaveWad
            // 
            this.btnSaveWad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveWad.Enabled = false;
            this.btnSaveWad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSaveWad.Location = new System.Drawing.Point(8, 462);
            this.btnSaveWad.Name = "btnSaveWad";
            this.btnSaveWad.Size = new System.Drawing.Size(700, 52);
            this.btnSaveWad.TabIndex = 0;
            this.btnSaveWad.Text = "Save WAD";
            this.btnSaveWad.UseVisualStyleBackColor = true;
            this.btnSaveWad.Click += new System.EventHandler(this.btnSaveWad_Click);
            // 
            // addImagesFileDialog
            // 
            this.addImagesFileDialog.Filter = "Image files|*.bmp;*.png;*.jpg;*.gif;*.tiff|Bitmap files (*.bmp)|*.bmp|PNG files (" +
    "*.png)|*.png|JPEG files (*.jpg)|*.jpg|Gif files (*.gif)|*.gif|Tiff files (*.tiff" +
    ")|*.tiff";
            this.addImagesFileDialog.Multiselect = true;
            // 
            // saveWadFileDialog
            // 
            this.saveWadFileDialog.Filter = "HL WAD Files (*.wad)|*.wad";
            // 
            // colorPicker
            // 
            this.colorPicker.Color = System.Drawing.Color.Blue;
            // 
            // NewWadForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 551);
            this.Controls.Add(this.tabControl1);
            this.Name = "NewWadForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create new texture file ...";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnAddImages;
        private System.Windows.Forms.Button btnRemoveImages;
        private System.Windows.Forms.Button btnMoveImageUp;
        private System.Windows.Forms.Button btnMoveImageDown;
        private System.Windows.Forms.OpenFileDialog addImagesFileDialog;
        private System.Windows.Forms.Button btnSaveWad;
        private System.Windows.Forms.SaveFileDialog saveWadFileDialog;
        private ListViewEx listPictures;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label progLbl;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPickTransparentColor;
        private System.Windows.Forms.WebBrowser helpBrowser;
    }
}