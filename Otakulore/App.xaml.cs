using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Core.Providers;
using Otakulore.Models;
using Otakulore.Views;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;

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

    public static void NavigateFrame(Type type, object? parameter = null)
    {
        if (Window.Content is Frame { Content: MainView page })
            page.PageFrame.Navigate(type, parameter);
    }

    public static async Task AttachDialog(ContentDialog dialog)
    {
        dialog.XamlRoot = Window.Content.XamlRoot;
        await dialog.ShowAsync();
    }

    public static void ShowNotification(NotificationDataModel model)
    {
        if (Window.Content is not Frame { Content: MainView page })
            return;
        model.ContinueClicked += (_, _) => page.NotificationArea.Dismiss();
        page.NotificationArea.Show(model);
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
        if (Settings.UserToken != null)
            Client.SetToken(Settings.UserToken);
        var frame = new Frame();
        Window = new Window { Title = "Otakulore", Content = frame };
        frame.Navigate(typeof(MainView));
        Window.Closed += (_, _) => Settings.Save();
        Window.Activate();
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        ShowNotification("An unhandled exception occurred! " + args.Message);
        args.Handled = true;
    }

}