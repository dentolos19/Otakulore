using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class FavoritesPage
{

    public FavoritesPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        var favorites = App.Settings.Favorites;
        foreach (var entry in favorites)
            FavoriteList.Items.Add(entry);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}