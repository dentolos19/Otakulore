using Microsoft.UI.Xaml;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class ProfileSettingsPanel
{

    public ProfileSettingsPanel()
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
        App.Settings.Save();
        App.NavigateFrame(typeof(HomePage));
        App.ShowNotification("You have been logged out!");
    }

}