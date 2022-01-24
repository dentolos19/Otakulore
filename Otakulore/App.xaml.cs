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

    public static Window MainWindow { get; private set; }
    public static Settings Settings { get; private set; }
    public static AniClient Client { get; } = new();
    public static IList<IProvider> Providers { get; } = new List<IProvider>();

    public static void ResetSettings()
    {
        Settings = new Settings();
        Settings.Save();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Settings = Settings.Load();
        if (Settings.LoadSeleniumAtStartup)
            ScrapingService.LoadWebDriver();
        var providers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IProvider)));
        foreach (var provider in providers)
            Providers.Add((IProvider)Activator.CreateInstance(provider));
        MainWindow = new Window { Title = "Otakulore" };
        var frame = new Frame();
        MainWindow.Content = frame;
        frame.Navigate(typeof(MainPage));
        MainWindow.Closed += (_, _) => Settings.Save();
        MainWindow.Activate();
    }

}