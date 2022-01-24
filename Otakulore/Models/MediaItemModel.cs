using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaItemModel
{

    public MediaItemModel(Media data)
    {
        Data = data;
        ImageUrl = Data.CoverImage.LargeImageUrl;
        Title = Data.Title.Romaji;
        if (App.Settings.UseEnglishTitles && Data.Title.English != null)
            Title = Data.Title.English;
        Score = Data.Score.HasValue ? Data.Score.Value / 20 : 0;
        ScoreCaption = data.Score?.ToString() ?? "Unknown";
        Description = !string.IsNullOrEmpty(Data.Description) ? Utilities.HtmlToPlainText(Data.Description) : "No description provided.";
    }

    public string ImageUrl { get; }
    public string Title { get; }
    public double? Score { get; }
    public string ScoreCaption { get; }
    public string? Description { get; }
    public Media Data { get; }

}