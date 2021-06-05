using Humanizer;
using Otakulore.Core.Kitsu;

namespace Otakulore.Models
{

    public class ShelfItemModel
    {

        public string ImageUrl { get; init; }
        public string Title { get; init; }
        public KitsuData Data { get; init; }

        public string Subtitle
        {
            get
            {
                var subtitle = string.Empty;
                subtitle += Data.Attributes.Format.Humanize();
                subtitle += " | ";
                subtitle += Data.Attributes.StartingDate?.Substring(0, 4) ?? "Unknown";
                subtitle += " | ";
                subtitle += Data.Attributes.Status.Humanize();
                return subtitle;
            }
        }

    }

}