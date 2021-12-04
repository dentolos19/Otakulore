using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class FavoritesPage
{

    private FavoriteViewModel ViewModel => (FavoriteViewModel)DataContext;

    public FavoritesPage()
    {
        InitializeComponent();
        ((CollectionView)CollectionViewSource.GetDefaultView(FavoriteList.ItemsSource)).Filter = FilterFavorite;
    }

    private bool FilterFavorite(object item)
    {
        if (item is not MediaItemModel mediaItem)
            return false;
        var filter = FilterInput.Text;
        return TypeSelection.SelectedIndex switch
        {
            1 => mediaItem.Title.Contains(filter, StringComparison.OrdinalIgnoreCase) && mediaItem.Type == MediaType.Anime,
            2 => mediaItem.Title.Contains(filter, StringComparison.OrdinalIgnoreCase) && mediaItem.Type == MediaType.Manga,
            _ => mediaItem.Title.Contains(filter, StringComparison.OrdinalIgnoreCase)
        };
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        foreach (var mediaItem in App.Settings.Favorites)
            ViewModel.FavoriteList.Add(mediaItem);
        TypeSelection.SelectedIndex = 0;
    }

    private void OnFilter(object sender, TextChangedEventArgs args)
    {
        CollectionViewSource.GetDefaultView(FavoriteList.ItemsSource).Refresh();
    }

    private void OnTypeChange(object sender, SelectionChangedEventArgs args)
    {
        CollectionViewSource.GetDefaultView(FavoriteList.ItemsSource).Refresh();
    }

    private void OnOpenMedia(object sender, MouseButtonEventArgs args)
    {
        if (FavoriteList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}