using System.ComponentModel;
using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum MediaEntrySort
{

    [EnumMember(Value = "UPDATED_TIME_DESC")] [Description("Last Updated")] LastUpdated,
    [EnumMember(Value = "PROGRESS_DESC")] Progress,
    [EnumMember(Value = "SCORE_DESC")] Score

}