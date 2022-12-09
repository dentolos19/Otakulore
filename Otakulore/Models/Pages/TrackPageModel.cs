using System.Collections.ObjectModel;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class TrackPageModel : BasePageModel
{

    private int _id;

    [ObservableProperty] private MediaEntryStatus _selectedStatus = MediaEntryStatus.Planning;
    [ObservableProperty] private string _progress = "0";
    [ObservableProperty] private bool _startDateEnabled;
    [ObservableProperty] private DateTime _startDate = DateTime.Today;
    [ObservableProperty] private bool _completeDateEnabled;
    [ObservableProperty] private DateTime _completeDate = DateTime.Today;

    [ObservableProperty] private ObservableCollection<MediaEntryStatus> _statuses = new();

    public TrackPageModel()
    {
        foreach (var @enum in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            Statuses.Add(@enum);
    }

    protected override async void Initialize(object? args = null)
    {
        if (args is not int id)
            return;
        _id = id;
        var entry = await DataService.Instance.Client.GetMediaEntryAsync(_id);
        if (entry is null)
            return;
        SelectedStatus = entry.Status;
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
            await ParentPage.DisplayAlert(
                "Otakulore",
                "Make sure to enter a valid progress value.",
                "Okay"
            );
            return;
        }
        try
        {
            var mutation = new MediaEntryMutation
            {
                Status = SelectedStatus,
                Progress = progress
            };
            if (StartDateEnabled)
                mutation.StartDate = StartDate;
            if (CompleteDateEnabled)
                mutation.CompleteDate = CompleteDate;
            await DataService.Instance.Client.SaveMediaEntryAsync(_id, mutation);
            await MauiHelper.NavigateBack();
        }
        catch
        {
            await ParentPage.DisplayAlert(
                "Unsuccessful",
                "We are unable to complete your request, please try again later.",
                "Okay"
            );
        }
    }

    [RelayCommand]
    private async Task Delete()
    {
        var canDelete = await ParentPage.DisplayAlert(
            "Confirmation",
            "Are you sure that you want to delete this entry?",
            "Yes",
            "No"
        );
        if (!canDelete)
            return;
        try
        {
            _ = await DataService.Instance.Client.DeleteMediaEntryAsync(_id);
            await MauiHelper.NavigateBack();
        }
        catch
        {
            await ParentPage.DisplayAlert(
                "Unsuccessful",
                "We are unable to complete your request, please try again later.",
                "Okay"
            );
        }
    }

    [RelayCommand]
    private Task Cancel() => MauiHelper.NavigateBack();

}