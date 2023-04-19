using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

bool isOpen = true;

while (isOpen)
{

    Console.WriteLine("Choose an option:");
    Console.WriteLine("1. Generate RSA keys");
    Console.WriteLine("2. Encrypt message");
    Console.WriteLine("3. Decrypt message");
    Console.WriteLine("4. Close the program");
    Console.Write("Option: ");
    int mode = int.Parse(Console.ReadLine());

    if (mode == 1)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

        Console.WriteLine("Public Key: " + publicKey);
        Console.WriteLine("Private Key: " + privateKey);
    }
    else if (mode == 2)
    {
        Console.Write("Enter message to encrypt: ");
        string message = Console.ReadLine();
        Console.Write("Enter public key: ");
        string publicKeyString = Console.ReadLine();

        byte[] data = Encoding.UTF8.GetBytes(message);
        byte[] encryptedData = Encrypt(data, Convert.FromBase64String(publicKeyString));
        string encryptedMessage = Convert.ToBase64String(encryptedData);

        Console.WriteLine("Encrypted message: " + encryptedMessage);
    }
    else if (mode == 3)
    {
        Console.Write("Enter message to decrypt: ");
        string encryptedMessage = Console.ReadLine();
        Console.Write("Enter private key: ");
        string privateKeyString = Console.ReadLine();

        byte[] decryptedData = Decrypt(Convert.FromBase64String(encryptedMessage), Convert.FromBase64String(privateKeyString));
        string decryptedMessage = Encoding.UTF8.GetString(decryptedData);

        Console.WriteLine("Decrypted message: " + decryptedMessage);
    }
    else if (mode == 4)
    {
        isOpen = false;
    }
    else
    {
        Console.WriteLine("Invalid option selected.");
    }
}
byte[] Encrypt(byte[] data, byte[] publicKey)
{
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    rsa.ImportRSAPublicKey(publicKey, out int bytesRead);
    return rsa.Encrypt(data, false);
}

byte[] Decrypt(byte[] data, byte[] privateKey)
{
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    rsa.ImportRSAPrivateKey(privateKey, out int bytesRead);
    return rsa.Decrypt(data, false);
}