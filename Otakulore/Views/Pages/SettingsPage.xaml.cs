using System;
using Windows.Storage;
using Windows.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views.Pages;

public sealed partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        foreach (var provider in App.Providers)
            ProviderList.Items.Add(new ProviderItemModel(provider));
        RateLimitText.Text = $"Rate Limit: {App.Client.RateRemaining}/{App.Client.RateLimit}";
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Provider);
        await dialog.ShowAsync();
    }

    private void OnResetAllSettings(object sender, RoutedEventArgs args)
    {
        var model = new NotificationDataModel
        {
            Message = "Are you sure that you want to reset all settings? This will also clear all your favorites and sign out from your AniList account.",
            ContinueText = "Yes"
        };
        model.ContinueClicked += (_, _) =>
        {
            App.ResetSettings();
            App.NavigateFrame(typeof(HomePage));
        };
        App.ShowNotification(model);
    }

    private async void OnOpenLocalFolder(object sender, RoutedEventArgs args)
    {
        await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
    }

}