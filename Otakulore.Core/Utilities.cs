using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Otakulore.Core.AniList;

namespace Otakulore.Core;

public static class Utilities
{

    public static string? GetEnumValue<T>(this T type)
    {
        var field = type.GetType().GetField(type.ToString());
        var attributes = (EnumMemberAttribute[])field.GetCustomAttributes(typeof(EnumMemberAttribute), false);
        return attributes.Length > 0 ? attributes[0].Value : type.ToString();
    }

    public static string? GetEnumDescription<T>(this T type)
    {
        var field = type.GetType().GetField(type.ToString());
        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : type.ToString();
    }

    public static void ExtractEmbeddedFile(string resourceName, string filePath)
    {
        using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        resourceStream?.CopyTo(fileStream);
    }

    public static MediaSeason GetSeasonFromDate(DateTime date)
    {
        var day = date.DayOfYear - Convert.ToInt32(DateTime.IsLeapYear(date.Year) && date.DayOfYear > 59);
        return day switch
        {
            < 80 or >= 355 => MediaSeason.Winter,
            >= 80 and < 172 => MediaSeason.Spring,
            >= 172 and < 266 => MediaSeason.Summer,
            _ => MediaSeason.Fall
        };
    }

    // from https://stackoverflow.com/a/16407272
    public static string HtmlToPlainText(string html)
    {
        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";
        const string stripFormatting = @"<[^>]*(>|$)";
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";
        var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);
        var text = html;
        text = WebUtility.HtmlDecode(text);
        text = tagWhiteSpaceRegex.Replace(text, "><");
        text = lineBreakRegex.Replace(text, Environment.NewLine);
        text = stripFormattingRegex.Replace(text, string.Empty);
        return text;
    }

}