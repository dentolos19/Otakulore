using AniListNet.Objects;

namespace Otakulore.Models;

public class MediaScheduleItemModel
{

    public string Title { get; }
    public DateTime Time { get; }
    public DateTime EndTime { get; }

    public MediaScheduleItemModel(MediaSchedule media)
    {
        Title = media.Media.Title.PreferredTitle + (media.Media.Type == MediaType.Anime && media.Media.Episodes.HasValue
            ? media.Media.Format == MediaFormat.Movie
                ? string.Empty
                : $" (Episode {media.Media.Episodes})"
            : string.Empty);
        Time = media.AiringTime;
        // EndTime = Time.AddMinutes(media.Media.Duration?.TotalMinutes ?? 30);
        EndTime = Time.AddMinutes(30);
    }

}