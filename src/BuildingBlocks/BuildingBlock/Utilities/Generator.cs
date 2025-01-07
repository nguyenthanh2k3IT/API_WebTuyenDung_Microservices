using System.Security.Cryptography;
using System.Text;

namespace BuildingBlock.Utilities;

public static class Generator
{
    private static readonly Random Random = new Random();

    public static int RandomNumber(int min, int max)
    {
        if (min > max)
            throw new ArgumentException("min should not be greater than max");

        return Random.Next(min, max + 1);
    }

    public static int RandomNumberByLength(int length)
    {
        if (length <= 0)
            throw new ArgumentException("Length must be greater than 0");

        int min = (int)Math.Pow(10, length - 1);
        int max = (int)Math.Pow(10, length) - 1;

        return Random.Next(min, max + 1);
    }

    public static string GenerateCode()
    {
        try
        {
            // Tạo chuỗi ngẫu nhiên 6 ký tự
            string randomString = CodeRandom(3).ToUpper();

            // Chèn dấu gạch nối vào giữa
            string formattedCode = $"{randomString.Substring(0, 3)}-{randomString.Substring(3, 3)}";

            return formattedCode;
        }
        catch
        {
            // Fallback trong trường hợp lỗi
            return "ERR-XXX";
        }
    }

    private static string CodeRandom(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new RNGCryptoServiceProvider();
        var data = new byte[length];

        random.GetBytes(data);

        var result = new StringBuilder(length);
        foreach (var b in data)
        {
            result.Append(chars[b % chars.Length]);
        }

        return result.ToString();
    }

    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random random = new Random();
        char[] result = new char[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }

        return new string(result);
    }
}
