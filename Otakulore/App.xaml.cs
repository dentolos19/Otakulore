using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    public static void NavigateContent(Type pageType, object? parameter = null)
    {
        if (Window.Content is Frame { Content: MainPage page })
            page.ContentView.Navigate(pageType, parameter);
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
        Window = new Window { Title = "Otakulore" };
        var frame = new Frame();
        frame.Navigate(typeof(MainPage));
        Window.Content = frame;
        Window.Closed += (_, _) => Settings.Save();
        Window.Activate();
    }

}