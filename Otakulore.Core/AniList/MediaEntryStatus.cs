using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MediaEntryStatus
{

    [EnumMember(Value = "CURRENT")] Current,
    [EnumMember(Value = "PLANNING")] Planning,
    [EnumMember(Value = "COMPLETED")] Completed,
    [EnumMember(Value = "DROPPED")] Dropped,
    [EnumMember(Value = "PAUSED")] Paused,
    [EnumMember(Value = "REPEATED")] Repeating

}