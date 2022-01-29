using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views;

public sealed partial class DetailsPage
{

    private Media _media;

    public DetailsPage()
    {
        InitializeComponent();
    }

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media data)
            return;
        _media = data;
        DataContext = new DetailsViewModel
        {
            Id = _media.Id,
            ImageUrl = _media.CoverImage.ExtraLargeImageUrl,
            Title = _media.Title.Romaji,
            Subtitle = _media.StartDate.Year != null ? _media.StartDate.Year.ToString() : "Unknown Year",
            Description = !string.IsNullOrEmpty(_media.Description) ? Utilities.HtmlToPlainText(_media.Description) : "No description provided.",
            Format = _media.Format.GetEnumDescription(),
            Status = _media.Status.GetEnumDescription(),
            Episodes = _media.Episodes?.ToString() ?? "Unknown",
            IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id) != null
        };
        foreach (var provider in App.Providers)
            switch (_media.Type)
            {
                case MediaType.Anime when provider is IAnimeProvider:
                case MediaType.Manga when provider is IMangaProvider:
                    ProviderList.Items.Add(new ProviderItemModel(provider));
                    break;
            }
    }

    private async void OnProviderClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is not ProviderItemModel item)
            return;
        var dialog = new SearchProviderDialog(item.Provider, ViewModel.Title);
        await dialog.ShowAsync();
        if (dialog.Result != null)
            Frame.Navigate(typeof(CinemaPage), new KeyValuePair<IProvider, object>(item.Provider, dialog.Result));
    }

    private async void OnTrackRequested(object sender, RoutedEventArgs args)
    {
        var dialog = new ManageTrackerDialog();
        await dialog.ShowAsync();
    }

    private void OnUpdateFavorite(object sender, RoutedEventArgs args)
    {
        var item = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id);
        if (ViewModel.IsFavorite)
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

}