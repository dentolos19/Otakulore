using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum CharacterRole
{

    [EnumMember(Value = "MAIN")] Main,
    [EnumMember(Value = "SUPPORTING")] Supporting,
    [EnumMember(Value = "BACKGROUND")] Background

}