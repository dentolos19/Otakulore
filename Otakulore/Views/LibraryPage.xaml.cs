using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class LibraryPage
{

    public LibraryPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (App.Settings.UserToken == null)
        {
            Frame.Navigate(typeof(LoginPage));
            return;
        }
        App.Client.SetToken(App.Settings.UserToken);
        var user = await App.Client.GetUser();
        DataContext = new LibraryViewModel
        {
            ImageUrl = user.User.Avatar.LargeImageUrl,
            Name = user.User.Name
        };
    }

}