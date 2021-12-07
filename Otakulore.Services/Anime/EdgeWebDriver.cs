using OpenQA.Selenium.Edge;

namespace Otakulore.Services.Anime;

public class EdgeWebDriver
{

    private static EdgeDriver? _instance;

    public static EdgeDriver Instance => _instance ??= CreateInstance();

    private static EdgeDriver CreateInstance()
    {
        var edgeOptions = new EdgeOptions();
        edgeOptions.AddArgument("--headless"); // reference: https://peter.sh/experiments/chromium-command-line-switches/#headless
        var edgeDriver = new EdgeDriver();
        return edgeDriver;
    }

}