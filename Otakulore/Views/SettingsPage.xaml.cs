using Otakulore.Core;
using Otakulore.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Otakulore.Views;

public partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
        foreach (var provider in App.Providers)
            ProviderList.Items.Add(new ProviderItemModel(provider));
    }

    private void OnOpenProvider(object sender, RoutedEventArgs args)
    {
        if (sender is not Button component)
            return;
        if (component.Tag is IProvider provider)
            NavigationService.Navigate(new SearchProviderPage(provider));
    }

    private void OnOpenProviderUrl(object sender, RoutedEventArgs args)
    {
        if (sender is not Button component)
            return;
        if (component.Tag is Uri url)
            Process.Start(new ProcessStartInfo
            {
                FileName = url.ToString(),
                UseShellExecute = true
            });
    }

}