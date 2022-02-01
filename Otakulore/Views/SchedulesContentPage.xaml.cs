﻿using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class SchedulesContentPage
{

    public SchedulesContentPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not KeyValuePair<int, MediaSeason>(var year, var season))
            return;
        ProgressIndicator.IsActive = true;
        var schedule = await App.Client.GetSeasonalMedia(season, year);
        foreach (var entry in schedule)
            ScheduleList.Items.Add(new MediaItemModel(entry));
        ProgressIndicator.IsActive = false;
    }

    private void OnItemClick(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            App.NavigateMainContent(typeof(DetailsPage), item.Data);
    }

}