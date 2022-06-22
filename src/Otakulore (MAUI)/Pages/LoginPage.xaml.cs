using System.Text.RegularExpressions;

namespace Otakulore.Pages;

public partial class LoginPage
{
    
    public LoginPage()
    {
        InitializeComponent();
        WebView.Source = "https://anilist.co/api/v2/oauth/authorize?client_id=7375&response_type=token";
    }

    private async void OnNavigated(object sender, WebNavigatedEventArgs args)
    {
        var tokenRegex = new Regex("(?<=access_token=)(.*)(?=&token_type)");
        if (!tokenRegex.IsMatch(args.Url))
            return;
        var accessToken = tokenRegex.Match(args.Url).Value;
        var isAuthenticated = await App.Client.TryAuthenticateAsync(accessToken);
        if (isAuthenticated)
        {
            App.Settings.AccessToken = accessToken;
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("Sorry!", "We are unable to log you in.", "Okay");
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }

}