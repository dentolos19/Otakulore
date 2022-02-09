using Microsoft.UI.Xaml;

namespace Otakulore.Views;

public sealed partial class ProfileSettingsPage
{

    public ProfileSettingsPage()
    {
        InitializeComponent();
    }

    private void OnRequestSave(object sender, RoutedEventArgs args)
    {
        App.ShowNotification("This function is unavailable at the moment!");
    }

    private void OnRequestLogout(object sender, RoutedEventArgs args)
    {
        App.Settings.UserToken = null;
        App.NavigateContent(typeof(HomePage));
        App.ShowNotification("You have been logged out!");
    }

}