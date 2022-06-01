using System;
using System.IO;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.System;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views.Pages;

public sealed partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
        var version = Package.Current.Id.Version;
        VersionText.Text = $"v{version.Major}.{version.Minor}";
        #if DEBUG
        VersionText.Text += "-DEBUG";
        #endif
        using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Otakulore.Resources.Files.About.md");
        using var streamReader = new StreamReader(resourceStream);
        AboutText.Text = streamReader.ReadToEnd();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        foreach (var provider in App.Providers)
            ProviderList.Items.Add(new ProviderItemModel(provider));
        // RateLimitText.Text = $"Rate Remaining: {App.Client.RateRemaining}/{App.Client.RateLimit}";
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Data);
        await App.AttachDialog(dialog);
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
            App.Settings = new Settings();
            App.Settings.Save();
            App.NavigateFrame(typeof(HomePage));
            // TODO: replace navigation to home page with app restarting
        };
        App.ShowNotification(model);
    }

    private async void OnOpenLocalFolder(object sender, RoutedEventArgs args)
    {
        await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
    }

    private async void OnOpenMarkdownLink(object? sender, LinkClickedEventArgs args)
    {
        await Launcher.LaunchUriAsync(new Uri(args.Link));
    }

}