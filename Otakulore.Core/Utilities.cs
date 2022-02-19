using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Otakulore.Core.AniList;

namespace Otakulore.Core;

public static class Utilities
{

    private static HtmlWeb? _htmlWebInstance;

    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

    public static string? ToEnumValue<T>(this T type, bool allowDefaultValue = true)
    {
        if (type == null)
            return type.ToString();
        var field = type.GetType().GetField(type.ToString());
        var attributes = (EnumMemberAttribute[])field.GetCustomAttributes(typeof(EnumMemberAttribute), false);
        return attributes.Length > 0 ? attributes[0].Value : allowDefaultValue ? type.ToString() : null;
    }

    public static string? ToEnumDescription<T>(this T type, bool allowDefaultValue = false)
    {
        if (type == null)
            return type.ToString();
        var field = type.GetType().GetField(type.ToString());
        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : allowDefaultValue ? type.ToString() : null;
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

    public static string ConvertHtmlToPlainText(string html)
    {
        var lineBreakRegex = new Regex(@"(>|$)(\W|\n|\r)+<", RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(@"<[^>]*(>|$)", RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(@"<(br|BR)\s{0,1}\/{0,1}>", RegexOptions.Multiline);
        var text = html;
        text = WebUtility.HtmlDecode(text);
        text = tagWhiteSpaceRegex.Replace(text, "><");
        text = lineBreakRegex.Replace(text, Environment.NewLine);
        text = stripFormattingRegex.Replace(text, string.Empty);
        return text;
    }

}