using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class ContentViewerViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private string _title;
    [ObservableProperty] private Uri _currentAddress;
    [ObservableProperty] private bool _isLoading;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!(query.ContainsKey("data") && query.ContainsKey("provider")))
            return;
        var data = (MediaContent)query["data"];
        var provider = (IProvider)query["provider"];
        Title = data.Name;
        if (provider is IAnimeProvider animeProvider)
        {
            Task.Run(async () =>
            {
                IsLoading = true;
                var hasVideoUrl = await animeProvider.TryExtractVideoPlayer(data, out var videoUrl);
                IsLoading = false;
                CurrentAddress = hasVideoUrl
                    ? videoUrl
                    : new Uri(data.Data.ToString());
            });
        }
        else
        {
            CurrentAddress = new Uri(data.Data.ToString());
        }
    }

    [ICommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }

}