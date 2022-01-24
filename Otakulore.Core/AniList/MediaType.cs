using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaType
{

    [EnumMember(Value = "ANIME")] Anime,
    [EnumMember(Value = "MANGA")] Manga

}