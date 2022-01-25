using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class MutationResponse
{

    [JsonPropertyName("UpdateUser")] public User User { get; init; }

}