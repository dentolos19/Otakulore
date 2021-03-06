using CommunityToolkit.Maui.Alerts;
using Otakulore.Pages;
using Otakulore.Resources.Themes;
using Otakulore.Services;

namespace Otakulore;

public partial class AppShell
{

    public AppShell()
    {
        InitializeComponent();

        var data = MauiHelper.GetService<DataService>();
        var settings = MauiHelper.GetService<SettingsService>();

        switch (settings.ThemeIndex)
        {
            case 1:
                Application.Current?.Resources.MergedDictionaries.Add(new Lavender());
                break;
        }
        if (!string.IsNullOrEmpty(settings.AccessToken))
        {
            Task.Run(async () =>
            {
                var hasAuthenticated = await data.Client.TryAuthenticateAsync(settings.AccessToken);
                if (!hasAuthenticated)
                {
                    await Toast.Make("Unable to authenticate with AniList!").Show();
                }
            });
        }

        MauiHelper.SetupRoutes();
    }

}