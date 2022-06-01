using System;
using System.Text.RegularExpressions;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;

namespace Otakulore.Views.Pages;

public sealed partial class LoginPage
{

    public LoginPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        await WebView.EnsureCoreWebView2Async();
        WebView.CoreWebView2.Navigate("https://anilist.co/api/v2/oauth/authorize?client_id=7375&response_type=token");
    }

    private async void OnNavigationStarting(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
    {
        var regex = new Regex("(?<=access_token=)(.*)(?=&token_type)");
        var url = args.Uri;
        if (!regex.IsMatch(url))
            return;
        var token = regex.Match(url).Value;
        var isAuthenticated = await App.Client.TryAuthenticateAsync(token);
        if (isAuthenticated)
        {
            App.Settings.UserToken = token;
            App.Settings.Save();
            App.NavigateFrame(typeof(ProfilePage));
        }
        else
        {
            // TODO: notify user
            App.NavigateFrame(typeof(HomePage));
        }
    }

}