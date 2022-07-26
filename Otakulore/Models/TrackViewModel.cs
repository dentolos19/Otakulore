using System.Collections.ObjectModel;
using AniListNet.Objects;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class TrackViewModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    [ObservableProperty] private MediaEntryStatus _status = MediaEntryStatus.Planning;
    [ObservableProperty] private string _progress = "0";
    [ObservableProperty] private DateTime? _startDate;
    [ObservableProperty] private DateTime? _completeDate;
    [ObservableProperty] private ObservableCollection<MediaEntryStatus> _statuses = new();

    public TrackViewModel()
    {
        foreach (var @enum in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            Statuses.Add(@enum);
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        var entry = await _data.Client.GetMediaEntryAsync(id);
        if (entry is null)
            return;
        Status = entry.Status;
        Progress = entry.Progress.ToString();
        StartDate = entry.StartDate.ToDateTime();
        CompleteDate = entry.CompleteDate.ToDateTime();
    }

    [ICommand]
    private async Task Save()
    {
        await Toast.Make("This feature is not implemented yet!").Show(); // TODO: implement feature
    }

    [ICommand]
    private async Task Delete()
    {
        await Toast.Make("This feature is not implemented yet!").Show(); // TODO: implement feature
    }

    [ICommand]
    private Task Cancel()
    {
        return MauiHelper.NavigateBack();
    }

}