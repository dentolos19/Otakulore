using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class ProfilePage
{

    private ProfileViewModel ViewModel => (ProfileViewModel)DataContext;

    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (App.Settings.UserToken == null)
        {
            var model = new NotificationDataModel
            {
                Message = "This feature requires an authenticated AniList account.",
                ContinueText = "Login"
            };
            model.ContinueClicked += (_, _) => App.NavigateContent(typeof(ProfileLoginPage));
            App.ShowNotification(model);
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