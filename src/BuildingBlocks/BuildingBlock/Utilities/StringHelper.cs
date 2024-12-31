using System.Text;
using System.Text.RegularExpressions;

namespace BuildingBlock.Utilities;

public static class StringHelper
{
    public static string convertToUnSign(string s)
    {
        try
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        catch (Exception ex)
        {
            return s;
        }
    }

    public static string BoDauVaKhoangTrang(string s)
    {
        s = s.ToLower();
        s = s.Replace(" ", "-");
        s = s.Replace(".", "dot");
        s = convertToUnSign(s);
        return s;
    }

    public static bool IsNullOrEmpty(Guid? id)
    {
        if (id is null || id == Guid.Empty)
        {
            return true;
        }
        return false;
    }

    public static bool IsModified(string? root, string? input)
    {
        if (!string.IsNullOrEmpty(input) && input != root)
        {
            return true;
        }
        return false;
    }
}
