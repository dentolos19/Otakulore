using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

[ObservableObject]
public partial class DetailsViewModel : IQueryAttributable
{

    private readonly AniClient _client = new();

    [ObservableProperty] private string _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;
    [ObservableProperty] private string _episodes;
    [ObservableProperty] private bool _isLoading;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!int.TryParse((string)query["id"], out var id))
            return;
        IsLoading = true;
        var data = await _client.GetMedia(id);
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.Preferred;
        Description = data.Description ?? "No description has been provided.";
        StartDate = data.StartDate.HasValue ? data.StartDate.Value.ToShortDateString() : "???";
        EndDate = data.EndDate.HasValue ? data.EndDate.Value.ToShortDateString() : "???";
        Episodes = data.Content.HasValue ? data.Content.Value.ToString() : "???";
        IsLoading = false;
    }

}