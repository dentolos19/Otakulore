using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
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
        if (dialog.Result != null)
            Frame.Navigate(typeof(CinemaPage), new KeyValuePair<IProvider, object>(item.Provider, dialog.Result));
    }

    private void OnResetSettings(object sender, RoutedEventArgs args)
    {
        App.ResetSettings();
        Frame.Navigate(typeof(HomePage));
    }

}