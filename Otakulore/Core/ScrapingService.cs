using System.IO;
using System.Reflection;
using Windows.Storage;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Otakulore.Core;

public static class ScrapingService
{

    private static EdgeDriver? _webDriverInstance;
    private static HtmlWeb? _htmlWebInstance;

    public static EdgeDriver WebDriver => _webDriverInstance ??= CreateWebDriver();
    public static HtmlWeb HtmlWeb => _htmlWebInstance ??= new HtmlWeb();

    public static void LoadWebDriver()
    {
        _webDriverInstance ??= CreateWebDriver();
    }

    public static void ScrollToElement(this IJavaScriptExecutor executor, IWebElement element)
    {
        if (element.Location.Y > 200)
            executor.ExecuteScript($"window.scrollTo({0}, {element.Location.Y - 100})");
    }

    private static EdgeDriver CreateWebDriver()
    {
        var driverDir = ApplicationData.Current.LocalFolder.Path;
        var driverFile = Path.Combine(driverDir, "msedgedriver.exe");
        if (!File.Exists(driverFile))
        {
            using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Otakulore.Resources.Files.msedgedriver.exe");
            using var file = new FileStream(driverFile, FileMode.Create, FileAccess.Write);
            resource?.CopyTo(file);
        }
        var edgeService = EdgeDriverService.CreateDefaultService(driverDir);
        edgeService.HideCommandPromptWindow = true;
        var edgeOptions = new EdgeOptions();
        edgeOptions.AddArgument("--headless"); // reference: https://peter.sh/experiments/chromium-command-line-switches/#headless
        var edgeDriver = new EdgeDriver(edgeService, edgeOptions);
        return edgeDriver;
    }

}