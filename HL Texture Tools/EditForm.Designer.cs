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
            this.radioParallelOriented = new System.Windows.Forms.RadioButton();
            this.radioOriented = new System.Windows.Forms.RadioButton();
            this.radioParallel = new System.Windows.Forms.RadioButton();
            this.radioFacingUpright = new System.Windows.Forms.RadioButton();
            this.radioParallelUpright = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioParallelOriented);
            this.groupBox1.Controls.Add(this.radioOriented);
            this.groupBox1.Controls.Add(this.radioParallel);
            this.groupBox1.Controls.Add(this.radioFacingUpright);
            this.groupBox1.Controls.Add(this.radioParallelUpright);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(297, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sprite type";
            // 
            // radioParallelOriented
            // 
            this.radioParallelOriented.AutoSize = true;
            this.radioParallelOriented.Location = new System.Drawing.Point(35, 151);
            this.radioParallelOriented.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioParallelOriented.Name = "radioParallelOriented";
            this.radioParallelOriented.Size = new System.Drawing.Size(126, 20);
            this.radioParallelOriented.TabIndex = 4;
            this.radioParallelOriented.TabStop = true;
            this.radioParallelOriented.Text = "Parallel Oriented";
            this.radioParallelOriented.UseVisualStyleBackColor = true;
            // 
            // radioOriented
            // 
            this.radioOriented.AutoSize = true;
            this.radioOriented.Location = new System.Drawing.Point(35, 122);
            this.radioOriented.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioOriented.Name = "radioOriented";
            this.radioOriented.Size = new System.Drawing.Size(77, 20);
            this.radioOriented.TabIndex = 3;
            this.radioOriented.TabStop = true;
            this.radioOriented.Text = "Oriented";
            this.radioOriented.UseVisualStyleBackColor = true;
            // 
            // radioParallel
            // 
            this.radioParallel.AutoSize = true;
            this.radioParallel.Location = new System.Drawing.Point(35, 92);
            this.radioParallel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioParallel.Name = "radioParallel";
            this.radioParallel.Size = new System.Drawing.Size(72, 20);
            this.radioParallel.TabIndex = 2;
            this.radioParallel.TabStop = true;
            this.radioParallel.Text = "Parallel";
            this.radioParallel.UseVisualStyleBackColor = true;
            // 
            // radioFacingUpright
            // 
            this.radioFacingUpright.AutoSize = true;
            this.radioFacingUpright.Location = new System.Drawing.Point(35, 63);
            this.radioFacingUpright.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioFacingUpright.Name = "radioFacingUpright";
            this.radioFacingUpright.Size = new System.Drawing.Size(113, 20);
            this.radioFacingUpright.TabIndex = 1;
            this.radioFacingUpright.TabStop = true;
            this.radioFacingUpright.Text = "Facing Upright";
            this.radioFacingUpright.UseVisualStyleBackColor = true;
            // 
            // radioParallelUpright
            // 
            this.radioParallelUpright.AutoSize = true;
            this.radioParallelUpright.Location = new System.Drawing.Point(35, 33);
            this.radioParallelUpright.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioParallelUpright.Name = "radioParallelUpright";
            this.radioParallelUpright.Size = new System.Drawing.Size(118, 20);
            this.radioParallelUpright.TabIndex = 0;
            this.radioParallelUpright.TabStop = true;
            this.radioParallelUpright.Text = "Parallel Upright";
            this.radioParallelUpright.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSave.Location = new System.Drawing.Point(16, 235);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(213, 235);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 278);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change sprite type";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
    }
}