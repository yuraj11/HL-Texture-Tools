using HLTools;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HLTextureTools
{
    public partial class ExtractWadBsp : Form
    {
        private readonly OpenFileDelegate openFileNow;

        struct WorkerData
        {
            public string InputFilename;
            public string OutputFilename;
        }

        public ExtractWadBsp(OpenFileDelegate openFileDlg)
        {
            InitializeComponent();
            openFileNow = openFileDlg;
        }

        private void btnOpenBsp_Click(object sender, EventArgs e)
        {
            if (openBspFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputBspFile.Text = openBspFileDialog.FileName;
            }
        }

        private void btnSaveWad_Click(object sender, EventArgs e)
        {
            if (saveWadFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputWadFile.Text = saveWadFileDialog.FileName;
            }
        }
        
        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (inputBspFile.Text.Trim().Length > 0 && inputWadFile.Text.Trim().Length > 0)
            {
                WorkerData wdata = new WorkerData
                {
                    InputFilename = inputBspFile.Text,
                    OutputFilename = inputWadFile.Text
                };

                progressBar1.Show();
                btnExtract.Enabled = false;
                backgroundWorker.RunWorkerAsync(wdata);
            }
            else
            {
                MessageBox.Show("Error, empty filename.");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is WorkerData wdata)
            {
                e.Result = WAD3Loader.ExtractWadFromBsp(wdata.InputFilename, wdata.OutputFilename);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is int count)
            {
                if (count > 0)
                {
                    MessageBox.Show("Extracted " + count + " textures into new WAD!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No embedded textures found in BSP!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                progressBar1.Hide();
                btnExtract.Enabled = true;

                Close();

                if (count > 0)
                {
                    openFileNow(inputWadFile.Text);
                }
            }
        }
    }
}
