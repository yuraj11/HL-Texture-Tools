namespace HLTextureTools
{
    partial class VheManager
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
            this.btnAddTextures = new System.Windows.Forms.Button();
            this.btnRemoveTextures = new System.Windows.Forms.Button();
            this.addWadMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.currentWADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listTextures = new HLTextureTools.ListBoxEx();
            this.addWadMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddTextures
            // 
            this.btnAddTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTextures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAddTextures.Image = global::HLTextureTools.Properties.Resources.add;
            this.btnAddTextures.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddTextures.Location = new System.Drawing.Point(167, 283);
            this.btnAddTextures.Name = "btnAddTextures";
            this.btnAddTextures.Size = new System.Drawing.Size(106, 26);
            this.btnAddTextures.TabIndex = 1;
            this.btnAddTextures.Text = "Add";
            this.btnAddTextures.UseVisualStyleBackColor = true;
            this.btnAddTextures.Click += new System.EventHandler(this.btnAddTextures_Click);
            // 
            // btnRemoveTextures
            // 
            this.btnRemoveTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTextures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemoveTextures.Enabled = false;
            this.btnRemoveTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnRemoveTextures.Image = global::HLTextureTools.Properties.Resources.delete;
            this.btnRemoveTextures.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveTextures.Location = new System.Drawing.Point(279, 283);
            this.btnRemoveTextures.Name = "btnRemoveTextures";
            this.btnRemoveTextures.Size = new System.Drawing.Size(106, 26);
            this.btnRemoveTextures.TabIndex = 1;
            this.btnRemoveTextures.Text = "Remove";
            this.btnRemoveTextures.UseVisualStyleBackColor = true;
            this.btnRemoveTextures.Click += new System.EventHandler(this.btnRemoveTextures_Click);
            // 
            // addWadMenu
            // 
            this.addWadMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentWADToolStripMenuItem,
            this.fromFileToolStripMenuItem});
            this.addWadMenu.Name = "addWadMenu";
            this.addWadMenu.Size = new System.Drawing.Size(145, 48);
            // 
            // currentWADToolStripMenuItem
            // 
            this.currentWADToolStripMenuItem.Name = "currentWADToolStripMenuItem";
            this.currentWADToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.currentWADToolStripMenuItem.Text = "Current WAD";
            this.currentWADToolStripMenuItem.Click += new System.EventHandler(this.currentWADToolStripMenuItem_Click);
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fromFileToolStripMenuItem.Text = "From file ...";
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
            // 
            // openWadFileDialog
            // 
            this.openWadFileDialog.Filter = "WAD Texture Files (*.wad)|*.wad";
            this.openWadFileDialog.Multiselect = true;
            this.openWadFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openWadFileDialog_FileOk);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAll.Enabled = false;
            this.btnRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnRemoveAll.Location = new System.Drawing.Point(391, 283);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(106, 26);
            this.btnRemoveAll.TabIndex = 3;
            this.btnRemoveAll.Text = "Remove all";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Double click on item to open in viewer.";
            // 
            // listTextures
            // 
            this.listTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTextures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listTextures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listTextures.ItemHeight = 15;
            this.listTextures.Location = new System.Drawing.Point(14, 15);
            this.listTextures.Name = "listTextures";
            this.listTextures.ScrollAlwaysVisible = true;
            this.listTextures.Size = new System.Drawing.Size(482, 242);
            this.listTextures.TabIndex = 0;
            this.listTextures.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listTextures_DrawItem);
            this.listTextures.SelectedIndexChanged += new System.EventHandler(this.listTextures_SelectedIndexChanged);
            this.listTextures.DoubleClick += new System.EventHandler(this.listTextures_DoubleClick);
            this.listTextures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listTextures_KeyDown);
            // 
            // VheManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 324);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveTextures);
            this.Controls.Add(this.btnAddTextures);
            this.Controls.Add(this.listTextures);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "VheManager";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Valve Hammer Editor - Textures Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VheManager_FormClosing);
            this.Load += new System.EventHandler(this.VheManager_Load);
            this.addWadMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBoxEx listTextures;
        private System.Windows.Forms.Button btnAddTextures;
        private System.Windows.Forms.Button btnRemoveTextures;
        private System.Windows.Forms.ContextMenuStrip addWadMenu;
        private System.Windows.Forms.ToolStripMenuItem currentWADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openWadFileDialog;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Label label1;
    }
}