using HtmlAgilityPack;
using OpenQA.Selenium.Edge;

namespace Otakulore.Core;

public static class ScrapingServices
{

    private static EdgeDriver? _webDriverInstance;
    private static HtmlWeb? _htmlWebInstance;

    public static EdgeDriver WebDriver => _webDriverInstance ??= CreateWebDriver();
    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

    private static EdgeDriver CreateWebDriver()
    {
        var edgeService = EdgeDriverService.CreateDefaultService();
        edgeService.HideCommandPromptWindow = true;
        var edgeOptions = new EdgeOptions();
        edgeOptions.AddArgument("--headless"); // reference: https://peter.sh/experiments/chromium-command-line-switches/#headless
        edgeOptions.AddArgument("--disable-extensions"); // reference: https://peter.sh/experiments/chromium-command-line-switches/#disable-extensions
        var edgeDriver = new EdgeDriver(edgeService, edgeOptions);
        return edgeDriver;
    }

}