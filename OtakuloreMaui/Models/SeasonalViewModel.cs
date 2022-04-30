using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public partial class SeasonalViewModel : ObservableObject
{
    
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public MediaSeason? CurrentSeason { get; private set; }
    
    public void SetSeason(MediaSeason season)
    {
        CurrentSeason = season;
        Task.Run(async () =>
        {
            Items.Clear();
            IsLoading = true;
            var results = (await App.Client.GetMediaBySeason(season)).Data;
            foreach (var media in results)
                Items.Add(new MediaItemModel(media));
            IsLoading = false;
        });
    }

}