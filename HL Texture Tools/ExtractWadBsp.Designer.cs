namespace HLTextureTools
{
    partial class ExtractWadBsp
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
            this.inputBspFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenBsp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveWad = new System.Windows.Forms.Button();
            this.inputWadFile = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openBspFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveWadFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputBspFile
            // 
            this.inputBspFile.Location = new System.Drawing.Point(32, 38);
            this.inputBspFile.Margin = new System.Windows.Forms.Padding(4);
            this.inputBspFile.Name = "inputBspFile";
            this.inputBspFile.Size = new System.Drawing.Size(417, 22);
            this.inputBspFile.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenBsp);
            this.groupBox1.Controls.Add(this.inputBspFile);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(525, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input *.bsp map file";
            // 
            // btnOpenBsp
            // 
            this.btnOpenBsp.Location = new System.Drawing.Point(459, 36);
            this.btnOpenBsp.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenBsp.Name = "btnOpenBsp";
            this.btnOpenBsp.Size = new System.Drawing.Size(32, 28);
            this.btnOpenBsp.TabIndex = 1;
            this.btnOpenBsp.Text = "...";
            this.btnOpenBsp.UseVisualStyleBackColor = true;
            this.btnOpenBsp.Click += new System.EventHandler(this.btnOpenBsp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSaveWad);
            this.groupBox2.Controls.Add(this.inputWadFile);
            this.groupBox2.Location = new System.Drawing.Point(16, 116);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(525, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output *.wad file to save";
            // 
            // btnSaveWad
            // 
            this.btnSaveWad.Location = new System.Drawing.Point(459, 34);
            this.btnSaveWad.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveWad.Name = "btnSaveWad";
            this.btnSaveWad.Size = new System.Drawing.Size(32, 28);
            this.btnSaveWad.TabIndex = 1;
            this.btnSaveWad.Text = "...";
            this.btnSaveWad.UseVisualStyleBackColor = true;
            this.btnSaveWad.Click += new System.EventHandler(this.btnSaveWad_Click);
            // 
            // inputWadFile
            // 
            this.inputWadFile.Location = new System.Drawing.Point(32, 38);
            this.inputWadFile.Margin = new System.Windows.Forms.Padding(4);
            this.inputWadFile.Name = "inputWadFile";
            this.inputWadFile.Size = new System.Drawing.Size(417, 22);
            this.inputWadFile.TabIndex = 1;
            // 
            // btnExtract
            // 
            this.btnExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExtract.Location = new System.Drawing.Point(441, 230);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(4);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(100, 47);
            this.btnExtract.TabIndex = 2;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 240);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(417, 31);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // openBspFileDialog
            // 
            this.openBspFileDialog.Filter = "BSP Map File (*.bsp)|*.bsp";
            // 
            // saveWadFileDialog
            // 
            this.saveWadFileDialog.Filter = "HL WAD Texture (*.wad)|*.wad";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ExtractWadBsp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 292);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtractWadBsp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extract embedded WAD file(s) from BSP map file";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox inputBspFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox inputWadFile;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnOpenBsp;
        private System.Windows.Forms.Button btnSaveWad;
        private System.Windows.Forms.OpenFileDialog openBspFileDialog;
        private System.Windows.Forms.SaveFileDialog saveWadFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}