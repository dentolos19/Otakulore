using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Otakulore.Models;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _format;
    [ObservableProperty] private string _content;
    [ObservableProperty] private string _contentLabel;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        var id = int.Parse(query["id"].ToString());
        var data = await App.Client.GetMediaAsync(id);
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Subtitle = data.StartDate.Year.HasValue ? data.StartDate.Year.Value.ToString() : "????";
        Description = data.Description;
        Format = data.Format.ToString();
        switch (data.Type)
        {
            case MediaType.Anime:
                Content = data.Episodes.HasValue ? data.Episodes.Value.ToString() : "???";
                ContentLabel = "Episodes";
                break;
            case MediaType.Manga:
                Content = data.Chapters.HasValue ? data.Chapters.Value.ToString() : "???";
                ContentLabel = "Chapters";
                break;
        }
        var startDate = data.StartDate.ToDateOnly();
        StartDate = startDate.HasValue ? startDate.Value.ToShortDateString() : "???";
        var endDate = data.EndDate.ToDateOnly();
        EndDate = endDate.HasValue ? endDate.Value.ToShortDateString() : "???";
    }

}