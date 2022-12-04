using AniListNet.Objects;
using Humanizer;

namespace Otakulore.Models;

public class MediaScheduleItemModel
{

    public required int Id { get; init; }
    public required Uri ImageUrl { get; init; }
    public required string Title { get; init; }
    public required string Subtitle { get; init; }

    public static MediaScheduleItemModel Map(MediaSchedule schedule)
    {
        return new MediaScheduleItemModel
        {
            Id = schedule.Media.Id,
            ImageUrl = schedule.Media.Cover.ExtraLargeImageUrl,
            Title = schedule.Media.Title.PreferredTitle + (
                schedule.Media.Type == MediaType.Anime && schedule.Media.Episodes.HasValue
                    ? schedule.Media.Format == MediaFormat.Movie
                        ? string.Empty
                        : $" (Episode {schedule.Media.Episodes})"
                    : string.Empty
            ),
            Subtitle = schedule.AiringTime.Humanize()
        };
    }

}