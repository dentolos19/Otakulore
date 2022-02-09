using System.Linq;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class DetailsViewModel : BaseViewModel
{

    private bool _isFavorite;

    public MediaType? Type { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public string Description { get; set; }
    public string? Background { get; set; }
    public MediaFormat Format { get; set; }
    public MediaStatus Status { get; set; }
    public string Content { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }

    public bool IsFavorite
    {
        get => _isFavorite;
        set => UpdateProperty(ref _isFavorite, value);
    }

    public static DetailsViewModel Create(Media data)
    {
        var content = "Unknown";
        if (data.Format == MediaFormat.Movie)
            content = data.Duration.HasValue ? data.Duration.Value.ToString() : content;
        else if (data.Episodes.HasValue)
            content = data.Episodes.Value.ToString();
        else if (data.Chapters.HasValue)
            content = data.Chapters.Value.ToString();
        return new DetailsViewModel
        {
            Type = data.Type,
            Id = data.Id,
            ImageUrl = data.CoverImage.ExtraLargeImageUrl,
            Title = data.Title.Romaji,
            Subtitle = data.StartDate.Year.HasValue ? data.StartDate.Year.Value.ToString() : "Unknown Year",
            Description = data.Description ?? "No description provided.",
            Format = (MediaFormat)data.Format,
            Status = data.Status,
            Content = content,
            StartDate = data.StartDate.ToString() ?? "Unknown",
            EndDate = data.EndDate.ToString() ?? "Unknown",
            IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Id == data.Id) != null
        };
    }

}