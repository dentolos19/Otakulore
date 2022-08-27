using System.Collections.ObjectModel;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core.Attributes;
using Otakulore.Services;

namespace Otakulore.Models;

[AsTransientService]
public partial class MediaTrackPageModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private int _id;

    [ObservableProperty] private MediaEntryStatus _status = MediaEntryStatus.Planning;
    [ObservableProperty] private string _progress = "0";
    [ObservableProperty] private bool _startDateEnabled;
    [ObservableProperty] private DateTime _startDate = DateTime.Today;
    [ObservableProperty] private bool _completeDateEnabled;
    [ObservableProperty] private DateTime _completeDate = DateTime.Today;
    [ObservableProperty] private ObservableCollection<MediaEntryStatus> _statuses = new();

    public MediaTrackPageModel()
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
        _id = id;
        var entry = await _data.Client.GetMediaEntryAsync(_id);
        if (entry is null)
            return;
        Status = entry.Status;
        Progress = entry.Progress.ToString();
        var startDate = entry.StartDate.ToDateTime();
        var completeDate = entry.CompleteDate.ToDateTime();
        if (startDate.HasValue)
        {
            StartDateEnabled = true;
            StartDate = startDate.Value;
        }
        if (completeDate.HasValue)
        {
            CompleteDateEnabled = true;
            CompleteDate = completeDate.Value;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (!int.TryParse(Progress, out var progress))
        {
            await Toast.Make("Make sure your progress value is valid!").Show();
            return;
        }
        try
        {
            var mutation = new MediaEntryMutation
            {
                Status = Status,
                Progress = progress
            };
            if (StartDateEnabled)
                mutation.StartDate = StartDate;
            if (CompleteDateEnabled)
                mutation.CompleteDate = CompleteDate;
            await _data.Client.SaveMediaEntryAsync(_id, mutation);
            await MauiHelper.NavigateBack();
        }
        catch
        {
            await Toast.Make("The operation was unsuccessful!").Show();
        }
    }

    [RelayCommand]
    private async Task Delete()
    {
        try
        {
            _ = await _data.Client.DeleteMediaEntryAsync(_id);
            await MauiHelper.NavigateBack();
        }
        catch
        {
            await Toast.Make("The operation was unsuccessful!").Show();
        }
    }

    [RelayCommand]
    private Task Cancel()
    {
        return MauiHelper.NavigateBack();
    }

}