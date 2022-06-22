using Otakulore.Models;

namespace Otakulore.Pages;

public partial class LibraryPage
{

    private LibraryViewModel Context => (LibraryViewModel)BindingContext;

    public LibraryPage()
    {
        InitializeComponent();
        BindingContext = MauiExtensions.GetService<LibraryViewModel>();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (App.Client.IsAuthenticated)
        {
            Context.OnAfterAuthentication();
            return;
        }
        var token = App.Settings.AccessToken;
        if (token == null)
        {
            var loginAccepted = await DisplayAlert("Sorry!", "This page requires you to login into your AniList account.", "Login", "Back");
            if (loginAccepted)
                await Shell.Current.GoToAsync(nameof(LoginPage));
            else
                await Shell.Current.GoToAsync("..");
            return;
        }
        var isAuthenticated = await App.Client.TryAuthenticateAsync(token);
        if (isAuthenticated)
        {
            Context.OnAfterAuthentication();
            return;
        }
        var reLoginAccepted = await DisplayAlert("Sorry!", "Your access token no longer has enabled access for this client.", "Relogin", "Back");
        if (reLoginAccepted)
            await Shell.Current.GoToAsync(nameof(LoginPage));
        else
            await Shell.Current.GoToAsync("..");
    }

}