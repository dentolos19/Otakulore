using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaFormat
{

    [EnumMember(Value = "TV")] Tv,
    [EnumMember(Value = "TV_SHORT")] TvShort,
    [EnumMember(Value = "MOVIE")] Movie,
    [EnumMember(Value = "SPECIAL")] Special,
    [EnumMember(Value = "OVA")] Ova,
    [EnumMember(Value = "ONA")] Ona,
    [EnumMember(Value = "MUSIC")] Music,
    [EnumMember(Value = "MANGA")] Manga,
    [EnumMember(Value = "NOVEL")] Novel,
    [EnumMember(Value = "ONE_SHOT")] OneShot

}