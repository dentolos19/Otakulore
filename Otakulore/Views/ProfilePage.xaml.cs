using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class ProfilePage
{

    public ProfilePage()
    {
        InitializeComponent();
    }

    private ProfileViewModel ViewModel => (ProfileViewModel)DataContext;

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (App.Settings.UserToken == null)
        {
            if (await App.ShowDialog("This feature requires an authenticated AniList account!\n\nDo you want to login to use this feature?", "AniList Exclusive Content", true))
                Frame.Navigate(typeof(ProfileLoginPage));
            else
                Frame.Navigate(typeof(HomePage));
            return;
        }
        App.Client.SetToken(App.Settings.UserToken);
        var user = await App.Client.GetUser();
        DataContext = new ProfileViewModel
        {
            Id = user.Id,
            ImageUrl = user.Avatar.LargeImageUrl,
            Name = user.Name
        };
        NavigationView.SelectedItem = NavigationView.MenuItems.First();
    }

    private void OnNavigationChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is not NavigationViewItem item)
            return;
        var type = Type.GetType("Otakulore.Views." + item.Tag);
        if (type != null)
            ContentView.Navigate(type, ViewModel.Id);
    }

}