using System;
using System.Linq;
using AniListNet;
using AniListNet.Objects;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Dialogs;
using Otakulore.Views.Panels;

namespace Otakulore.Views.Pages;

public sealed partial class DetailsPage
{

    private Media _media;
    private MediaEntry? _mediaEntry;

    public DetailsPage()
    {
        InitializeComponent();
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Overview", Tag = typeof(DetailsOverviewPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Characters", Tag = typeof(DetailsCharactersPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Staff", Tag = typeof(DetailsStaffPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Relations", Tag = typeof(DetailsRelationsPanel) });
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not int id)
            return;
        _media = await App.Client.GetMediaAsync(id);
        _mediaEntry = await App.Client.GetMediaEntryAsync(_media.Id);
        LoadingIndicator.IsLoading = false;
        CoverImage.Source = _media.Cover.LargeImageUrl;
        TitleText.Text = _media.Title.PreferredTitle;
        var startDate = _media.StartDate.ToDateOnly();
        if (startDate.HasValue)
            SubtitleText.Text = startDate.Value.Year.ToString();
        else
            SubtitleText.Visibility = Visibility.Collapsed;
        TrackButton.IsChecked = _mediaEntry != null;
        foreach (var provider in App.Providers)
            switch (_media.Type)
            {
                case MediaType.Anime:
                {
                    if (provider is IAnimeProvider)
                        ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
                }
                case MediaType.Manga when _media.Format == MediaFormat.Novel:
                {
                    if (provider is INovelProvider)
                        ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
                }
                case MediaType.Manga:
                {
                    if (provider is IMangaProvider)
                        ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
                }
            }
        PanelNavigation.SelectedItem = PanelNavigation.MenuItems.First();
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Data, _media.Title.PreferredTitle);
        await App.AttachDialog(dialog);
    }

    private async void OnTrackRequested(object sender, RoutedEventArgs args)
    {
        TrackButton.IsChecked = _mediaEntry != null;
        if (!App.Client.IsAuthenticated)
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
        var dialog = _mediaEntry != null ? new ManageTrackerDialog(_mediaEntry) : new ManageTrackerDialog(_media.Id);
        await App.AttachDialog(dialog);
        _mediaEntry = dialog.Result;
        TrackButton.IsChecked = _mediaEntry != null;
    }

    private void OnNavigatePanel(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem { Tag: Type type })
            PanelFrame.Navigate(type, _media);
    }

}