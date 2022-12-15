using System.Text.RegularExpressions;
using Otakulore.Services;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Pages;

[PageService(PageServiceType.Transient)]
public partial class LoginPage
{

    public LoginPage()
    {
        InitializeComponent();
        WebView.Source = new Uri("https://anilist.co/api/v2/oauth/authorize?client_id=7375&response_type=token");
    }

    private async void OnNavigating(object? sender, WebNavigatingEventArgs args)
    {
        var tokenRegex = new Regex("(?<=access_token=)(.*)(?=&token_type)");
        if (!tokenRegex.IsMatch(args.Url))
            return;
        var accessToken = tokenRegex.Match(args.Url).Value;
        var hasAuthenticated = await DataService.Instance.Client.TryAuthenticateAsync(accessToken);
        if (hasAuthenticated)
        {
            SettingsService.Instance.AccessToken = accessToken;
            await MauiHelper.NavigateBack();
            await DisplayAlert("Login", "You have successfully logged into AniList!", "Back");
        }
        else
        {
            await MauiHelper.NavigateBack();
            await DisplayAlert("Login", "You have unsuccessfully logged into AniList!", "Back");
        }
    }

}