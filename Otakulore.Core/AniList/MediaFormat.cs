using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaFormat
{

    [EnumMember(Value = "TV")] [Description("TV")] Tv,
    [EnumMember(Value = "TV_SHORT")] [Description("TV Short")] TvShort,
    [EnumMember(Value = "MOVIE")] Movie,
    [EnumMember(Value = "SPECIAL")] Special,
    [EnumMember(Value = "OVA")] [Description("OVA")] Ova,
    [EnumMember(Value = "ONA")] [Description("ONA")] Ona,
    [EnumMember(Value = "MUSIC")] Music,
    [EnumMember(Value = "MANGA")] Manga,
    [EnumMember(Value = "NOVEL")] Novel,
    [EnumMember(Value = "ONE_SHOT")] [Description("One-Shot")] OneShot

}