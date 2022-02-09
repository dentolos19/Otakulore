using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Views;

public sealed partial class SchedulesPage
{

    public SchedulesPage()
    {
        InitializeComponent();
        var currentSeason = Utilities.GetSeasonFromDate(DateTime.Today);
        foreach (var season in (MediaSeason[])Enum.GetValues(typeof(MediaSeason)))
        {
            var item = new NavigationViewItem
            {
                Content = season.ToString(),
                Tag = season
            };
            SchedulesView.MenuItems.Add(item);
            if (season != currentSeason)
                continue;
            item.Content += " (Current)";
            SchedulesView.SelectedItem = item;
        }
    }

    private void OnNavigationChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item)
            ContentView.Navigate(typeof(SchedulePage), new KeyValuePair<MediaSeason, int>((MediaSeason)item.Tag, DateTime.Today.Year));
    }

}