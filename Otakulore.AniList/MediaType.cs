using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaType
{

    [EnumMember(Value = "ANIME")] Anime,
    [EnumMember(Value = "MANGA")] Manga

}