using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        var topAnime = await App.Jikan.GetAnimeTop();
        var seasonalAnime = await App.Jikan.GetSeason(DateTime.Today.Year, Utilities.GetAnimeSeason(DateOnly.FromDateTime(DateTime.Today)));
        foreach (var entry in topAnime.Top)
            TopAnimeList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in seasonalAnime.SeasonEntries)
            SeasonalAnimeList.Items.Add(MediaItemModel.Create(entry));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}