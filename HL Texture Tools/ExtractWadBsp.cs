using HLTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class ExtractWadBsp : Form
    {
        public delegate void OpenFileDelegate(string filename);
        private OpenFileDelegate openFileNow;

        struct WorkerData
        {
            public string InputFilename;
            public string OutputFilename;
        }

        public ExtractWadBsp(OpenFileDelegate openFileDlg)
        {
            InitializeComponent();
            this.openFileNow = openFileDlg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = saveFileDialog1.FileName;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0 && textBox2.Text.Trim().Length > 0)
            {
                WorkerData wdata = new WorkerData();
                wdata.InputFilename = textBox1.Text;
                wdata.OutputFilename = textBox2.Text;
                //
                progressBar1.Show();
                button1.Enabled = false;
                backgroundWorker1.RunWorkerAsync(wdata);
            }
            else
            {
                MessageBox.Show("Error, empty filename.");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is WorkerData)
            {
                WorkerData wdata = (WorkerData)e.Argument;
                e.Result = WAD3Loader.ExtractWadFromBsp(wdata.InputFilename, wdata.OutputFilename);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is int)
            {
                int count = (int)e.Result;
                if (count > 0)
                {
                    MessageBox.Show("Extracted " + count + " textures into new WAD!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No embedded textures found in BSP!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
            progressBar1.Hide();
            button1.Enabled = true;

            this.Close();
            if (openFileNow != null && count > 0)
                openFileNow(textBox2.Text);
            }
        }
    }
}
