using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaSeason
{

    [EnumMember(Value = "WINTER")] Winter,
    [EnumMember(Value = "SPRING")] Spring,
    [EnumMember(Value = "SUMMER")] Summer,
    [EnumMember(Value = "FALL")] Fall

}