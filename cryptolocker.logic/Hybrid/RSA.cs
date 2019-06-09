using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLocker.Logic.Hybrid
{
    /// <summary>
    /// 1. From http://www.csharpdeveloping.net/Snippet/how_to_encrypt_decrypt_using_asymmetric_algorithm_rsa
    /// An asymmetric algorithm to publically encrypt and privately decrypt text data.
    /// 
    /// Pattern:
    /// Encryption:
    /// i.Generate a random key of the length required for symmetrical encryption technique such as AES/Rijndael or similar.
    /// ii.Encrypt your data using AES/Rijndael using that random key generated in part i.
    /// iii.Use RSA encryption to asymmetrically encrypt the random key generated in part i.
    /// 
    /// Publish (eg write to a file) the outputs from parts ii.and iii.: the AES-encrypted data and the RSA-encrypted random key.
    /// 
    /// Decryption:
    /// i.Decrypt the AES random key using your private RSA key.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class RSA
    {
        /// <summary>
        /// The padding scheme often used together with RSA encryption.
        /// </summary>
        private const bool OptimalAsymmetricEncryptionPadding = false;

        public static CryptoKey GenerateRsaKeyPair()
        {
            //Generate a public/private key pair.
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();

            //Save the public key information to an RSAParameters structure.
            //RSAParameters rsaKeyInfo = rsaProvider.ExportParameters(true);

            return new CryptoKey(rsaProvider.ToXmlString(true), rsaProvider.ToXmlString(false));
        }
        
        /// <summary>
        /// Converts the RSA-encrypted text into a string
        /// </summary>
        /// <param name="text">The plain text input</param>
        /// <param name="publicKeyXml">The RSA public key in XML format</param>
        /// <param name="keySize">The RSA key length</param>
        /// <returns>The the RSA-encrypted text</returns>
        public static byte[] Encrypt(string text, string publicKeyXml, int keySize)
        {
            var encrypted = EncryptByteArray(Encoding.UTF8.GetBytes(text), publicKeyXml, keySize);
            return encrypted;
            //return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Converts the RSA-decrypted text into a string
        /// </summary>
        /// <param name="publicAndPrivateKeyXml"></param>
        /// <param name="keySize">The RSA key length</param>
        /// <param name="encryptedBytes"></param>
        /// <returns>The the RSA-decrypted text</returns>
        public static string Decrypt(byte[] encryptedBytes, string publicAndPrivateKeyXml, int keySize)
        {
            //var encryptedBytes =  Convert.FromBase64String(text);
            var decrypted = DecryptByteArray(encryptedBytes, publicAndPrivateKeyXml, keySize);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// Gets and validates the RSA-encrypted text as a byte array
        /// </summary>
        /// <param name="data">The plain text in byte array format</param>
        /// <param name="publicKeyXml">The RSA public key in XML format</param>
        /// <param name="keySize">The RSA key length</param>
        /// <returns>The the RSA-encrypted byte array</returns>
        private static byte[] EncryptByteArray(byte[] data, string publicKeyXml, int keySize)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("Data are empty", nameof(data));
            }

            int maxLength = GetMaxDataLength(keySize);

            if (data.Length > maxLength)
            {
                throw new ArgumentException($"Maximum data length is {maxLength}", nameof(data));
            }

            if (!IsKeySizeValid(keySize))
            {
                throw new ArgumentException("Key size is not valid", nameof(keySize));
            }

            if (String.IsNullOrEmpty(publicKeyXml))
            {
                throw new ArgumentException("Key is null or empty", "publicKeyXml");
            }

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, OptimalAsymmetricEncryptionPadding);
            }
        }

        /// <summary>
        /// Gets and validates the RSA-decrypted text as a byte array
        /// </summary>
        /// <param name="data">The plain text in byte array format</param>
        /// <param name="publicAndPrivateKeyXml"></param>
        /// <param name="keySize">The RSA key length</param>
        /// <returns>The the RSA-decrypted byte array</returns>
        private static byte[] DecryptByteArray(byte[] data, string publicAndPrivateKeyXml, int keySize)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("Data are empty", nameof(data));
            }

            if (!IsKeySizeValid(keySize))
            {
                throw new ArgumentException("Key size is not valid", nameof(keySize));
            }

            if (String.IsNullOrEmpty(publicAndPrivateKeyXml))
            {
                throw new ArgumentException("Key is null or empty", nameof(publicAndPrivateKeyXml));
            }

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, OptimalAsymmetricEncryptionPadding);
            }
        }

        /// <summary>
        /// Gets the maximum data length for a given key
        /// </summary>       
        /// <param name="keySize">The RSA key length</param>
        /// <returns>The maximum allowable data length</returns>
        public static int GetMaxDataLength(int keySize)
        {
            if (OptimalAsymmetricEncryptionPadding)
            {
                return ((keySize - 384) / 8) + 7;
            }
            return ((keySize - 384) / 8) + 37;
        }

        /// <summary>
        /// Checks if the given key size if valid
        /// </summary>       
        /// <param name="keySize">The RSA key length</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 &&
                   keySize <= 16384 &&
                   keySize % 8 == 0;
        }
    }
}