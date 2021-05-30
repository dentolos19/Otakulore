using Otakulore.Core.Kitsu;

namespace Otakulore.Models
{

    public class ShelfItemModel
    {

        public string ImageUrl { get; init; }
        public string Title { get; init; }
        public object Data { get; init; }

        public string Subtitle
        {
            get
            {
                var subtitle = string.Empty;
                if (Data is not KitsuData animeData)
                    return subtitle;
                subtitle += animeData.Attributes.StartingDate.Substring(0, 4);
                subtitle += " | ";
                subtitle += animeData.Attributes.AgeRating ?? "Unknown Age Rating";
                subtitle += " | ";
                subtitle += animeData.Attributes.Status;
                return subtitle;
            }
        }

    }

}