using Microsoft.Win32;
using System;

namespace HLTextureTools
{
    /// <summary>
    /// Valve Hammer Editor 3.4+ WAD manager.
    /// </summary>
    class VheWadManager : IDisposable
    {
        private const string VheRegistryPath = @"Software\Valve\Valve Hammer Editor\General";
        private const string TextureFileCount = "TextureFileCount";
        private readonly RegistryKey regKey;

        public VheWadManager()
        {
            regKey = Registry.CurrentUser.OpenSubKey(VheRegistryPath, true);
        }

        /// <summary>
        /// Get list of currently set WADs in VHE.
        /// </summary>
        /// <returns>Returns array of WADs (full paths).</returns>
        public string[] GetWadList()
        {
            int count = GetWadCount();
            string[] wads = new string[count];
            for (int i = 0; i < count; i++)
            {
                wads[i] = (string)regKey.GetValue("TextureFile" + i);
            }

            return wads;
        }

        /// <summary>
        /// Save list of WADs.
        /// </summary>
        /// <param name="items">All WADs that will be visible in VHE.</param>
        public void SaveWadList(string[] items)
        {
            //Remove previous
            int prevWadCount = GetWadCount();
            for (int i = 0; i < prevWadCount; i++)
            {
                regKey.DeleteValue("TextureFile" + i);
            }
            regKey.SetValue(TextureFileCount, items.Length);
            for (int i = 0; i < items.Length; i++)
            {
                regKey.SetValue("TextureFile" + i, items[i]);
            }
        }

        /// <summary>
        /// Get count of WADs.
        /// </summary>
        /// <returns></returns>
        public int GetWadCount()
        {
            return (int)regKey.GetValue(TextureFileCount);
        }

        public void Dispose()
        {
            regKey.Close();
        }

        /// <summary>
        /// Check if VHE is installed.
        /// </summary>
        /// <returns>Returns true if installed.</returns>
        public static bool IsVheInstalled()
        {
            return Registry.GetValue(@"HKEY_CURRENT_USER\" + VheRegistryPath, "TextureFileCount", null) != null;
        }
    }
}
