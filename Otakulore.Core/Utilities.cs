using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

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
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        resource?.CopyTo(file);
    }

    public static string ExtractEmbeddedText(string resourceName)
    {
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(resource);
        return reader.ReadToEnd();
    }

    public static Color ParseColorHex(string colorHex)
    {
        if (!colorHex.StartsWith('#'))
            return Color.White;
        colorHex = colorHex.TrimStart('#');
        return colorHex.Length == 6
            ? // parses rgb
            Color.FromArgb(255, // alpha
                int.Parse(colorHex.Substring(0, 2), NumberStyles.HexNumber), // red
                int.Parse(colorHex.Substring(2, 2), NumberStyles.HexNumber), // green
                int.Parse(colorHex.Substring(4, 2), NumberStyles.HexNumber)) // blue
            : // parses argb
            Color.FromArgb(int.Parse(colorHex.Substring(0, 2), NumberStyles.HexNumber), // alpha
                int.Parse(colorHex.Substring(2, 2), NumberStyles.HexNumber), // red
                int.Parse(colorHex.Substring(4, 2), NumberStyles.HexNumber), // green
                int.Parse(colorHex.Substring(6, 2), NumberStyles.HexNumber)); // blue
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