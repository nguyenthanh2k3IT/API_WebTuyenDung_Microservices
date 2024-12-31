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
            DateTime today = DateTime.Today;
            string datePart = today.ToString("ddMMyy");
            string randomString = GenerateRandomString(6).ToUpper();
            string finalCode = $"{randomString}-{datePart}";
            return finalCode;
        }
        catch (Exception ex)
        {
            return GenerateRandomString(6);
        }
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
