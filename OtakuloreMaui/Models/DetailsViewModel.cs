using CommunityToolkit.Mvvm.ComponentModel;
using JikanDotNet;

namespace Otakulore.Models;

[ObservableObject]
public partial class DetailsViewModel : IQueryAttributable
{

    private readonly IJikan _client = new Jikan();

    [ObservableProperty] private string _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _synopsis;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;
    [ObservableProperty] private string _episodes;
    [ObservableProperty] private bool _isLoading = true;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!long.TryParse((string)query["id"], out var id))
            return;
        var data = (await _client.GetAnimeAsync(id)).Data;
        ImageUrl = data.Images.JPG.ImageUrl;
        Title = data.Title;
        Synopsis = data.Synopsis;
        StartDate = data.Aired.To.HasValue ? data.Aired.To.Value.ToShortDateString() : "???";
        EndDate = data.Aired.From.HasValue ? data.Aired.From.Value.ToShortDateString() : "???";
        Episodes = data.Episodes.HasValue ? data.Episodes.Value.ToString() : "???";
        IsLoading = false;
    }

}