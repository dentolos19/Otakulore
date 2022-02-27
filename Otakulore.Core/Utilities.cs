using System.Drawing;
using System.Globalization;
using HtmlAgilityPack;
using ReverseMarkdown;

namespace Otakulore.Core;

public static class Utilities
{

    public static Random Random => _randomInstance ??= new Random();
    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

    private static Random? _randomInstance;
    private static HtmlWeb? _htmlWebInstance;

    public static Color ParseColorHex(string? colorHex)
    {
        if (colorHex == null || !colorHex.StartsWith('#'))
            return Color.Empty;
        colorHex = colorHex.TrimStart('#');
        return colorHex.Length == 6
            ? Color.FromArgb(255,
                int.Parse(colorHex.Substring(0, 2), NumberStyles.HexNumber),
                int.Parse(colorHex.Substring(2, 2), NumberStyles.HexNumber),
                int.Parse(colorHex.Substring(4, 2), NumberStyles.HexNumber))
            : Color.FromArgb(int.Parse(colorHex.Substring(0, 2), NumberStyles.HexNumber),
                int.Parse(colorHex.Substring(2, 2), NumberStyles.HexNumber),
                int.Parse(colorHex.Substring(4, 2), NumberStyles.HexNumber),
                int.Parse(colorHex.Substring(6, 2), NumberStyles.HexNumber));
    }

    public static string ConvertHtmlToMarkdown(string html)
    {
        var configuration = new Config();
        var converter = new Converter(configuration);
        return converter.Convert(html);
    }

}