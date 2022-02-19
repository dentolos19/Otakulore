using System.ComponentModel;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Core.Providers;

namespace Otakulore.Views;

public sealed partial class LoadingView
{

    public LoadingView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        var task = new BackgroundWorker { WorkerReportsProgress = true };
        task.DoWork += async (_, _) =>
        {
            App.Settings = Settings.Load();
            App.Providers = new IProvider[]
            {
                new AnimeKisaProvider(),
                new MangakakalotProvider(),
                new NovelhallProvider()
            };
            task.ReportProgress(0, "Authenticating with AniList");
            App.Client = new AniClient();
            App.Client.RateUpdated += (_, _) =>
            {
                var rateLimit = App.Client.RateLimit;
                var rateRemaining = App.Client.RateRemaining;
                if (App.Client.RateLimit > 20)
                    return;
                App.ShowNotification($"Your rate remaining is running low! ({rateRemaining}/{rateLimit})");
            };
            if (App.Settings.UserToken != null)
                App.Client.SetToken(App.Settings.UserToken);
            var (genres, tags) = await App.Client.GetGenresAndTags();
            App.Genres = genres;
            App.Tags = tags;
        };
        task.ProgressChanged += (_, args) =>
        {
            ProgressIndicator.IsIndeterminate = args.ProgressPercentage <= 0;
            ProgressIndicator.Value = args.ProgressPercentage;
            ProgressStatus.Text = args.UserState != null ? args.UserState.ToString() : "Loading";
        };
        task.RunWorkerCompleted += (_, _) =>
        {
            Frame.Navigate(typeof(MainView));
        };
        task.RunWorkerAsync();
    }

}