namespace HLTextureTools
{
    partial class EditForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioParallelUpright = new System.Windows.Forms.RadioButton();
            this.radioParallelOriented = new System.Windows.Forms.RadioButton();
            this.radioFacingUpright = new System.Windows.Forms.RadioButton();
            this.radioOriented = new System.Windows.Forms.RadioButton();
            this.radioParallel = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(321, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sprite type";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.radioParallelUpright, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioParallelOriented, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.radioFacingUpright, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioOriented, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioParallel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(313, 192);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // radioParallelUpright
            // 
            this.radioParallelUpright.AutoSize = true;
            this.radioParallelUpright.Location = new System.Drawing.Point(20, 20);
            this.radioParallelUpright.Margin = new System.Windows.Forms.Padding(4);
            this.radioParallelUpright.Name = "radioParallelUpright";
            this.radioParallelUpright.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.radioParallelUpright.Size = new System.Drawing.Size(117, 24);
            this.radioParallelUpright.TabIndex = 0;
            this.radioParallelUpright.TabStop = true;
            this.radioParallelUpright.Text = "Parallel Upright";
            this.radioParallelUpright.UseVisualStyleBackColor = true;
            // 
            // radioParallelOriented
            // 
            this.radioParallelOriented.AutoSize = true;
            this.radioParallelOriented.Location = new System.Drawing.Point(20, 148);
            this.radioParallelOriented.Margin = new System.Windows.Forms.Padding(4);
            this.radioParallelOriented.MaximumSize = new System.Drawing.Size(250, 0);
            this.radioParallelOriented.Name = "radioParallelOriented";
            this.radioParallelOriented.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.radioParallelOriented.Size = new System.Drawing.Size(125, 24);
            this.radioParallelOriented.TabIndex = 4;
            this.radioParallelOriented.TabStop = true;
            this.radioParallelOriented.Text = "Parallel Oriented";
            this.radioParallelOriented.UseVisualStyleBackColor = true;
            // 
            // radioFacingUpright
            // 
            this.radioFacingUpright.AutoSize = true;
            this.radioFacingUpright.Location = new System.Drawing.Point(20, 52);
            this.radioFacingUpright.Margin = new System.Windows.Forms.Padding(4);
            this.radioFacingUpright.Name = "radioFacingUpright";
            this.radioFacingUpright.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.radioFacingUpright.Size = new System.Drawing.Size(112, 24);
            this.radioFacingUpright.TabIndex = 1;
            this.radioFacingUpright.TabStop = true;
            this.radioFacingUpright.Text = "Facing Upright";
            this.radioFacingUpright.UseVisualStyleBackColor = true;
            // 
            // radioOriented
            // 
            this.radioOriented.AutoSize = true;
            this.radioOriented.Location = new System.Drawing.Point(20, 116);
            this.radioOriented.Margin = new System.Windows.Forms.Padding(4);
            this.radioOriented.Name = "radioOriented";
            this.radioOriented.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.radioOriented.Size = new System.Drawing.Size(76, 24);
            this.radioOriented.TabIndex = 3;
            this.radioOriented.TabStop = true;
            this.radioOriented.Text = "Oriented";
            this.radioOriented.UseVisualStyleBackColor = true;
            // 
            // radioParallel
            // 
            this.radioParallel.AutoSize = true;
            this.radioParallel.Location = new System.Drawing.Point(20, 84);
            this.radioParallel.Margin = new System.Windows.Forms.Padding(4);
            this.radioParallel.Name = "radioParallel";
            this.radioParallel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.radioParallel.Size = new System.Drawing.Size(71, 24);
            this.radioParallel.TabIndex = 2;
            this.radioParallel.TabStop = true;
            this.radioParallel.Text = "Parallel";
            this.radioParallel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSave.Location = new System.Drawing.Point(4, 227);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(225, 227);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(329, 264);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // EditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(345, 280);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change sprite type";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioOriented;
        private System.Windows.Forms.RadioButton radioParallel;
        private System.Windows.Forms.RadioButton radioFacingUpright;
        private System.Windows.Forms.RadioButton radioParallelUpright;
        private System.Windows.Forms.RadioButton radioParallelOriented;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}