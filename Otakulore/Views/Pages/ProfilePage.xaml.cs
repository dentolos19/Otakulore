using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;
using Otakulore.Views.Panels;

namespace Otakulore.Views.Pages;

public sealed partial class ProfilePage
{

    private bool _isAlreadyNavigated;
    private int _id;

    public ProfilePage()
    {
        InitializeComponent();
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Overview", Tag = typeof(ComingSoonPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "List", Tag = typeof(ProfileListPanel) });
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (!App.Client.HasToken)
        {
            var model = new NotificationDataModel
            {
                Message = "This feature requires an authenticated AniList account.",
                ContinueText = "Login"
            };
            model.ContinueClicked += (_, _) => App.NavigateFrame(typeof(LoginPage));
            App.ShowNotification(model);
            return;
        }
        if (_isAlreadyNavigated)
            return;
        _isAlreadyNavigated = true;
        var user = await App.Client.GetUser();
        _id = user.Id;
        PersonPictureImage.ProfilePicture = new BitmapImage(new Uri(user.Avatar.LargeImageUrl));
        PersonNameText.Text = user.Name;
        PanelNavigation.SelectedItem = PanelNavigation.MenuItems.First();
        LoadingIndicator.IsLoading = false;
    }

    private void OnNavigatePanel(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
            PanelFrame.Navigate(typeof(ProfileSettingsPanel));
        else if (args.SelectedItem is NavigationViewItem { Tag: Type type })
            PanelFrame.Navigate(type, _id);
    }

}