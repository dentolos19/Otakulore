using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Views;

namespace Otakulore;

public partial class App
{

    public App()
    {
        InitializeComponent();
    }

    public static Window Window { get; private set; }
    public static Settings Settings { get; private set; }
    public static AniClient Client { get; } = new();
    public static IList<IProvider> Providers { get; } = new List<IProvider>();

    public static void NavigateMainContent(Type pageType, object? parameter = null)
    {
        if (Window.Content is Frame { Content: MainPage page })
            page.ContentView.Navigate(pageType, parameter);
    }

    public static async Task<bool> ShowDialog(string message, string? title = null, bool showOption = false, string optionText = "Continue")
    {
        var dialog = new ContentDialog
        {
            XamlRoot = Window.Content.XamlRoot,
            Content = message,
            CloseButtonText = "Close"
        };
        if (title != null)
            dialog.Title = title;
        if (!showOption)
            return await dialog.ShowAsync() == ContentDialogResult.Primary;
        dialog.PrimaryButtonText = optionText;
        dialog.CloseButtonText = "Cancel";
        return await dialog.ShowAsync() == ContentDialogResult.Primary;
    }

    public static void ResetSettings()
    {
        Settings = new Settings();
        Settings.Save();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Settings = Settings.Load();
        if (Settings.LoadSeleniumAtStartup)
            ScrapingService.PreloadWebDriver();
        var providers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IProvider)));
        foreach (var provider in providers)
            Providers.Add((IProvider)Activator.CreateInstance(provider));
        var frame = new Frame();
        Window = new Window { Title = "Otakulore", Content = frame };
        frame.Navigate(typeof(MainPage));
        Window.Closed += (_, _) => Settings.Save();
        Window.Activate();
    }

}