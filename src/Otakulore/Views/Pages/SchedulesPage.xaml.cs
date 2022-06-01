using System;
using System.Collections.Generic;
using AniListNet.Objects;
using Humanizer;
using Microsoft.UI.Xaml.Controls;
using Otakulore.Views.Panels;

namespace Otakulore.Views.Pages;

public sealed partial class SchedulesPage
{

    public SchedulesPage()
    {
        InitializeComponent();
        var year = DateTime.Now.Year;
        foreach (var season in (MediaSeason[])Enum.GetValues(typeof(MediaSeason)))
        {
            var item = new NavigationViewItem
            {
                Content = season.Humanize(),
                Tag = new KeyValuePair<MediaSeason, int>(season, year)
            };
            PanelNavigation.MenuItems.Add(item);
            if (season == App.CurrentSeason)
                PanelNavigation.SelectedItem = item;
        }
    }

    private void OnNavigatePanel(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem { Tag: object parameter })
            PanelFrame.Navigate(typeof(SchedulePanel), parameter);
    }

}