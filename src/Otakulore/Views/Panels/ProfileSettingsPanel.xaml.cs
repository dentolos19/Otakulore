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
        App.AttachNotification("This function is unavailable at the moment!");
    }

    private void OnRequestLogout(object sender, RoutedEventArgs args)
    {
        // App.Client.SetToken(null);
        App.Settings.UserToken = null;
        App.Settings.Save();
        App.NavigateFrame(typeof(HomePage));
        App.AttachNotification("You have been logged out!");
    }

}