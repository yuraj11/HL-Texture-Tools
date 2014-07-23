using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HLTextureTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!FreeImageAPI.FreeImage.IsAvailable())
            {
                MessageBox.Show("FreeImage.dll seems to be missing. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
