using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaStatus
{

    [EnumMember(Value = "FINISHED")] Finished,
    [EnumMember(Value = "RELEASING")] Releasing,

    [EnumMember(Value = "NOT_YET_RELEASED")] [Description("Not Yet Released")]
    NotYetReleased,
    [EnumMember(Value = "CANCELLED")] Cancelled,
    [EnumMember(Value = "HIATUS")] Hiatus

}