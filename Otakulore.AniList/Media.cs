using System.Text.Json.Serialization;

namespace Otakulore.AniList;

public class Media
{

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("idMal")] public int MalId { get; init; }

    [JsonPropertyName("coverImage")] public Cover Cover { get; init; }
    [JsonPropertyName("title")] public MediaTitle Title { get; init; }
    [JsonPropertyName("description")] public string Description { get; init; }
    [JsonPropertyName("type")] public MediaType Type { get; init; }
    [JsonPropertyName("format")] public MediaFormat? Format { get; init; }
    [JsonPropertyName("status")] public MediaStatus Status { get; init; }
    [JsonPropertyName("genres")] public string[]? Genres { get; init; }
    [JsonPropertyName("averageScore")] public int? Score { get; init; }
    [JsonPropertyName("startDate")] public Date StartDate { get; init; }
    [JsonPropertyName("endDate")] public Date EndDate { get; init; }
    [JsonPropertyName("isAdult")] public bool IsAdult { get; init; }

    [JsonPropertyName("episodes")] public int? Episodes { get; init; }
    [JsonPropertyName("duration")] public int? EpisodesDuration { get; init; }
    [JsonPropertyName("chapters")] public int? Chapters { get; init; }

}