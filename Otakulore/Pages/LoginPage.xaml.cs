using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Alerts;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Pages;

[PageRoute]
public partial class LoginPage
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();
    private readonly SettingsService _settings = MauiHelper.GetService<SettingsService>();

    public LoginPage()
    {
        InitializeComponent();
        WebView.Source = new Uri("https://anilist.co/api/v2/oauth/authorize?client_id=7375&response_type=token");
    }

    private async void OnNavigating(object sender, WebNavigatingEventArgs args)
    {
        var tokenRegex = new Regex("(?<=access_token=)(.*)(?=&token_type)");
        if (!tokenRegex.IsMatch(args.Url))
            return;
        var accessToken = tokenRegex.Match(args.Url).Value;
        var hasAuthenticated = await _data.Client.TryAuthenticateAsync(accessToken);
        if (hasAuthenticated)
        {
            _settings.AccessToken = accessToken;
            await MauiHelper.NavigateBack();
        }
        else
        {
            await Toast.Make("Unable to authenticate with AniList!").Show();
            await MauiHelper.NavigateBack();
        }
    }

}