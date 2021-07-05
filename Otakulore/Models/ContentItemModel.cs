using Windows.UI.Xaml;
using Humanizer;
using Otakulore.Core.Services.Kitsu;

namespace Otakulore.Models
{

    public class ContentItemModel
    {

        public Visibility RatingVisibility { get; set; }
        public Visibility SynopsisVisibility { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Synopsis { get; set; }
        public double Rating { get; set; }

        public KitsuData<KitsuAnimeAttributes> Data { get; set; }

        public static ContentItemModel CreateModel(KitsuData<KitsuAnimeAttributes> data)
        {
            var model = new ContentItemModel
            {
                ImageUrl = data.Attributes.PosterImage.ImageUrl,
                Title = data.Attributes.CanonicalTitle,
                Subtitle = $"{data.Attributes.Format.Humanize()} | " +
                           $"{data.Attributes.StartingDate?.Substring(0, 4) ?? "????"} | " +
                           data.Attributes.Status.Humanize(),
                Data = data
            };
            if (!string.IsNullOrEmpty(data.Attributes.Synopsis))
                model.Synopsis = data.Attributes.Synopsis;
            else
                model.SynopsisVisibility = Visibility.Collapsed;
            if (double.TryParse(data.Attributes.AverageRating, out var rating))
                model.Rating = rating / 20;
            else
                model.RatingVisibility = Visibility.Collapsed;
            return model;
        }

    }

}