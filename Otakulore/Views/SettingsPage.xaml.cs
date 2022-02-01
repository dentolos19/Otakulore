using System;
using Windows.Storage;
using Windows.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views;

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
        App.ResetSettings();
        Frame.Navigate(typeof(HomePage));
    }

    private async void OnOpenLocalFolder(object sender, RoutedEventArgs args)
    {
        await Launcher.LaunchFolderPathAsync(ApplicationData.Current.LocalFolder.Path);
    }

}