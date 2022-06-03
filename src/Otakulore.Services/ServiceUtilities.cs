using HtmlAgilityPack;

namespace Otakulore.Services;

public static class ServiceUtilities
{

    private static Random? _randomInstance;
    private static HtmlWeb? _htmlWebInstance;

    public static Random Random => _randomInstance ??= new Random();
    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

}