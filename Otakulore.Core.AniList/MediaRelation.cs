using System.ComponentModel;
using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum MediaRelation
{

    [EnumMember(Value = "ADAPTATION")] Adaptation,
    [EnumMember(Value = "PREQUEL")] Prequel,
    [EnumMember(Value = "SEQUEL")] Sequel,
    [EnumMember(Value = "PARENT")] Parent,
    [EnumMember(Value = "SIDE_STORY")] [Description("Side Story")] SideStory,
    [EnumMember(Value = "CHARACTER")] Character,
    [EnumMember(Value = "SUMMARY")] Summary,
    [EnumMember(Value = "ALTERNATIVE")] Alternative,
    [EnumMember(Value = "SPIN_OFF")] [Description("Spin-Off")] SpinOff,
    [EnumMember(Value = "OTHER")] Other,
    [EnumMember(Value = "SOURCE")] Source,
    [EnumMember(Value = "COMPILATION")] Compilation,
    [EnumMember(Value = "CONTAINS")] Contains

}