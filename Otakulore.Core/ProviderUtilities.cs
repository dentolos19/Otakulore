using HtmlAgilityPack;

namespace Otakulore.Core;

public class ProviderUtilities
{
    
    private static HtmlWeb? _htmlWebInstance;
    
    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

}