using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaItemModel
{

    public MediaItemModel(Media data)
    {
        Data = data;
        ImageUrl = Data.CoverImage.LargeImageUrl;
        BannerImageUrl = Data.BannerImageUrl;
        Title = Data.Title.Romaji;
        Format = data.Format.GetEnumDescription();
        Score = Data.Score.HasValue ? Data.Score.Value / 20 : 0;
        ScoreCaption = data.Score?.ToString() ?? "Unknown";
        Description = !string.IsNullOrEmpty(Data.Description) ? Utilities.HtmlToPlainText(Data.Description) : "No description provided.";
    }

    public string ImageUrl { get; }
    public string? BannerImageUrl { get; }
    public string Title { get; }
    public string Format { get; }
    public double Score { get; }
    public string ScoreCaption { get; }
    public string? Description { get; }
    public Media Data { get; }

}