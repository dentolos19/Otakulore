using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Views.Panels;

namespace Otakulore.Views.Pages;

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
            PanelNavigation.MenuItems.Add(item);
            if (season != currentSeason)
                continue;
            item.Content += " (Current)";
            PanelNavigation.SelectedItem = item;
        }
    }

    private void OnNavigatePanel(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item)
            PanelFrame.Navigate(typeof(SchedulePanel), new KeyValuePair<MediaSeason, int>((MediaSeason)item.Tag, DateTime.Today.Year));
    }

}