using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public partial class SeasonalViewModel : ObservableObject
{

    private Task? _currentTask;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public MediaSeason? CurrentSeason { get; private set; }

    [ICommand]
    private void LoadMoreItems()
    {
        if (_currentTask is { IsCompleted: false })
            return;
        _currentTask = Task.Run(async () =>
        {
            if (CurrentSeason is not { } season)
                return;
            IsLoading = true;
            var results = (await App.Client.GetMediaBySeason(season)).Data;
            foreach (var media in results)
                Items.Add(new MediaItemModel(media));
            IsLoading = false;
        });
    }

    public void SetSeason(MediaSeason season)
    {
        CurrentSeason = season;
        LoadMoreItems();
    }

}