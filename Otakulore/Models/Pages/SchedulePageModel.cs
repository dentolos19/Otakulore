using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class SchedulePageModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    [ObservableProperty] private ObservableCollection<MediaScheduleItemModel> _items = new();

    public SchedulePageModel()
    {
        Task.Run(async () =>
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var results = await _data.Client.GetMediaSchedulesAsync(new MediaSchedulesFilter
            {
                StartedAfterDate = firstDayOfMonth,
                EndedBeforeDate = lastDayOfMonth
            }, new AniPaginationOptions(1, 50));
            foreach (var item in results.Data)
                Items.Add(new MediaScheduleItemModel(item));
        });
    }

}