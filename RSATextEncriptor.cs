using System.Security.Cryptography;
using System.Text;

public class RSATextEncriptor
{
    public PublicPrivateKeyPair KeyPair { get; private set; }
    private RSACryptoServiceProvider _provider = new();

    public RSATextEncriptor()
    {
        GenrateKeys();
    }

    public void GenrateKeys()
    {
        KeyPair = new PublicPrivateKeyPair(_provider);
    }

    public string EncryptText(string text)
    {
        _provider.ImportRSAPublicKey(KeyPair.PublicKey, out int bytesRead);
        byte[] data = Encoding.UTF8.GetBytes(text);
        byte[] encryptedData = _provider.Encrypt(data, false);
        return Convert.ToBase64String(encryptedData);
    }

    public string DecryptText(string text)
    {
        _provider.ImportRSAPrivateKey(KeyPair.PrivateKey, out int bytesRead);
        byte[] encrytedData = Convert.FromBase64String(text);
        byte[] data = _provider.Decrypt(encrytedData, false);
        return Encoding.UTF8.GetString(data);
    }
}