using JikanDotNet;

namespace Otakulore.Models;

public class ReviewItemModel
{

    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public int? Score { get; init; }
    public string Content { get; init; }
    public string Date { get; init; }

    public static ReviewItemModel Create(AnimeReview animeReview)
    {
        return new ReviewItemModel
        {
            ImageUrl = animeReview.Reviewer.ImageURL,
            Name = animeReview.Reviewer.Username,
            Score = animeReview.Reviewer.Scores.Overall.HasValue ? animeReview.Reviewer.Scores.Overall / 2 : null,
            Content = animeReview.Content,
            Date = animeReview.Date.HasValue ? animeReview.Date.Value.ToString("yyyy-MM-dd") : "Unknown Post Date"
        };
    }

    public static ReviewItemModel Create(MangaReview mangaReview)
    {
        return new ReviewItemModel
        {
            ImageUrl = mangaReview.Reviewer.ImageURL,
            Name = mangaReview.Reviewer.Username,
            Score = mangaReview.Reviewer.Scores.Overall.HasValue ? mangaReview.Reviewer.Scores.Overall / 2 : null,
            Content = mangaReview.Content,
            Date = mangaReview.Date.HasValue ? mangaReview.Date.Value.ToString("yyyy-MM-dd") : "Unknown Post Date"
        };
    }

}