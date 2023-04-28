public static class Program
{
    private static RSATextEncriptor encriptor = new();
    private static Dictionary<string, Action<string>> _commandsCodes = new()
    {
        {"genkeys", GenerateKeys},
        {"showkeys", ShowKeys },
        {"enc", EncryptText},
        {"dec", DecryptText},
    };
    private static int minCommandLength = 3;
    private static bool _isOpened = true;

    private static void Main(string[] args)
    {
        Console.WriteLine("rsa-encrypting-decrypting-app v0.0.1 by ropira125, modified version by Kandellyabr717");
        while (_isOpened)
        {
            var expression = Console.ReadLine() ?? "";
            try
            {
                (var argument, var command) = ParseExpression(expression);
                command.Invoke(argument);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid command");
            }
        }
    }

    private static (string argument, Action<string> command) ParseExpression(string expression)
    {
        if (expression.Length < minCommandLength)
        {
            throw new ArgumentException();
        }
        var border = expression.IndexOf(' ');
        string argument;
        Action<string> command;
        if (border == -1)
        {
            argument = "";
            command = _commandsCodes[expression[0..^0]];
        }
        else
        {
            argument = expression[(border + 1)..^0];
            command = _commandsCodes[expression[0..border]];
        }
        return (argument, command);
    }

    private static void GenerateKeys(string blank)
    {
        encriptor.GenrateKeys();
    }

    private static void ShowKeys(string blank)
    {
        Console.WriteLine(encriptor.KeyPair);
    }

    private static void EncryptText(string text)
    {
        var enctripted = encriptor.EncryptText(text);
        Console.WriteLine("Encrypted Text:\n\n" + enctripted + "\n");
    }

    private static void DecryptText(string text)
    {
        var dectripted = encriptor.DecryptText(text);
        Console.WriteLine("Decrypted Text:\n\n" + dectripted + "\n");
    }
}