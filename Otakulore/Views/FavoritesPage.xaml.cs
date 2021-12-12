using System.Windows;
using System.Windows.Input;
using Otakulore.Models;

namespace Otakulore.Views;

public partial class FavoritesPage
{

    public FavoritesPage()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
        FavoriteList.Items.Clear();
        foreach (var item in App.Settings.Favorites)
            FavoriteList.Items.Add(item);
    }

    private void OnOpenMedia(object sender, MouseButtonEventArgs args)
    {
        if (FavoriteList.SelectedItem is MediaItemModel item)
            NavigationService.Navigate(new DetailsPage(item.Type, item.Id));
    }
}