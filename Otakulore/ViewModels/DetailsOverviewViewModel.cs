using Humanizer;
using Otakulore.Core.Services.Kitsu;

namespace Otakulore.ViewModels
{

    public class DetailsOverviewViewModel
    {

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string Episodes { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }

        public static DetailsOverviewViewModel CreateViewModel(KitsuData<KitsuAnimeAttributes> data)
        {
            return new DetailsOverviewViewModel
            {
                ImageUrl = data.Attributes.PosterImage.ImageUrl,
                Title = data.Attributes.CanonicalTitle,
                Synopsis = data.Attributes.Synopsis,
                Format = data.Attributes.Format.Humanize(),
                Status = data.Attributes.Status.Humanize(),
                Episodes = data.Attributes.EpisodeCount?.ToString() ?? "???",
                StartingDate = string.IsNullOrEmpty(data.Attributes.StartingDate) ? "TBA" : data.Attributes.StartingDate,
                EndingDate = string.IsNullOrEmpty(data.Attributes.EndingDate) ? "???" : data.Attributes.EndingDate
            };
        }

    }

}