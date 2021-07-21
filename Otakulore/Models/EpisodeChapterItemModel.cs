using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Manga;

namespace Otakulore.Models
{

    public class EpisodeChapterItemModel
    {
        
        public string Text { get; set; }
        public string Url { get; set; }

        public static EpisodeChapterItemModel CreateModel(AnimeEpisode episode)
        {
            return new EpisodeChapterItemModel
            {
                Text = episode.Number != null ? "Episode " + episode.Number : "Unknown Episode",
                Url = episode.Url
            };
        }

        public static EpisodeChapterItemModel CreateModel(MangaChapter chapter)
        {
            return new EpisodeChapterItemModel
            {
                Text = chapter.Name,
                Url = chapter.Url
            };
        }

    }

}