using System.Text.RegularExpressions;

namespace BuildingBlock.Utilities;

internal static class Validator
{
    public static bool IsValidEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        }
        else
        {
            return false;
        }
    }
}
