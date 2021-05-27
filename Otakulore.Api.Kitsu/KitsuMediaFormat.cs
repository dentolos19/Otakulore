using System.Runtime.Serialization;

namespace Otakulore.Api.Kitsu
{

    public enum KitsuMediaFormat
    {

        [EnumMember(Value = "ONA")] Ona,
        [EnumMember(Value = "OVA")] Ova,
        [EnumMember(Value = "TV")] Tv,
        [EnumMember(Value = "movie")] Movie,
        [EnumMember(Value = "music")] Music,
        [EnumMember(Value = "special")] Special

    }

}