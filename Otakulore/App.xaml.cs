using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JikanDotNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Views;

namespace Otakulore;

public partial class App
{

    public static Window MainWindow { get; } = new() { Title = "Otakulore" };
    public static Settings Settings { get; } = Settings.Load();
    public static IJikan Jikan { get; } = new Jikan(true);
    public static IList<IProvider> Providers { get; } = new List<IProvider>();

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        if (Settings.LoadSeleniumAtStartup)
            ScrapingService.LoadWebDriver();
        var providers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IProvider)));
        foreach (var provider in providers)
            Providers.Add((IProvider)Activator.CreateInstance(provider));
        var frame = new Frame();
        MainWindow.Content = frame;
        frame.Navigate(typeof(MainPage));
        MainWindow.Closed += (_, _) => Settings.Save();
        MainWindow.Activate();
    }

}