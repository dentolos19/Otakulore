using Humanizer;
using Otakulore.Core.Kitsu;

namespace Otakulore.Models
{

    public class ContentItemModel
    {

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public KitsuData Data { get; set; }

        public static ContentItemModel CreateModel(KitsuData data)
        {
            return new ContentItemModel
            {
                ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                Title = data.Attributes.CanonicalTitle,
                Subtitle = $"{data.Attributes.Format.Humanize()} | " +
                           $"{data.Attributes.StartingDate?.Substring(0, 4) ?? "????"} | " +
                           data.Attributes.Status.Humanize(),
                Data = data
            };
        }

    }

}