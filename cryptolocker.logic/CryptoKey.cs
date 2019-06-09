namespace CryptoLocker.Logic
{
    public class CryptoKey
    {
        public string PublicAndPrivateKey { get; private set; }
        public string PublicKey { get; private set; }
        
        public CryptoKey(string publicAndPrivateKey, string publicKey)
        {
            PublicAndPrivateKey = publicAndPrivateKey;
            PublicKey = publicKey;
        }
    }
}