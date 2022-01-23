using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.AniList;
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
        foreach (var entry in App.Settings.Favorites)
            FavoriteList.Items.Add(new MediaItemModel(entry));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Data);
    }

}