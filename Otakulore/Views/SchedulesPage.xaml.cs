using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class SchedulesPage
{

    public SchedulesPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        var schedules = await App.Jikan.GetSchedule();
        foreach (var entry in schedules.Monday)
            MondayScheduleList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in schedules.Tuesday)
            TuesdayScheduleList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in schedules.Wednesday)
            WednesdayScheduleList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in schedules.Thursday)
            ThursdayScheduleList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in schedules.Friday)
            FridayScheduleList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in schedules.Saturday)
            SaturdayScheduleList.Items.Add(MediaItemModel.Create(entry));
        foreach (var entry in schedules.Sunday)
            SundayScheduleList.Items.Add(MediaItemModel.Create(entry));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}