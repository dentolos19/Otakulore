using ReverseMarkdown;

namespace Otakulore.Core;

public static class Utilities
{

    public static string ConvertHtmlToMarkdown(string html)
    {
        var configuration = new Config();
        var converter = new Converter(configuration);
        return converter.Convert(html);
    }

}