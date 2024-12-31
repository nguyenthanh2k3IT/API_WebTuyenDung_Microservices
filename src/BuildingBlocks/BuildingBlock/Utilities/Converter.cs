using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BuildingBlock.Utilities;

public static class Converter
{
	public static List<T> SplitStringToList<T>(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return new List<T>();
		}

		return input
			.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(value => (T)Convert.ChangeType(value.Trim(), typeof(T)))
			.ToList();
	}

    public static T? DeserializeJson<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("JSON input cannot be null or empty");
        }

        return JsonSerializer.Deserialize<T>(json);
    }

    public static string SerializeToJson<T>(T obj, bool indented = false)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = indented
        };

        return JsonSerializer.Serialize(obj, options);
    }

    public static string FormatDateTime(DateTime dateTime, string format = "dd-MM-yyyy HH:mm:ss")
    {
        return dateTime.ToString(format);
    }

    public static string ToSlug(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        // Bước 1: Chuyển đổi sang chữ thường
        string slug = input.ToLowerInvariant();

        // Bước 2: Loại bỏ dấu (normalize)
        slug = RemoveDiacritics(slug);

        // Bước 3: Loại bỏ ký tự đặc biệt
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", string.Empty);

        // Bước 4: Thay thế khoảng trắng và dấu gạch ngang thừa
        slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

        return slug;
    }

    private static string RemoveDiacritics(string input)
    {
        var normalizedString = input.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
