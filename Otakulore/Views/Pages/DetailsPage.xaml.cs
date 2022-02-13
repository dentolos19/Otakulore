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

    private Media _media;
    private MediaEntry? _entry;

    public DetailsPage()
    {
        InitializeComponent();
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Overview", Tag = typeof(DetailsOverviewPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Characters", Tag = typeof(ComingSoonPanel) });
        PanelNavigation.MenuItems.Add(new NavigationViewItem { Content = "Related", Tag = typeof(ComingSoonPanel) });
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media media)
            return;
        _media = media;
        _entry = media.Entry;
        CoverImage.Source = _media.CoverImage.ExtraLargeImageUrl;
        TitleText.Text = _media.Title.Preferred;
        SubtitleText.Text = _media.StartDate.Year.HasValue ? _media.StartDate.Year.Value.ToString() : "Unknown Year";
        TrackButton.IsChecked = _entry != null;
        FavoriteButton.IsChecked = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id) != null;
        foreach (var provider in App.Providers)
            switch (_media.Type)
            {
                case MediaType.Anime when provider is IAnimeProvider:
                case MediaType.Manga when provider is IMangaProvider:
                    ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
            }
        PanelNavigation.SelectedItem = PanelNavigation.MenuItems.First();
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Provider, _media.Title.Preferred);
        await dialog.ShowAsync();
    }

    private async void OnTrackRequested(object sender, RoutedEventArgs args)
    {
        TrackButton.IsChecked = _entry != null;
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
        var dialog = new ManageTrackerDialog(_entry);
        await App.AttachDialog(dialog);
    }

    private void OnUpdateFavorite(object sender, RoutedEventArgs args)
    {
        var item = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id);
        if (FavoriteButton.IsChecked == true)
        {
            if (item == null)
                App.Settings.Favorites.Add(_media);
        }
        else
        {
            if (item != null)
                App.Settings.Favorites.Remove(item);
        }
    }

    private void OnNavigatePanel(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem { Tag: Type type })
            PanelFrame.Navigate(type, _media);
    }

}