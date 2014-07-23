using System.Windows.Forms;
namespace HLTextureTools
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractImageItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAllItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asJPEGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asBMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asGIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asTIFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.namesToTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.recFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPaletteToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rotateLItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateRItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgColorItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentBgItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.animationSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animSpeedTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.thumbnailSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x64ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x128ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.associateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewWadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.manageVHETexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractWADFromBSPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixTextureNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeTransparentColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.detTexturesPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDetailFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recommendedTextureSizesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBugsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.runSprToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.paletteStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rlStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.rpStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.pStripButton = new System.Windows.Forms.ToolStripButton();
            this.mStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.prevFileBtn = new System.Windows.Forms.ToolStripButton();
            this.filesBox = new System.Windows.Forms.ToolStripComboBox();
            this.nextFileBtn = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sLbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.totalLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSeperator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sLbl3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.nameLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.sLbl4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.sLbl2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.typeLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.timerAnimate = new System.Windows.Forms.Timer(this.components);
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialogTxt = new System.Windows.Forms.SaveFileDialog();
            this.tlCache = new System.Windows.Forms.ImageList(this.components);
            this.lblRgb = new System.Windows.Forms.Label();
            this.transLbl = new System.Windows.Forms.Label();
            this.openFileDialogDetail = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new HLTextureTools.SplitContainerEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listViewEx1 = new HLTextureTools.ListViewEx();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new HLTextureTools.ListBoxEx();
            this.projectHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Half-Life WAD/SPR files (*.wad;*.spr)|*.wad;*.spr|HL WAD Files (*.wad)|*.wad|HL S" +
    "PR Files (*.spr)|*.spr|All files (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|Gif f" +
    "iles (*.gif)|*.gif|Tiff files (*.tiff)|*.tiff";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(701, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileItem,
            this.extractImageItem,
            this.extractAllItem,
            this.toolStripMenuItem6,
            this.recFiles,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openFileItem
            // 
            this.openFileItem.Name = "openFileItem";
            this.openFileItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileItem.Size = new System.Drawing.Size(155, 22);
            this.openFileItem.Text = "Open...";
            this.openFileItem.Click += new System.EventHandler(this.openFileItem_Click);
            // 
            // extractImageItem
            // 
            this.extractImageItem.Enabled = false;
            this.extractImageItem.Name = "extractImageItem";
            this.extractImageItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.extractImageItem.Size = new System.Drawing.Size(155, 22);
            this.extractImageItem.Text = "Extract";
            this.extractImageItem.Click += new System.EventHandler(this.extractImageItem_Click);
            // 
            // extractAllItem
            // 
            this.extractAllItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asPNGToolStripMenuItem,
            this.asJPEGToolStripMenuItem,
            this.asBMPToolStripMenuItem,
            this.asGIFToolStripMenuItem,
            this.asTIFFToolStripMenuItem,
            this.toolStripMenuItem5,
            this.namesToTXTToolStripMenuItem});
            this.extractAllItem.Enabled = false;
            this.extractAllItem.Name = "extractAllItem";
            this.extractAllItem.Size = new System.Drawing.Size(155, 22);
            this.extractAllItem.Text = "Extract all";
            // 
            // asPNGToolStripMenuItem
            // 
            this.asPNGToolStripMenuItem.Name = "asPNGToolStripMenuItem";
            this.asPNGToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.asPNGToolStripMenuItem.Text = "As PNG ...";
            this.asPNGToolStripMenuItem.Click += new System.EventHandler(this.asPNGToolStripMenuItem_Click);
            // 
            // asJPEGToolStripMenuItem
            // 
            this.asJPEGToolStripMenuItem.Name = "asJPEGToolStripMenuItem";
            this.asJPEGToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.asJPEGToolStripMenuItem.Text = "As JPEG ...";
            this.asJPEGToolStripMenuItem.Click += new System.EventHandler(this.asJPEGToolStripMenuItem_Click);
            // 
            // asBMPToolStripMenuItem
            // 
            this.asBMPToolStripMenuItem.Name = "asBMPToolStripMenuItem";
            this.asBMPToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.asBMPToolStripMenuItem.Text = "As BMP ...";
            this.asBMPToolStripMenuItem.Click += new System.EventHandler(this.asBMPToolStripMenuItem_Click);
            // 
            // asGIFToolStripMenuItem
            // 
            this.asGIFToolStripMenuItem.Name = "asGIFToolStripMenuItem";
            this.asGIFToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.asGIFToolStripMenuItem.Text = "As GIF ...";
            this.asGIFToolStripMenuItem.Click += new System.EventHandler(this.asGIFToolStripMenuItem_Click);
            // 
            // asTIFFToolStripMenuItem
            // 
            this.asTIFFToolStripMenuItem.Name = "asTIFFToolStripMenuItem";
            this.asTIFFToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.asTIFFToolStripMenuItem.Text = "As TIFF ...";
            this.asTIFFToolStripMenuItem.Click += new System.EventHandler(this.asTIFFToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(158, 6);
            // 
            // namesToTXTToolStripMenuItem
            // 
            this.namesToTXTToolStripMenuItem.Name = "namesToTXTToolStripMenuItem";
            this.namesToTXTToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.namesToTXTToolStripMenuItem.Text = "Names to TXT ...";
            this.namesToTXTToolStripMenuItem.Click += new System.EventHandler(this.namesToTXTToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 6);
            // 
            // recFiles
            // 
            this.recFiles.Enabled = false;
            this.recFiles.Name = "recFiles";
            this.recFiles.Size = new System.Drawing.Size(155, 22);
            this.recFiles.Text = "Recent files";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorPaletteToolItem,
            this.animateMenuItem,
            this.toolStripSeparator2,
            this.rotateLItem,
            this.rotateRItem,
            this.toolStripMenuItem3,
            this.copyMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // colorPaletteToolItem
            // 
            this.colorPaletteToolItem.Enabled = false;
            this.colorPaletteToolItem.Name = "colorPaletteToolItem";
            this.colorPaletteToolItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.colorPaletteToolItem.Size = new System.Drawing.Size(203, 22);
            this.colorPaletteToolItem.Text = "Color palette";
            this.colorPaletteToolItem.Click += new System.EventHandler(this.colorPaletteToolItem_Click);
            // 
            // animateMenuItem
            // 
            this.animateMenuItem.Enabled = false;
            this.animateMenuItem.Name = "animateMenuItem";
            this.animateMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.animateMenuItem.Size = new System.Drawing.Size(203, 22);
            this.animateMenuItem.Text = "Animation Play/Stop";
            this.animateMenuItem.Click += new System.EventHandler(this.animateMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
            // 
            // rotateLItem
            // 
            this.rotateLItem.Enabled = false;
            this.rotateLItem.Name = "rotateLItem";
            this.rotateLItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.rotateLItem.Size = new System.Drawing.Size(203, 22);
            this.rotateLItem.Text = "Rotate left";
            this.rotateLItem.Click += new System.EventHandler(this.rotateLItem_Click);
            // 
            // rotateRItem
            // 
            this.rotateRItem.Enabled = false;
            this.rotateRItem.Name = "rotateRItem";
            this.rotateRItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.rotateRItem.Size = new System.Drawing.Size(203, 22);
            this.rotateRItem.Text = "Rotate right";
            this.rotateRItem.Click += new System.EventHandler(this.rotateRItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(200, 6);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Enabled = false;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyMenuItem.Text = "Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bgColorItem,
            this.transparentBgItem,
            this.tileImageToolStripMenuItem,
            this.switchBackgroundToolStripMenuItem,
            this.toolStripMenuItem2,
            this.animationSpeedToolStripMenuItem,
            this.thumbnailSizeToolStripMenuItem,
            this.associateToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // bgColorItem
            // 
            this.bgColorItem.Name = "bgColorItem";
            this.bgColorItem.Size = new System.Drawing.Size(181, 22);
            this.bgColorItem.Text = "Background color";
            this.bgColorItem.Click += new System.EventHandler(this.bgColorItem_Click);
            // 
            // transparentBgItem
            // 
            this.transparentBgItem.Checked = true;
            this.transparentBgItem.CheckOnClick = true;
            this.transparentBgItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transparentBgItem.Name = "transparentBgItem";
            this.transparentBgItem.Size = new System.Drawing.Size(181, 22);
            this.transparentBgItem.Text = "Transparent textures";
            this.transparentBgItem.Click += new System.EventHandler(this.transparentBgItem_Click);
            // 
            // tileImageToolStripMenuItem
            // 
            this.tileImageToolStripMenuItem.CheckOnClick = true;
            this.tileImageToolStripMenuItem.Name = "tileImageToolStripMenuItem";
            this.tileImageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.tileImageToolStripMenuItem.Text = "Tile image";
            this.tileImageToolStripMenuItem.Click += new System.EventHandler(this.tileImageToolStripMenuItem_Click);
            // 
            // switchBackgroundToolStripMenuItem
            // 
            this.switchBackgroundToolStripMenuItem.Name = "switchBackgroundToolStripMenuItem";
            this.switchBackgroundToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.switchBackgroundToolStripMenuItem.Text = "Switch background";
            this.switchBackgroundToolStripMenuItem.Click += new System.EventHandler(this.switchBackgroundToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 6);
            // 
            // animationSpeedToolStripMenuItem
            // 
            this.animationSpeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animSpeedTextBox});
            this.animationSpeedToolStripMenuItem.Name = "animationSpeedToolStripMenuItem";
            this.animationSpeedToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.animationSpeedToolStripMenuItem.Text = "Animation speed";
            // 
            // animSpeedTextBox
            // 
            this.animSpeedTextBox.Name = "animSpeedTextBox";
            this.animSpeedTextBox.Size = new System.Drawing.Size(100, 23);
            this.animSpeedTextBox.Text = "100";
            this.animSpeedTextBox.TextChanged += new System.EventHandler(this.animSpeedTextBox_TextChanged);
            // 
            // thumbnailSizeToolStripMenuItem
            // 
            this.thumbnailSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x32ToolStripMenuItem,
            this.x64ToolStripMenuItem,
            this.x128ToolStripMenuItem});
            this.thumbnailSizeToolStripMenuItem.Name = "thumbnailSizeToolStripMenuItem";
            this.thumbnailSizeToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.thumbnailSizeToolStripMenuItem.Text = "Thumbnail size";
            // 
            // x32ToolStripMenuItem
            // 
            this.x32ToolStripMenuItem.CheckOnClick = true;
            this.x32ToolStripMenuItem.Name = "x32ToolStripMenuItem";
            this.x32ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.x32ToolStripMenuItem.Text = "32x32";
            this.x32ToolStripMenuItem.Click += new System.EventHandler(this.x32ToolStripMenuItem_Click);
            // 
            // x64ToolStripMenuItem
            // 
            this.x64ToolStripMenuItem.Checked = true;
            this.x64ToolStripMenuItem.CheckOnClick = true;
            this.x64ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x64ToolStripMenuItem.Name = "x64ToolStripMenuItem";
            this.x64ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.x64ToolStripMenuItem.Text = "64x64";
            this.x64ToolStripMenuItem.Click += new System.EventHandler(this.x64ToolStripMenuItem_Click);
            // 
            // x128ToolStripMenuItem
            // 
            this.x128ToolStripMenuItem.CheckOnClick = true;
            this.x128ToolStripMenuItem.Name = "x128ToolStripMenuItem";
            this.x128ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.x128ToolStripMenuItem.Text = "128x128";
            this.x128ToolStripMenuItem.Click += new System.EventHandler(this.x128ToolStripMenuItem_Click);
            // 
            // associateToolStripMenuItem
            // 
            this.associateToolStripMenuItem.Name = "associateToolStripMenuItem";
            this.associateToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.associateToolStripMenuItem.Text = "Associate files ...";
            this.associateToolStripMenuItem.Click += new System.EventHandler(this.associateToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewWadToolStripMenuItem,
            this.createNewSpriteToolStripMenuItem,
            this.toolStripMenuItem8,
            this.manageVHETexturesToolStripMenuItem,
            this.extractWADFromBSPToolStripMenuItem,
            this.toolStripMenuItem4,
            this.editToolStripMenuItem,
            this.fixTextureNameToolStripMenuItem,
            this.makeTransparentColorToolStripMenuItem,
            this.toolStripMenuItem9,
            this.detTexturesPrev});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.toolsToolStripMenuItem_DropDownOpening);
            // 
            // createNewWadToolStripMenuItem
            // 
            this.createNewWadToolStripMenuItem.Name = "createNewWadToolStripMenuItem";
            this.createNewWadToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.createNewWadToolStripMenuItem.Text = "Create new Wad ...";
            this.createNewWadToolStripMenuItem.Click += new System.EventHandler(this.createNewWadToolStripMenuItem_Click);
            // 
            // createNewSpriteToolStripMenuItem
            // 
            this.createNewSpriteToolStripMenuItem.Name = "createNewSpriteToolStripMenuItem";
            this.createNewSpriteToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.createNewSpriteToolStripMenuItem.Text = "Create new Sprite ...";
            this.createNewSpriteToolStripMenuItem.Click += new System.EventHandler(this.createNewSpriteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(230, 6);
            // 
            // manageVHETexturesToolStripMenuItem
            // 
            this.manageVHETexturesToolStripMenuItem.Enabled = false;
            this.manageVHETexturesToolStripMenuItem.Name = "manageVHETexturesToolStripMenuItem";
            this.manageVHETexturesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.manageVHETexturesToolStripMenuItem.Text = "Manage VHE textures";
            this.manageVHETexturesToolStripMenuItem.Click += new System.EventHandler(this.manageVHETexturesToolStripMenuItem_Click);
            // 
            // extractWADFromBSPToolStripMenuItem
            // 
            this.extractWADFromBSPToolStripMenuItem.Name = "extractWADFromBSPToolStripMenuItem";
            this.extractWADFromBSPToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.extractWADFromBSPToolStripMenuItem.Text = "Extract WAD from BSP";
            this.extractWADFromBSPToolStripMenuItem.Click += new System.EventHandler(this.extractWADFromBSPToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(230, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.editToolStripMenuItem.Text = "Fix sprite type";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // fixTextureNameToolStripMenuItem
            // 
            this.fixTextureNameToolStripMenuItem.Enabled = false;
            this.fixTextureNameToolStripMenuItem.Name = "fixTextureNameToolStripMenuItem";
            this.fixTextureNameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.fixTextureNameToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.fixTextureNameToolStripMenuItem.Text = "Fix texture name";
            this.fixTextureNameToolStripMenuItem.Click += new System.EventHandler(this.fixTextureNameToolStripMenuItem_Click);
            // 
            // makeTransparentColorToolStripMenuItem
            // 
            this.makeTransparentColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectColorToolStripMenuItem,
            this.doBackupToolStripMenuItem});
            this.makeTransparentColorToolStripMenuItem.Enabled = false;
            this.makeTransparentColorToolStripMenuItem.Name = "makeTransparentColorToolStripMenuItem";
            this.makeTransparentColorToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.makeTransparentColorToolStripMenuItem.Text = "Make transparent background";
            // 
            // selectColorToolStripMenuItem
            // 
            this.selectColorToolStripMenuItem.Name = "selectColorToolStripMenuItem";
            this.selectColorToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.selectColorToolStripMenuItem.Text = "Select color";
            this.selectColorToolStripMenuItem.Click += new System.EventHandler(this.selectColorToolStripMenuItem_Click);
            // 
            // doBackupToolStripMenuItem
            // 
            this.doBackupToolStripMenuItem.Checked = true;
            this.doBackupToolStripMenuItem.CheckOnClick = true;
            this.doBackupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.doBackupToolStripMenuItem.Name = "doBackupToolStripMenuItem";
            this.doBackupToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.doBackupToolStripMenuItem.Text = "Do backup";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(230, 6);
            this.toolStripMenuItem9.Visible = false;
            // 
            // detTexturesPrev
            // 
            this.detTexturesPrev.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDetailFileToolStripMenuItem});
            this.detTexturesPrev.Name = "detTexturesPrev";
            this.detTexturesPrev.Size = new System.Drawing.Size(233, 22);
            this.detTexturesPrev.Text = "Detail textures preview";
            this.detTexturesPrev.Visible = false;
            // 
            // loadDetailFileToolStripMenuItem
            // 
            this.loadDetailFileToolStripMenuItem.Name = "loadDetailFileToolStripMenuItem";
            this.loadDetailFileToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.loadDetailFileToolStripMenuItem.Text = "Load detail file...";
            this.loadDetailFileToolStripMenuItem.Click += new System.EventHandler(this.loadDetailFileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recommendedTextureSizesToolStripMenuItem,
            this.reportBugsToolStripMenuItem,
            this.projectHomepageToolStripMenuItem,
            this.toolStripMenuItem7,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // recommendedTextureSizesToolStripMenuItem
            // 
            this.recommendedTextureSizesToolStripMenuItem.Name = "recommendedTextureSizesToolStripMenuItem";
            this.recommendedTextureSizesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.recommendedTextureSizesToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.recommendedTextureSizesToolStripMenuItem.Text = "Recommended Texture Sizes";
            this.recommendedTextureSizesToolStripMenuItem.Click += new System.EventHandler(this.recommendedTextureSizesToolStripMenuItem_Click);
            // 
            // reportBugsToolStripMenuItem
            // 
            this.reportBugsToolStripMenuItem.Name = "reportBugsToolStripMenuItem";
            this.reportBugsToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.reportBugsToolStripMenuItem.Text = "Report bugs, feature request, updates";
            this.reportBugsToolStripMenuItem.Click += new System.EventHandler(this.reportBugsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(268, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.runSprToolButton,
            this.toolStripSeparator,
            this.paletteStripButton,
            this.copyToolStripButton,
            this.toolStripSeparator3,
            this.rlStripButton1,
            this.rpStripButton1,
            this.toolStripSeparator4,
            this.pStripButton,
            this.mStripButton,
            this.toolStripButton2,
            this.toolStripSeparator5,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.prevFileBtn,
            this.filesBox,
            this.nextFileBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(701, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::HLTextureTools.Properties.Resources.folder_picture;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openFileItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = global::HLTextureTools.Properties.Resources.picture_save;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Extract";
            this.saveToolStripButton.Click += new System.EventHandler(this.extractImageItem_Click);
            // 
            // runSprToolButton
            // 
            this.runSprToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runSprToolButton.Enabled = false;
            this.runSprToolButton.Image = global::HLTextureTools.Properties.Resources.control_play_blue;
            this.runSprToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runSprToolButton.Name = "runSprToolButton";
            this.runSprToolButton.Size = new System.Drawing.Size(23, 22);
            this.runSprToolButton.Text = "Animation Play/Stop";
            this.runSprToolButton.Click += new System.EventHandler(this.animateMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // paletteStripButton
            // 
            this.paletteStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.paletteStripButton.Enabled = false;
            this.paletteStripButton.Image = global::HLTextureTools.Properties.Resources.palette;
            this.paletteStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paletteStripButton.Name = "paletteStripButton";
            this.paletteStripButton.Size = new System.Drawing.Size(23, 22);
            this.paletteStripButton.Text = "Color palette";
            this.paletteStripButton.Click += new System.EventHandler(this.colorPaletteToolItem_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Enabled = false;
            this.copyToolStripButton.Image = global::HLTextureTools.Properties.Resources.page_copy;
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // rlStripButton1
            // 
            this.rlStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rlStripButton1.Enabled = false;
            this.rlStripButton1.Image = global::HLTextureTools.Properties.Resources.shape_rotate_anticlockwise;
            this.rlStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rlStripButton1.Name = "rlStripButton1";
            this.rlStripButton1.Size = new System.Drawing.Size(23, 22);
            this.rlStripButton1.Text = "Rotate left";
            this.rlStripButton1.Click += new System.EventHandler(this.rotateLItem_Click);
            // 
            // rpStripButton1
            // 
            this.rpStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rpStripButton1.Enabled = false;
            this.rpStripButton1.Image = global::HLTextureTools.Properties.Resources.shape_rotate_clockwise;
            this.rpStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rpStripButton1.Name = "rpStripButton1";
            this.rpStripButton1.Size = new System.Drawing.Size(23, 22);
            this.rpStripButton1.Text = "Rotate right";
            this.rpStripButton1.Click += new System.EventHandler(this.rotateRItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // pStripButton
            // 
            this.pStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pStripButton.Enabled = false;
            this.pStripButton.Image = global::HLTextureTools.Properties.Resources.zoom_in;
            this.pStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pStripButton.Name = "pStripButton";
            this.pStripButton.Size = new System.Drawing.Size(23, 22);
            this.pStripButton.Text = "Zoom in (ctrl +)";
            this.pStripButton.Click += new System.EventHandler(this.pStripButton_Click);
            // 
            // mStripButton
            // 
            this.mStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mStripButton.Enabled = false;
            this.mStripButton.Image = global::HLTextureTools.Properties.Resources.zoom_out;
            this.mStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mStripButton.Name = "mStripButton";
            this.mStripButton.Size = new System.Drawing.Size(23, 22);
            this.mStripButton.Text = "Zoom out (ctrl -)";
            this.mStripButton.Click += new System.EventHandler(this.mStripButton_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = global::HLTextureTools.Properties.Resources._11;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Reset zooming";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.CheckOnClick = true;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = global::HLTextureTools.Properties.Resources.application_view_tile;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Tile View";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // prevFileBtn
            // 
            this.prevFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.prevFileBtn.Enabled = false;
            this.prevFileBtn.Image = global::HLTextureTools.Properties.Resources.resultset_previous;
            this.prevFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevFileBtn.Name = "prevFileBtn";
            this.prevFileBtn.Size = new System.Drawing.Size(23, 22);
            this.prevFileBtn.Text = "Previous file";
            this.prevFileBtn.Click += new System.EventHandler(this.prevFileBtn_Click);
            // 
            // filesBox
            // 
            this.filesBox.Enabled = false;
            this.filesBox.Name = "filesBox";
            this.filesBox.Size = new System.Drawing.Size(121, 25);
            this.filesBox.SelectedIndexChanged += new System.EventHandler(this.filesBox_SelectedIndexChanged);
            // 
            // nextFileBtn
            // 
            this.nextFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextFileBtn.Enabled = false;
            this.nextFileBtn.Image = global::HLTextureTools.Properties.Resources.resultset_next;
            this.nextFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextFileBtn.Name = "nextFileBtn";
            this.nextFileBtn.Size = new System.Drawing.Size(23, 22);
            this.nextFileBtn.Text = "Next file";
            this.nextFileBtn.Click += new System.EventHandler(this.nextFileBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sLbl1,
            this.totalLbl,
            this.statusSeperator1,
            this.sLbl3,
            this.nameLbl,
            this.sLbl4,
            this.sizeLbl,
            this.sLbl2,
            this.typeLbl,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 392);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(701, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sLbl1
            // 
            this.sLbl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sLbl1.Name = "sLbl1";
            this.sLbl1.Size = new System.Drawing.Size(43, 17);
            this.sLbl1.Text = "Count:";
            this.sLbl1.Visible = false;
            // 
            // totalLbl
            // 
            this.totalLbl.Name = "totalLbl";
            this.totalLbl.Size = new System.Drawing.Size(13, 17);
            this.totalLbl.Text = "0";
            this.totalLbl.Visible = false;
            // 
            // statusSeperator1
            // 
            this.statusSeperator1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusSeperator1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusSeperator1.Name = "statusSeperator1";
            this.statusSeperator1.Size = new System.Drawing.Size(4, 17);
            // 
            // sLbl3
            // 
            this.sLbl3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sLbl3.Name = "sLbl3";
            this.sLbl3.Size = new System.Drawing.Size(43, 17);
            this.sLbl3.Text = "Name:";
            this.sLbl3.Visible = false;
            // 
            // nameLbl
            // 
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(12, 17);
            this.nameLbl.Text = "-";
            this.nameLbl.Visible = false;
            // 
            // sLbl4
            // 
            this.sLbl4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sLbl4.Name = "sLbl4";
            this.sLbl4.Size = new System.Drawing.Size(33, 17);
            this.sLbl4.Text = "Size:";
            this.sLbl4.Visible = false;
            // 
            // sizeLbl
            // 
            this.sizeLbl.Name = "sizeLbl";
            this.sizeLbl.Size = new System.Drawing.Size(12, 17);
            this.sizeLbl.Text = "-";
            this.sizeLbl.Visible = false;
            // 
            // sLbl2
            // 
            this.sLbl2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sLbl2.Name = "sLbl2";
            this.sLbl2.Size = new System.Drawing.Size(37, 17);
            this.sLbl2.Text = "Type:";
            this.sLbl2.Visible = false;
            // 
            // typeLbl
            // 
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(12, 17);
            this.typeLbl.Text = "-";
            this.typeLbl.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Visible = false;
            // 
            // timerAnimate
            // 
            this.timerAnimate.Tick += new System.EventHandler(this.timerAnimate_Tick);
            // 
            // folderDialog
            // 
            this.folderDialog.Description = "Select directory";
            // 
            // saveFileDialogTxt
            // 
            this.saveFileDialogTxt.Filter = "Text files (*.txt)|*.txt";
            // 
            // tlCache
            // 
            this.tlCache.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.tlCache.ImageSize = new System.Drawing.Size(64, 64);
            this.tlCache.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblRgb
            // 
            this.lblRgb.BackColor = System.Drawing.Color.White;
            this.lblRgb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRgb.Location = new System.Drawing.Point(24, 70);
            this.lblRgb.Name = "lblRgb";
            this.lblRgb.Size = new System.Drawing.Size(18, 15);
            this.lblRgb.TabIndex = 3;
            this.lblRgb.Visible = false;
            // 
            // transLbl
            // 
            this.transLbl.AutoSize = true;
            this.transLbl.BackColor = System.Drawing.Color.White;
            this.transLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.transLbl.Location = new System.Drawing.Point(47, 70);
            this.transLbl.Name = "transLbl";
            this.transLbl.Size = new System.Drawing.Size(39, 15);
            this.transLbl.TabIndex = 2;
            this.transLbl.Text = "RGB: ";
            this.transLbl.Visible = false;
            // 
            // openFileDialogDetail
            // 
            this.openFileDialogDetail.Filter = "Detail definition File (*.txt)|*.txt";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10, 10, 2, 10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.listViewEx1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(4, 30, 10, 10);
            this.splitContainer1.Size = new System.Drawing.Size(701, 343);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 323);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(273, 319);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // listViewEx1
            // 
            this.listViewEx1.AllowDrop = true;
            this.listViewEx1.AllowRowReorder = false;
            this.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEx1.HideSelection = false;
            this.listViewEx1.LargeImageList = this.tlCache;
            this.listViewEx1.Location = new System.Drawing.Point(4, 30);
            this.listViewEx1.MultiSelect = false;
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(392, 303);
            this.listViewEx1.TabIndex = 5;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.VirtualListSize = 50;
            this.listViewEx1.VirtualMode = true;
            this.listViewEx1.Visible = false;
            this.listViewEx1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewEx1_RetrieveVirtualItem);
            this.listViewEx1.SelectedIndexChanged += new System.EventHandler(this.listViewEx1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.ForeColor = System.Drawing.Color.Silver;
            this.textBox1.Location = new System.Drawing.Point(4, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(392, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Search";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(4, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(392, 303);
            this.listBox1.TabIndex = 3;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
            // 
            // projectHomepageToolStripMenuItem
            // 
            this.projectHomepageToolStripMenuItem.Name = "projectHomepageToolStripMenuItem";
            this.projectHomepageToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.projectHomepageToolStripMenuItem.Text = "Project homepage (GitHub)";
            this.projectHomepageToolStripMenuItem.Click += new System.EventHandler(this.projectHomepageToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 414);
            this.Controls.Add(this.lblRgb);
            this.Controls.Add(this.transLbl);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(385, 256);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Half-Life Texture Tools ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private ListBoxEx listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileItem;
        private System.Windows.Forms.ToolStripMenuItem extractImageItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bgColorItem;
        private System.Windows.Forms.ToolStripMenuItem transparentBgItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorPaletteToolItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tileImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton runSprToolButton;
        private System.Windows.Forms.Timer timerAnimate;
        private System.Windows.Forms.ToolStripMenuItem animationSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox animSpeedTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox filesBox;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractAllItem;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.ToolStripMenuItem asPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asJPEGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asBMPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asGIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asTIFFToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel totalLbl;
        private System.Windows.Forms.ToolStripStatusLabel statusSeperator1;
        private System.Windows.Forms.ToolStripStatusLabel sLbl1;
        private System.Windows.Forms.ToolStripStatusLabel sLbl2;
        private System.Windows.Forms.ToolStripStatusLabel typeLbl;
        private System.Windows.Forms.ToolStripStatusLabel sLbl3;
        private System.Windows.Forms.ToolStripStatusLabel nameLbl;
        private System.Windows.Forms.ToolStripStatusLabel sLbl4;
        private System.Windows.Forms.ToolStripStatusLabel sizeLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem rotateLItem;
        private System.Windows.Forms.ToolStripMenuItem rotateRItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton rlStripButton1;
        private System.Windows.Forms.ToolStripButton rpStripButton1;
        private System.Windows.Forms.ToolStripButton paletteStripButton;
        private System.Windows.Forms.ToolStripMenuItem recFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem namesToTXTToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogTxt;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton mStripButton;
        private System.Windows.Forms.ToolStripButton pStripButton;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem makeTransparentColorToolStripMenuItem;
        private System.Windows.Forms.Label transLbl;
        private System.Windows.Forms.Label lblRgb;
        private System.Windows.Forms.ToolStripMenuItem selectColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixTextureNameToolStripMenuItem;
        private ToolStripMenuItem createNewWadToolStripMenuItem;
        private SplitContainerEx splitContainer1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton toolStripButton1;
        private ListViewEx listViewEx1;
        private ImageList tlCache;
        private ToolStripSeparator toolStripMenuItem7;
        private ToolStripMenuItem recommendedTextureSizesToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem8;
        private ToolStripMenuItem manageVHETexturesToolStripMenuItem;
        private ToolStripButton prevFileBtn;
        private ToolStripButton nextFileBtn;
        private ToolStripMenuItem extractWADFromBSPToolStripMenuItem;
        private ToolStripMenuItem associateToolStripMenuItem;
        private ToolStripMenuItem reportBugsToolStripMenuItem;
        private ToolStripMenuItem thumbnailSizeToolStripMenuItem;
        private ToolStripMenuItem x64ToolStripMenuItem;
        private ToolStripMenuItem x128ToolStripMenuItem;
        private ToolStripMenuItem x32ToolStripMenuItem;
        private ToolStripButton toolStripButton2;
        private ToolStripMenuItem switchBackgroundToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem9;
        private ToolStripMenuItem detTexturesPrev;
        private ToolStripMenuItem loadDetailFileToolStripMenuItem;
        private OpenFileDialog openFileDialogDetail;
        private ToolStripMenuItem projectHomepageToolStripMenuItem;
    }
}

