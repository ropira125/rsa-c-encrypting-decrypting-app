using System.Security.Cryptography;

public struct PublicPrivateKeyPair
{
    public byte[] PublicKey { get; private set; }
    public byte[] PrivateKey { get; private set; }
    public string PublicKeyString
    {
        get
        {
            return Convert.ToBase64String(PublicKey);
        }
    }
    public string PrivateKeyString
    {
        get
        {
            return Convert.ToBase64String(PrivateKey);
        }
    }

    public PublicPrivateKeyPair(RSACryptoServiceProvider provider)
    {
        PublicKey = provider.ExportRSAPublicKey();
        PrivateKey = provider.ExportRSAPrivateKey();
    }

    public override string ToString()
    {
        return "Public Key:\n\n" + PublicKeyString + "\n\nPrivate Key:\n\n" + PrivateKeyString + "\n";
    }
}