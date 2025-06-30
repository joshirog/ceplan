using System.Globalization;
using System.Text;

namespace Application.Commons.Helpers;

public static class StringHelper
{
    public static string ReplaceAccents(string input)
    {
        return input
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u")
            .Replace("Á", "A")
            .Replace("É", "E")
            .Replace("Í", "I")
            .Replace("Ó", "O")
            .Replace("Ú", "U");
    }
    
    public static string RemoveDiacritics(this string s)
    {
        var normalizedString = s.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString
                     .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != 
                                 UnicodeCategory.NonSpacingMark))
        {
            stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }
}