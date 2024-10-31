using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Linq;

public class PasswordManager
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hash);
    }

    public static string SuggestPassword(int length, bool includeSpecialCharacters)
    {
        const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        string validChars = lowerChars + upperChars + digits;
        if (includeSpecialCharacters)
        {
            validChars += specialChars;
        }

        StringBuilder password = new StringBuilder();
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[randomBytes[i] % validChars.Length]);
            }
        }

        return password.ToString();
    }

    public static void SaveHashToFile(string hash)
    {
        File.AppendAllText("hashes.txt", hash + Environment.NewLine);
    }

    public static string[] LoadHashesFromFile()
    {
        if (File.Exists("hashes.txt"))
        {
            return File.ReadAllLines("hashes.txt");
        }
        return Array.Empty<string>(); // Retorna array vazio se o arquivo não existir
    }

    public static bool ValidatePassword(string password)
    {
        return password.Length >= 8 && password.Any(char.IsDigit) && password.Any(char.IsUpper);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciador de Senhas ===");
            Console.WriteLine("1. Gerar Hash da Senha");
            Console.WriteLine("2. Verificar Senha");
            Console.WriteLine("3. Sugerir Senha");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha uma opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Digite a senha: ");
                    string passwordToHash = Console.ReadLine();

                    
                    if (PasswordManager.ValidatePassword(passwordToHash))
                    {
                        string hash = PasswordManager.HashPassword(passwordToHash);
                        PasswordManager.SaveHashToFile(hash);
                        Console.WriteLine($"Hash gerado: {hash}");
                    }
                    else
                    {
                        Console.WriteLine("A senha deve ter pelo menos 8 caracteres, incluir uma letra maiúscula e um dígito.");
                    }
                    break;

                case "2":
                    Console.Write("Digite a senha a ser verificada: ");
                    string passwordToVerify = Console.ReadLine();
                    Console.Write("Digite o hash: ");
                    string hashToVerify = Console.ReadLine();

                    if (PasswordManager.VerifyPassword(passwordToVerify, hashToVerify))
                    {
                        Console.WriteLine("Senha verificada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta.");
                    }
                    break;

                case "3":
                    Console.Write("Comprimento da senha sugerida: ");
                    int length;
                    if (int.TryParse(Console.ReadLine(), out length) && length >= 8)
                    {
                        Console.Write("Incluir caracteres especiais? (s/n): ");
                        bool includeSpecialChars = Console.ReadLine().Trim().ToLower() == "s";
                        string suggestedPassword = PasswordManager.SuggestPassword(length, includeSpecialChars);
                        Console.WriteLine($"Senha sugerida: {suggestedPassword}");
                    }
                    else
                    {
                        Console.WriteLine("O comprimento deve ser um número inteiro maior ou igual a 8.");
                    }
                    break;

                case "4":
                    return; // Sair do programa

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
 