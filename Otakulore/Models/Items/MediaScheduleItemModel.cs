using AniListNet.Objects;

namespace Otakulore.Models;

public class MediaScheduleItemModel
{

    public int Id { get; }
    public string Title { get; }
    public DateTime Time { get; }
    public DateTime EndTime { get; }

    public MediaScheduleItemModel(MediaSchedule data)
    {
        Id = data.Media.Id;
        Title = data.Media.Title.PreferredTitle + (data.Media.Type == MediaType.Anime && data.Media.Episodes.HasValue
            ? data.Media.Format == MediaFormat.Movie
                ? string.Empty
                : $" (Episode {data.Media.Episodes})"
            : string.Empty);
        Time = data.AiringTime;
        // EndTime = Time.AddMinutes(data.Media.Duration?.TotalMinutes ?? 30);
        EndTime = Time.AddMinutes(30);
    }

}