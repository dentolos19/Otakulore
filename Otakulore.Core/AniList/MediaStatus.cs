using System.ComponentModel;
using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum MediaStatus
{

    [EnumMember(Value = "FINISHED")] Finished,
    [EnumMember(Value = "RELEASING")] Releasing,
    [EnumMember(Value = "NOT_YET_RELEASED")] [Description("Not Yet Released")] NotYetReleased,
    [EnumMember(Value = "CANCELLED")] Cancelled,
    [EnumMember(Value = "HIATUS")] Hiatus

}