using System;
using System.IO;
using OpenQA.Selenium.Edge;

namespace Otakulore.Core
{

    public static class EdgeWebDriver
    {

        private static EdgeDriver? _webDriver;

        public static EdgeDriver GetWebDriver()
        {
            if (_webDriver != null)
                return _webDriver;
            var driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            var service = EdgeDriverService.CreateChromiumService(driverPath, Path.Combine(driverPath, "EdgeWebDriver.exe"));
            service.HideCommandPromptWindow = true;
            var options = new EdgeOptions();
            options.UseChromium = true;
            options.AddArgument("headless");
            _webDriver = new EdgeDriver(service, options);
            return _webDriver;
        }

        public static string ScrapeDynamicHtml(string url)
        {
            var driver = GetWebDriver();
            driver.Url = url;
            return driver.PageSource;
        }

    }

}