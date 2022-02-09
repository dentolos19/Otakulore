using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Core.Providers;
using Otakulore.Models;
using Otakulore.Views;

namespace Otakulore;

public partial class App
{

    public static Window Window { get; private set; }
    public static Settings Settings { get; private set; }
    public static AniClient Client { get; } = new();
    public static IList<IProvider> Providers { get; } = new List<IProvider>();

    public App()
    {
        UnhandledException += OnUnhandledException;
        InitializeComponent();
    }

    public static void NavigateContent(Type type, object? parameter = null)
    {
        if (Window.Content is Frame { Content: MainPage page })
            page.ContentView.Navigate(type, parameter);
    }

    public static void ShowNotification(NotificationDataModel model)
    {
        if (Window.Content is not Frame { Content: MainPage page })
            return;
        model.ContinueClicked += (_, _) => page.NotificationSystem.Dismiss();
        page.NotificationSystem.Show(model);
    }

    public static void ShowNotification(string message)
    {
        ShowNotification(new NotificationDataModel { Message = message });
    }

    public static void ResetSettings()
    {
        Settings = new Settings();
        Settings.Save();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Settings = Settings.Load();
        Providers.Add(new AnimeKisaProvider());
        Providers.Add(new MangakakalotProvider());
        var frame = new Frame();
        Window = new Window { Title = "Otakulore", Content = frame };
        frame.Navigate(typeof(MainPage));
        Window.Closed += (_, _) => Settings.Save();
        Window.Activate();
    }

    private void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs args)
    {
        ShowNotification("An unhandled exception occurred! " + args.Message);
        args.Handled = true;
    }

}