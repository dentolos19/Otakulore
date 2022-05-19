using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views.Dialogs;
using Otakulore.Views.Panels;

namespace Otakulore.Views.Pages;

public sealed partial class DetailsPage
{

    private MediaExtra _media;
    private MediaEntry? _mediaEntry;

    public DetailsPage()
    {
        InitializeComponent();
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Overview", Tag = typeof(DetailsOverviewPanel) });
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not int id)
            return;
        _media = await App.Client.GetMedia(id);
        LoadingIndicator.IsLoading = false;
        _mediaEntry = _media.Entry;
        CoverImage.Source = _media.Cover.LargeImageUrl;
        TitleText.Text = _media.Title.Preferred;
        SubtitleText.Text = _media.StartDate.HasValue ? _media.StartDate.Value.Year.ToString() : "Unknown Year";
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
        if (_media.Characters is { Length: > 0 })
            PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Characters", Tag = typeof(DetailsCharactersPanel) });
        if (_media.Staff is { Length: > 0 })
            PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Staff", Tag = typeof(DetailsStaffPanel) });
        if (_media.Relations is { Length: > 0 })
            PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Relations", Tag = typeof(DetailsRelationsPanel) });
        PanelNavigation.SelectedItem = PanelNavigation.MenuItems.First();
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Provider, _media.Title.Preferred);
        await App.AttachDialog(dialog);
    }

    private async void OnTrackRequested(object sender, RoutedEventArgs args)
    {
        TrackButton.IsChecked = _mediaEntry != null;
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