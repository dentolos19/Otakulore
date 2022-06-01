using System;
using System.Threading.Tasks;
using AniListNet;
using AniListNet.Objects;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.Providers;
using Otakulore.Models;
using Otakulore.Views;

namespace Otakulore;

public partial class App
{

    private static Window _window;

    public static Settings Settings { get; set; }

    public static AniClient Client { get; private set; }
    public static IProvider[] Providers { get; private set; }

    public static string[] Genres { get; private set; }
    public static MediaTag[] Tags { get; private set; }
    public static MediaSeason CurrentSeason { get; private set; }

    public App()
    {
        UnhandledException += (_, args) =>
        {
            ShowNotification("An unhandled exception occurred! " + args.Message);
            args.Handled = true;
        };
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Settings = Settings.Load();
        Providers = new IProvider[]
        {
            new GogoanimeProvider(),
            new MangakakalotProvider(),
            new NovelhallProvider()
        };

        var date = DateTime.Today;
        var day = date.DayOfYear - Convert.ToInt32(DateTime.IsLeapYear(date.Year) && date.DayOfYear > 59);
        CurrentSeason = day switch
        {
            < 80 or >= 355 => MediaSeason.Winter,
            >= 80 and < 172 => MediaSeason.Spring,
            >= 172 and < 266 => MediaSeason.Summer,
            _ => MediaSeason.Fall
        };

        Client = new AniClient();
        Client.RateChanged += (_, args) =>
        {
            var rateLimit = args.RateLimit;
            var rateRemaining = args.RateRemaining;
            if (args.RateRemaining > 20)
                return;
            ShowNotification($"Your rate remaining is running low! ({rateRemaining}/{rateLimit})");
        };

        Task.Run(async () =>
        {
            if (Settings.UserToken != null)
                _ = await Client.TryAuthenticateAsync(Settings.UserToken);
            Genres = await Client.GetGenreCollectionAsync();
            Tags = await Client.GetTagCollectionAsync();
        });

        var frame = new Frame();
        _window = new Window { Title = "Otakulore", Content = frame };
        frame.Navigate(typeof(MainView));
        _window.Closed += (_, _) => Settings.Save();
        _window.Activate();
    }

    public static void NavigateFrame(Type type, object? parameter = null)
    {
        if (_window.Content is Frame { Content: MainView page })
            page.PageFrame.Navigate(type, parameter);
    }

    public static void ShowNotification(NotificationDataModel model)
    {
        if (_window.Content is not Frame { Content: MainView page })
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
        dialog.XamlRoot = _window.Content.XamlRoot;
        await dialog.ShowAsync();
    }

}