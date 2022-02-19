using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;

namespace Otakulore;

public partial class App
{

    public static Window Window { get; set; }
    public static Settings Settings { get; set; }
    public static AniClient Client { get; set; }
    public static IProvider[] Providers { get; set; }

    public static string[] Genres { get; set; }
    public static MediaTag[] Tags { get; set; }

    public App()
    {
        UnhandledException += OnUnhandledException;
        InitializeComponent();
    }

    public static void ResetSettings()
    {
        Settings = new Settings();
        Settings.Save();
    }

    public static void NavigateFrame(Type type, object? parameter = null)
    {
        if (Window.Content is Frame { Content: MainView page })
            page.PageFrame.Navigate(type, parameter);
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

    public static async Task AttachDialog(ContentDialog dialog)
    {
        dialog.XamlRoot = Window.Content.XamlRoot;
        await dialog.ShowAsync();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var frame = new Frame();
        Window = new Window { Title = "Otakulore", Content = frame };
        frame.Navigate(typeof(LoadingView));
        Window.Closed += (_, _) => Settings.Save();
        Window.Activate();
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        ShowNotification("An unhandled exception occurred! " + args.Message);
        args.Handled = true;
    }

}