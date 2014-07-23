using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace HLTextureTools
{
    class UAC
    {
        [DllImport("user32")]
        private static extern UInt32 SendMessage
            (IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        private const int BCM_FIRST = 0x1600; //Normal button
        private const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

        public static void SetShieldIcon(Control ctrl)
        {
            SendMessage(ctrl.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }

        public static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool RestartElevated(string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = args;
            try
            {
                Process p = Process.Start(startInfo);
                Application.Exit();
                return true;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return false;
            }

            
        }
    }
}
