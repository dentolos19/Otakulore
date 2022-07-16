using HtmlAgilityPack;

namespace Otakulore.Core;

public static class Utilities
{
    
    private static HtmlWeb? _htmlWebInstance;
    
    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

}