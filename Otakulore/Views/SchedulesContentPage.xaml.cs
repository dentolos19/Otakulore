using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Controls;
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
        ScheduleResultIndicator.State = ResultIndicatorState.Loading;
        try
        {
            var schedule = await App.Client.GetSeasonalMedia(season, year);
            if (schedule is not { Length: > 0 })
            {
                ScheduleResultIndicator.Text = "Yielded no results";
                ScheduleResultIndicator.State = ResultIndicatorState.NoResult;
                return;
            }
            ScheduleResultIndicator.State = ResultIndicatorState.None;
            foreach (var entry in schedule)
                ScheduleList.Items.Add(new MediaItemModel(entry));
        }
        catch (Exception exception)
        {
            ScheduleResultIndicator.Text = $": {exception.Message}";
            ScheduleResultIndicator.State = ResultIndicatorState.NoResult;
        }
    }

    private void OnItemClick(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            App.NavigateMainContent(typeof(DetailsPage), item.Data);
    }

}