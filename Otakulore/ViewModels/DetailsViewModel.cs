using Humanizer;
using Otakulore.Core.Kitsu;

namespace Otakulore.ViewModels
{

    public class DetailsViewModel
    {

        public string ImageUrl { get; init; }
        public string Title { get; init; }
        public string Subtitle { get; init; }

        public string Format { get; init; }
        public string Status { get; init; }
        public string Episodes { get; init; }
        public string StartingDate { get; init; }
        public string EndingDate { get; init; }

        public string Synopsis { get; init; }

        public static DetailsViewModel CreateModel(KitsuData data)
        {
            return new()
            {
                ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                Title = data.Attributes.CanonicalTitle,
                Subtitle = data.Attributes.StartingDate?.Substring(0, 4) ?? "Unknown",
                Format = data.Attributes.Format.Humanize(),
                Status = data.Attributes.Status.Transform(To.TitleCase),
                Episodes = data.Attributes.EpisodeCount.HasValue ? data.Attributes.EpisodeCount.ToString() : "Unknown",
                StartingDate = data.Attributes.StartingDate ?? "TBA",
                EndingDate = data.Attributes.EndingDate ?? "Unknown",
                Synopsis = data.Attributes.Synopsis
            };
        }

    }

}