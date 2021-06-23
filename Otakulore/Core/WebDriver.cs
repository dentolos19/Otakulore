using System.Drawing;
using System.IO;
using Windows.Storage;
using OpenQA.Selenium.Edge;

namespace Otakulore.Core
{

    public static class WebDriver
    {

        private static EdgeDriver _webDriver;

        public static EdgeDriver GetWebDriver()
        {
            if (_webDriver != null)
                return _webDriver;
            var service = EdgeDriverService.CreateChromiumService(ApplicationData.Current.LocalFolder.Path, Path.Combine(ApplicationData.Current.LocalFolder.Path, "EdgeWebDriver.exe"));
            service.HideCommandPromptWindow = true;
            var options = new EdgeOptions { UseChromium = true };
            options.AddArgument("headless");
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-dev-shm-using");
            options.AddArgument("--remote-debugging-port=9222");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-setuid-sandbox");
            _webDriver = new EdgeDriver(service, options);
            _webDriver.Manage().Window.Size = new Size(1920, 1080);
            return _webDriver;
        }

    }

}